using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    [SerializeField]
    float xRange = 100;

    [SerializeField]
    float yRange = 100;

    [SerializeField]
    float interpolatMargin = 0.5f;

    [SerializeField]
    float randomMinRange = -1;

    [SerializeField]
    float randomMaxRange = 1;

    [SerializeField]
    int shift = 100;

    [SerializeField]
    Transform Player;

    [SerializeField]
    public GameObject prefab;

    [SerializeField]
    public GameObject bound;

    // Start is called before the first frame update
    void Start()
    {
        SetCoordiant(new Vector2(0, 0));
        SpawnBound();
    }

    void SetCoordiant(Vector2 Coordinat)
    {
        Vector2 Start = Coordinat + new Vector2(-xRange, -yRange);
        Vector2 End = Coordinat + new Vector2(xRange, yRange);

        Vector2 CurPoz = Start;
        while (CurPoz.y != End.y) // Row
        {
            while (CurPoz.x != End.x) // Col
            {

                float Per = Mathf.PerlinNoise(CurPoz.x + Random.Range(-shift, shift), CurPoz.y + Random.Range(-shift, shift));

                if(Per > 0.9)
                {
                    float PozX = CurPoz.x + Random.Range(randomMinRange, randomMaxRange);
                    float PozY = CurPoz.y + Random.Range(randomMinRange, randomMaxRange);

                    if((PozX > - 99 && PozX < 99) && (PozY > -99 && PozY < 99))
                        Instantiate(prefab, new Vector3(PozX, PozY, 0.0f), transform.rotation, transform);
                }
                else
                {
                    //Debug.Log(Per);
                }
                
                CurPoz.x += interpolatMargin;
            }
            CurPoz.x = Start.x;
            CurPoz.y += interpolatMargin;
        }
    }

    void SpawnBound()
    {
        var box1 = Instantiate(bound, new Vector3(xRange, 0, 0), transform.rotation, transform);
        box1.transform.rotation = Quaternion.Euler(0, 0, 90f);
        box1.GetComponent<SpriteRenderer>().size = new Vector2(200.3f, 0.32f);
        box1.GetComponent<BoxCollider2D>().size = new Vector2(200.3f, 0.32f);

        var box2 = Instantiate(bound, new Vector3(-xRange, 0, 0), transform.rotation, transform);
        box2.transform.rotation = Quaternion.Euler(0, 0, -90f);
        box2.GetComponent<SpriteRenderer>().size = new Vector2(200.3f, 0.32f);
        box2.GetComponent<BoxCollider2D>().size = new Vector2(200.3f, 0.32f);

        var box3 = Instantiate(bound, new Vector3(0, yRange, 0), transform.rotation, transform);
        box3.transform.rotation = Quaternion.Euler(0, 0, -180f);
        box3.GetComponent<SpriteRenderer>().size = new Vector2(200.3f, 0.32f);
        box3.GetComponent<BoxCollider2D>().size = new Vector2(200.3f, 0.32f);

        var box4 = Instantiate(bound, new Vector3(0, -yRange, 0), transform.rotation, transform);
        box4.GetComponent<SpriteRenderer>().size = new Vector2(200.3f, 0.32f);
        box4.GetComponent<BoxCollider2D>().size = new Vector2(200.3f, 0.32f);
    }
}
