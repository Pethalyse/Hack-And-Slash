using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float spawnCD;
    private float spawnTime;
    public int minSpawn;
    public int maxSpawn;

    public GameObject spawnPrefab;

    public float xRound;
    public float zRound;

    static List<GameObject> enemys = new List<GameObject>();
    bool verif = false;

    void Update()
    {
        if(spawnTime <= 0)
        {
            spawnTime = spawnCD;
            for(int i=0; i<Random.Range(minSpawn, maxSpawn); i++)
            {
                int trying = 20;
                do
                {
                    Vector3 v = new Vector3(Random.Range(transform.position.x - xRound, transform.position.x + xRound), 0, Random.Range(transform.position.z - zRound, transform.position.z + zRound));
                    foreach (GameObject go in enemys)
                    {
                        if (go != null)
                        {
                            if (go.transform.position == v)
                            {
                                trying--;
                                verif = true;
                                break;
                            }
                        }
                    }

                    if(enemys.Count == 0 || !verif) { enemys.Add(Instantiate(spawnPrefab, v, Quaternion.identity)); break; }

                } while (trying > 0);
                
            }
        }
        spawnTime -= Time.deltaTime;

        foreach(GameObject go in new List<GameObject>(enemys))
        {
            if(go == null)
            {
                enemys.Remove(go);
            }
        }
    }
}
