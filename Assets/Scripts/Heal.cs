using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    GameManagement gameManager;

    int type = 0;

    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManagement>();

        type = Random.Range(0, sprites.Count);
        spriteRenderer.sprite = sprites[type];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameManager.Health < 6)
        {
            //SoundManager.PlaySound("Fire_hit_player");

            switch (type)
            {
                case 0: // Half Heal
                    if (gameManager.Health < 6)
                        gameManager.Health++;
                    collision.gameObject.GetComponent<Animator>().SetTrigger("Half");
                    break;
                case 1: // Full Heal
                    if (gameManager.Health < 5)
                        gameManager.Health += 2;
                    else if (gameManager.Health < 6)
                        gameManager.Health++;
                    collision.gameObject.GetComponent<Animator>().SetTrigger("Full");
                    break;
            }
            SoundManager.PlaySound("Heal");
            Destroy(gameObject);
        }
    }

}
