using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer sp;
    GameManagement gameManager;

    bool chase = true;

    private GameObject playerObj;

    [SerializeField]
    public float runSpeed = 5.0f;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sp= GetComponent<SpriteRenderer>();

        gameManager = FindObjectOfType<GameManagement>();

        // find player by script
        if (playerObj == null)
            playerObj = FindObjectOfType<PlayerControler>().gameObject;
    }

    void FixedUpdate()
    {

        if(playerObj.transform.position.x > transform.position.x)
        {
            sp.flipX= true;
        }
        else
        {
            sp.flipX = false;
        }

        if (chase)
            transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, runSpeed * Time.fixedDeltaTime);
    }

    //Just hit another collider 2D
   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            chase = false;
            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            gameManager.maxEnemyKills++;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            chase = false;
            gameManager.Health--;
            // audioSrc.PlayOneShot ("Bullet_shoot");
        }

        if (collision.gameObject.tag == "Mele")
        {
            gameManager.maxEnemyKills++;
            Destroy(gameObject);
        }
    }
}
