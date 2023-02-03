using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    float range = 5;

    [SerializeField]
    private GameObject enemyObj;

    public Transform container;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, 2);
    }

    // Update is called once per frame
    void Spawn()
    {
        Instantiate(enemyObj, transform.position + new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0.0f), transform.rotation, container);
    }
}
