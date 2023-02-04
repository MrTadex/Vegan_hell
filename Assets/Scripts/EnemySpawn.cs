using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    int Clock;

    [SerializeField]
    float range = 1;
    float buffer = 20;

    [SerializeField]
    List<GameObject> Vegans = new List<GameObject>();

    [SerializeField]
    Transform container;

    [SerializeField]
    float SpawnRate = 0.5f;

    [SerializeField]
    int Amount = 50;

    float Timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Clock = (int)FindObjectOfType<GameManagement>().Clock;
    }
    private void Update()
    {
        //*InvokeRepeating("Spawn", 0, SpawnRate);

        Timer += Time.deltaTime;

        if(Timer >= SpawnRate)
        {
            Spawn();
            Timer -= SpawnRate;
        }

        switch (Clock)
        {
            case > 150: // Death Wave
                Amount = 300;
                SpawnRate = 0.1f;
                break;
            case > 130 and <= 150:
                Amount = 150;
                SpawnRate = 0.3f;
                break;
            case > 120 and <= 130: // 3. Wave
                Amount = 175;
                SpawnRate = 0.1f;
                break;
            case > 80 and <= 120:
                Amount = 100;
                SpawnRate = 0.3f;
                break;
            case > 70 and <= 80: // 2. Wave
                Amount = 125;
                SpawnRate = 0.1f;
                break;
            case > 40 and <= 70:
                Amount = 75;
                SpawnRate = 0.4f;
                break;
            case > 30 and <= 40: // 1. Wave
                SpawnRate = 0.1f;
                Amount = 100;
                Debug.Log("2");
                break;
            case <= 30:
                SpawnRate = 0.5f;
                Amount = 50;
                Debug.Log("1");
                break;
        }
    }

    // Update is called once per frame
    void Spawn()
    {
        //Debug.Log(container.childCount);
        if (container.childCount < Amount)
            Instantiate(Vegans[Random.Range(0, Vegans.Count)], transform.position + new Vector3(Random.Range(-range, range) * buffer, Random.Range(-range, range) * buffer, 0.0f), transform.rotation, container);
    }
}
