using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer sp;

    float horizontal;
    float vertical;

    bool chase = true;

    private GameObject playerObj;

    [SerializeField]
    public float runSpeed = 5.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sp= GetComponent<SpriteRenderer>();

        // find player by script
        if (playerObj == null)
            playerObj = FindObjectOfType<PlayerControler>().gameObject;
    }

    void FixedUpdate()
    {
        /*Debug.Log("Player Position: X = " + playerObj.transform.position.x + " --- Y = " + playerObj.transform.position.y + " --- Z = " + 
        playerObj.transform.position.z);*/

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
            Destroy(collision.gameObject);
            Destroy(gameObject);
            // maxEnemyKills++;
        }

        if (collision.gameObject.tag == "Player")
        {
            chase = false;
        }
    }
}
