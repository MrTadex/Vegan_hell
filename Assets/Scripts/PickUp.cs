using System.Collections;
using System.Collections.Generic;
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
            //SoundManager.PlaySound("Fire_hit_player");
            ChangePlayer(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void ChangePlayer(GameObject oldPlayer)
    {
        GameObject newPlayer = Instantiate(players[type], oldPlayer.transform.position, oldPlayer.transform.rotation);
        UnityEditorInternal.ComponentUtility.CopyComponent(oldPlayer.GetComponent<EnemySpawn>());
        Destroy(newPlayer.GetComponent<EnemySpawn>());
        UnityEditorInternal.ComponentUtility.PasteComponentAsNew(newPlayer);
        Camera.main.GetComponent<CameraFollow>().target = newPlayer;
        Destroy(oldPlayer);
    }
}
