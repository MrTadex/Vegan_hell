using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PickUp : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    int type = 0;

    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();

    [SerializeField]
    List<GameObject> players = new List<GameObject>();

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        type = Random.Range(0, sprites.Count);
        spriteRenderer.sprite = sprites[type];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.PlaySound("PowerUp");
            ChangePlayer(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void ChangePlayer(GameObject oldPlayer)
    {
        GameObject newPlayer = Instantiate(players[type], oldPlayer.transform.position, oldPlayer.transform.rotation);

        newPlayer.GetComponent<EnemySpawn>().GetCopyOf(oldPlayer.GetComponent<EnemySpawn>());
        
        Camera.main.GetComponent<CameraFollow>().target = newPlayer;
        Destroy(oldPlayer);
    }
}

public static class ComponentExtensions
{
    public static T GetCopyOf<T>(this T comp, T other) where T : Component
    {
        System.Type type = comp.GetType();
        System.Type othersType = other.GetType();
        if (type != othersType)
        {
            Debug.LogError($"The type \"{type.AssemblyQualifiedName}\" of \"{comp}\" does not match the type \"{othersType.AssemblyQualifiedName}\" of \"{other}\"!");
            return null;
        }

        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default;
        PropertyInfo[] pinfos = type.GetProperties(flags);

        foreach (var pinfo in pinfos)
        {
            if (pinfo.CanWrite)
            {
                try
                {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch
                {
                    /*
                     * In case of NotImplementedException being thrown.
                     * For some reason specifying that exception didn't seem to catch it,
                     * so I didn't catch anything specific.
                     */
                }
            }
        }

        FieldInfo[] finfos = type.GetFields(flags);

        foreach (var finfo in finfos)
        {
            finfo.SetValue(comp, finfo.GetValue(other));
        }
        return comp as T;
    }
}
