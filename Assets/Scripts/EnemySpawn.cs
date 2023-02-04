using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    float range = 1;
    float buffer = 20;
    int maxEnemies = 50;

    [SerializeField]
    private GameObject enemyObj;

    [SerializeField]
    List<GameObject> Vegans = new List<GameObject>();

    public Transform container;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, 0.2f);
    }

    // Update is called once per frame
    void Spawn()
    {
        //Debug.Log(container.childCount);
        if (container.childCount < maxEnemies)
            Instantiate(Vegans[Random.Range(0, Vegans.Count)], transform.position + new Vector3(Random.Range(-range, range) * buffer, Random.Range(-range, range) * buffer, 0.0f), transform.rotation, container);
    }
}
