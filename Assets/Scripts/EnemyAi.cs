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

    Transform playerObj;

    [SerializeField]
    public float runSpeed = 5.0f;

    bool Invicibale = false;

    [SerializeField]
    float InvicibaleDuration = 1.0f;

    [SerializeField]
    int DropChanceClass = 10;

    [SerializeField]
    int DropChanceHealth = 30;

    [SerializeField]
    int DropRatio = 10;

    [SerializeField]
    List<GameObject> PickUp = new List<GameObject>();

    Transform PickUpContainer;

    bool onlyOnce = true;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sp= GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        gameManager = FindObjectOfType<GameManagement>();
        PickUpContainer = GameObject.Find("PickUps").transform;

        playerObj = Camera.main.GetComponent<CameraFollow>().target.transform;
    }

    private void Update()
    {
        if(!((transform.position.x > -100 && transform.position.x < 100) && (transform.position.y > -100 && transform.position.y < 100)))
        {
            Destroy(gameObject);
        }

        playerObj = Camera.main.GetComponent<CameraFollow>().target.transform;

        if (Heath < 1 && onlyOnce)
        {
            onlyOnce = false;
            animator.SetTrigger("Dead");

            chase = false;
            if(Random.Range(0, 100) < DropRatio)
            { 
                if(Random.Range(0,100) < DropChanceClass)
                {
                    GameObject PowerUp = Instantiate(PickUp[0], transform.position, transform.rotation, PickUpContainer);
                    Destroy(PowerUp, 20);
                }
            }
            else
            {
                if (Random.Range(0, 100) < DropChanceHealth)
                {
                    GameObject HealUp = Instantiate(PickUp[1], transform.position, transform.rotation, PickUpContainer);
                    Destroy(HealUp, 20);
                }
            }

            Destroy(gameObject, 1);
        }
    }

    void FixedUpdate()
    {
        if (chase)
        {
            if (playerObj.position.x > transform.position.x)
            {
                body.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                body.transform.localScale = new Vector3(1, 1, 1);
            }

            transform.position = Vector3.MoveTowards(transform.position, playerObj.position, runSpeed * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && !Invicibale)
        {
            gameManager.maxEnemyKills++;
            Destroy(collision.gameObject);
            animator.SetTrigger("Damaged");
            Heath--;
            StartCoroutine(Invincibility());
            SoundManager.PlaySound("Enemy_hit_bullet_attack");
        }

        if (collision.gameObject.tag == "Player")
        {
            chase = false;
            StartCoroutine(WaitForFunction());
        }

        if (collision.gameObject.tag == "Melee" && !Invicibale)
        {
            gameManager.maxEnemyKills++;
            animator.SetTrigger("Damaged");
            Heath -= 2;
            StartCoroutine(Invincibility());
            SoundManager.PlaySound("Enemy_hit_root_attack");
        }

        IEnumerator WaitForFunction()
        {
            SoundManager.PlaySound("Munching_sound");
            yield return new WaitForSeconds(3);
            chase = true;
        }

        IEnumerator Invincibility()
        {
            Invicibale = true;
            yield return new WaitForSeconds(InvicibaleDuration);
            Invicibale = false;
        }
    }
}
