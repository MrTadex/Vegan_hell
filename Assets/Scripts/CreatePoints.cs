using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using static Unity.VisualScripting.Member;

public class CreatePoints : MonoBehaviour
{
    [SerializeField]
    GameObject Prefab;

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    Transform Bullets;

    [SerializeField]
    Transform Points;

    static int numberOfPoints = 8;

    [SerializeField]
    float offSet = 0;

    [SerializeField]
    float offSetSpeed = 1.0f;

    [SerializeField]
    float force = 10.0f;

    [SerializeField]
    float setTimer = 2.5f;

    float timer = 0;

    bool canFire = true;

    public Vector2[] points = new Vector2[numberOfPoints];

    float OneStep;


    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < numberOfPoints; i++)
        {
            Instantiate(Prefab, transform.position, transform.rotation, Points);
        }
    }

    // Update is called once per frame
    void Update()
    {
        OneStep = Mathf.Deg2Rad * ((360 / numberOfPoints));

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = new Vector2(Mathf.Cos((i * OneStep) + offSet), Mathf.Sin((i * OneStep) + offSet));

            Points.GetChild(i).transform.position = (points[i] + (Vector2)transform.position);

            Vector2 facing = points[i];

            float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
            Points.GetChild(i).transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (Input.GetKey(KeyCode.Q))
        {
            offSet += offSetSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
            offSet -= offSetSpeed * Time.deltaTime;
        }

        if (!canFire)
        {
            if(timer < setTimer)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                canFire = true;
            }
        }

        if (Input.GetKey(KeyCode.Space) && canFire)
        {
            SoundManager.PlaySound("Bullet_shoot");

            foreach (Transform point in Points)
            {
                point.GetComponent<Animator>().SetTrigger("Shoot");
            }

            canFire = false;
            for (int i = 0; i < numberOfPoints; i++)
            {
                Vector2 Direction = ((points[i] + (Vector2)transform.position) - (Vector2)transform.position).normalized;
                GameObject obj = Instantiate(Bullet, points[i] + (Vector2)transform.position, transform.rotation, Bullets);
                Rigidbody2D b = obj.GetComponent<Rigidbody2D>();
                b.AddForce(Direction * force /*+ this.GetComponent<Rigidbody2D>().velocity*/, ForceMode2D.Impulse);

                float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                Destroy(obj, 10);
            }
        }
    }
}
