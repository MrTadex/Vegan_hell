using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    [SerializeField]
    private GameObject playerObj = null;

    [SerializeField]
    public float runSpeed = 5.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        // find player by script
        if (playerObj == null)
            playerObj = FindObjectOfType<PlayerControler>().gameObject;
    }

    void Update()
    {
        Debug.Log("Player Position: X = " + playerObj.transform.position.x + " --- Y = " + playerObj.transform.position.y + " --- Z = " + 
        playerObj.transform.position.z);

        // go to player
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed * Time.fixedDeltaTime, vertical * runSpeed * Time.fixedDeltaTime);
    }
}
