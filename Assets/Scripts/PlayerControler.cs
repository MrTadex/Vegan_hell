using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    SpriteRenderer spriteRenderer;

    float horizontal;
    float vertical;

    [SerializeField]
    public float runSpeed = 5.0f;

    AudioSource audioSrc;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource> ();
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
}
