using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    SpriteRenderer spriteRenderer;
    GameManagement gameManager;

    float horizontal;
    float vertical;

    bool Invicibale = false;

    [SerializeField]
    float InvicibaleDuration = 1.0f;

    [SerializeField]
    public float runSpeed = 5.0f;

    //player sound
    AudioSource audioSrc;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource> ();
        gameManager = FindObjectOfType<GameManagement>();
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
            spriteRenderer.flipX= true;
        }
        else if (body.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed * Time.fixedDeltaTime, vertical * runSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fire" && !Invicibale)
        {
            gameManager.Health--;
            animator.SetTrigger("Damaged");

            StartCoroutine(Invincibility());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy" && !Invicibale)
        {
            gameManager.Health--;
            animator.SetTrigger("Damaged");

            StartCoroutine(Invincibility());
        }
    }

    private IEnumerator Invincibility()
    {
        Invicibale = true;
        yield return new WaitForSeconds(InvicibaleDuration);
        Invicibale = false;
    }
}
