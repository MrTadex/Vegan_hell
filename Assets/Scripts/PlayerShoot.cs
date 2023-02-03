using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    Transform Bullets;

    [SerializeField]
    float force = 500.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 Direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position).normalized;
            GameObject obj = Instantiate(Bullet, transform.position, transform.rotation, Bullets);
            Rigidbody2D b = obj.GetComponent<Rigidbody2D>();
            b.AddRelativeForce(Direction * force, ForceMode2D.Impulse);
        }
        
    }
}
