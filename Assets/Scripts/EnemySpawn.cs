using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    float range = 100f;

    [SerializeField]
    List<GameObject> Vegans = new List<GameObject>();

    [SerializeField]
    Transform container;

    [SerializeField]
    float SpawnRate = 0.5f;

    [SerializeField]
    int Amount = 50;

    float Timer = 0;

    int NoramlWave = 30;
    int DeathWave = 10;
    bool DWTrigger = false;
    float WaveSec = 0;
    float SpawnRateTemp = 0.5f;
    

    private void Update()
    {
        Timer += Time.deltaTime;
        WaveSec += Time.deltaTime;

        if (Timer >= SpawnRate)
        {
            Spawn();
            Timer -= SpawnRate;
        }
        
        if (!DWTrigger)
        {
            if (!((int)WaveSec <= NoramlWave)) //Normal Wave
            {
                WaveSec -= NoramlWave;
                Amount += 50;
                SpawnRate = 0.1f;
                DWTrigger= true;
            }
        }
        else
        {
            if (!((int)WaveSec <= DeathWave)) //Death Wave
            {
                WaveSec -= DeathWave;
                Amount -= 25;
                range += (range < 100)? 10: 0;
                SpawnRateTemp += (SpawnRateTemp == 0.1f)? 0.0f:-0.1f;
                DWTrigger = false;
            }
        }

        
    }

    // Update is called once per frame
    void Spawn()
    {
        //Debug.Log(container.childCount);
        if (container.childCount < Amount)
        {
            float Angle = Random.Range(0, 360);
            float Rads = Mathf.Deg2Rad * Angle;
            Vector2 Direction = new Vector2(Mathf.Cos(Rads), Mathf.Sin(Rads));
            Instantiate(Vegans[Random.Range(0, Vegans.Count)], transform.position + new Vector3(Direction.x * range, Direction.y * range, 0.0f), transform.rotation, container);
        }
    }
}
