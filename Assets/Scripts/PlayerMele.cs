using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlayerMele: MonoBehaviour
{
    [SerializeField]
    GameObject Weapon;

    [SerializeField]
    GameObject Mark;

    GameObject obj;
    GameObject mark;

    void Start()
    {
        obj = Instantiate(Weapon, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation, transform);
        obj.GetComponent<BoxCollider2D>().enabled = false;

        mark = Instantiate(Mark, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation, transform);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 Direction = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0) - /*Camera.main.*/transform.position).normalized;
        // Fix gittering
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        mark.transform.position = (new Vector3(Direction.x, Direction.y, -1) * 1.35f) + transform.position;

        obj.transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z+1);

        if (Input.GetMouseButtonDown(0))
        {
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            obj.GetComponent<Animator>().SetTrigger("Attack");

            StartCoroutine(EnableAndDisable());
        }
    }

    private IEnumerator EnableAndDisable()
    {
        obj.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        obj.GetComponent<BoxCollider2D>().enabled = false;
        yield return null;
    }
}
