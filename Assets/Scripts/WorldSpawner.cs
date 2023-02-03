using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    [SerializeField]
    float range = 5;

    [SerializeField]
    int number = 10;

    public GameObject prefab;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", 0);
    }

    void Spawn()
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(prefab, new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0.0f), transform.rotation, transform);
        }
    }
}
