using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    SpriteRenderer renderer;

    float horizontal;
    float vertical;

    [SerializeField]
    public float runSpeed = 5.0f;

    int maxEnemyKills;

    AudioSource audioSrc;

    public void GameOver () {
        //GameOverScreen.Setup(maxEnemyKills);
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();

        audioSrc = GetComponent<AudioSource> ();

        maxEnemyKills = FindObjectOfType<GameManager>().maxEnemyKills;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(body.velocity != Vector2.zero)
        {
            if (!audioSrc.isPlaying)
                audioSrc.Play ();
            animator.SetBool("Walk",true);
        }
        else
        {
            animator.SetBool("Walk", false);
            audioSrc.Stop ();
        }

        if(body.velocity.x > 0)
        {
            renderer.flipX= true;
        }
        else if (body.velocity.x < 0)
        {
            renderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed * Time.fixedDeltaTime, vertical * runSpeed * Time.fixedDeltaTime);
    }
}
