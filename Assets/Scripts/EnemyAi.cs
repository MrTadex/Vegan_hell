using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer sp;
    Animator animator;
    GameManagement gameManager;

    bool chase = true;

    [SerializeField]
    int Heath = 2;

    private GameObject playerObj;

    [SerializeField]
    public float runSpeed = 5.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sp= GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManagement>();

        // find player by script
        if (playerObj == null)
            playerObj = FindObjectOfType<PlayerControler>().gameObject;
    }

    private void Update()
    {
        if (Heath < 1)
        {
            animator.SetTrigger("Dead");
            chase = false;
            Destroy(gameObject, 1);
        }
    }

    void FixedUpdate()
    {
        if (chase)
        {
            if (playerObj.transform.position.x > transform.position.x)
            {
                body.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                body.transform.localScale = new Vector3(1, 1, 1);
            }

            transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, runSpeed * Time.fixedDeltaTime);
        }
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
            animator.SetTrigger("Damaged");
            Heath--;
        }

        if (collision.gameObject.tag == "Player")
        {
            chase = false;
        }

        if (collision.gameObject.tag == "Melee")
        {
            gameManager.maxEnemyKills++;
            animator.SetTrigger("Damaged");
            Heath -= 2;
        }
    }
}
