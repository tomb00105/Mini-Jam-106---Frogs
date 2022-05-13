using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject flyPrefab;

    public int flyCount = 0;

    public int maxFlies = 10;

    [SerializeField]
    float spawnGap = 0;

    [SerializeField]
    int spawnFactor = 1000;

    [SerializeField]
    float flyAngleFactor = 25f;

    private void Update()
    {
        SpawnFly();    
    }

    //Spawns flies based on time since last spawn
    void SpawnFly()
    {
        int xSpawnPos;
        //Gets a random y position for the fly to spawn.
        float ySpawnPos = Random.Range(-5f, 5f);
        //If last spawn was less than a second ago, won't spawn a fly.
        if (spawnGap <= 1)
        {
            spawnGap += Time.deltaTime;
            return;
        }
        //If less than 5 seconds since last spawn, may or may not spawn a fly this update.
        else if (spawnGap <= 5)
        {
            if (flyCount < maxFlies)
            {
                xSpawnPos = Random.Range(-1, spawnFactor);
                if (xSpawnPos > -1 && xSpawnPos < spawnFactor - 1)
                {
                    //Debug.Log("NO SPAWN");
                    spawnGap += Time.deltaTime;
                    return;
                }
                else if (xSpawnPos == -1)
                {
                    GameObject newFly = Instantiate(flyPrefab, new Vector2(-10f, ySpawnPos), Quaternion.identity);
                    newFly.transform.Rotate(0, 0, Random.Range(-flyAngleFactor, flyAngleFactor));
                    newFly.transform.parent = GameObject.Find("Flys").transform;
                    flyCount++;
                    spawnGap = 0;
                    return;
                }
                else
                {
                    GameObject newFly = Instantiate(flyPrefab, new Vector2(10f, ySpawnPos), Quaternion.identity);
                    flyCount++;
                    newFly.transform.Rotate(0, 0, Random.Range(180 - flyAngleFactor, 180 + flyAngleFactor));
                    newFly.transform.parent = GameObject.Find("Flys").transform;
                    spawnGap = 0;
                    return;
                }
            }
            else
            {
                spawnGap += Time.deltaTime;
                return;
            }
        }
        //If more than 5 seconds since last spawn, spawns a fly.
        else
        {
            if (flyCount < maxFlies)
            {
                xSpawnPos = Random.Range(0, 2);
                if (xSpawnPos == 0)
                {
                    GameObject newFly = Instantiate(flyPrefab, new Vector2(-10f, ySpawnPos), Quaternion.identity);
                    newFly.transform.Rotate(0, 0, Random.Range(-flyAngleFactor, flyAngleFactor));
                    newFly.transform.parent = GameObject.Find("Flys").transform;
                    flyCount++;
                    spawnGap = 0;
                    return;
                }
                else
                {
                    GameObject newFly = Instantiate(flyPrefab, new Vector2(10f, ySpawnPos), Quaternion.identity);
                    newFly.transform.Rotate(0, 0, Random.Range(180 - flyAngleFactor, 180 + flyAngleFactor));
                    newFly.transform.parent = GameObject.Find("Flys").transform;
                    flyCount++;
                    spawnGap = 0;
                    return;
                }
            }
            else
            {
                spawnGap += Time.deltaTime;
                return;
            }
        }
    }
}
