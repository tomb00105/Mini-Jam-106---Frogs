using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject flyPrefab;

    [SerializeField]
    GameObject birdPrefab;

    public int flyCount = 0;

    public int maxFlies = 10;

    public int birdCount = 0;

    public int maxBirds = 5;

    [SerializeField]
    float flySpawnGap = 0;

    public float minFlySpawnTime = 1f;

    public float maxFlySpawnTime = 5f;
    
    public int flySpawnFactor = 1000;

    [SerializeField]
    float birdSpawnGap = 0;

    public float minBirdSpawnTime = 1f;

    public float maxBirdSpawnTime = 6f;

    public int birdSpawnFactor = 1000;

    [SerializeField]
    float flyAngleFactor = 25f;

    private void Update()
    {
        SpawnFly();
        SpawnBird();
    }

    //Spawns flies based on time since last spawn
    void SpawnFly()
    {
        int xSpawnPos;
        //Gets a random y position for the fly to spawn.
        float ySpawnPos = Random.Range(-5f, 5f);
        //If last spawn was less than a second ago, won't spawn a fly.
        if (flySpawnGap <= minFlySpawnTime)
        {
            flySpawnGap += Time.deltaTime;
            return;
        }
        //If less than 5 seconds since last spawn, may or may not spawn a fly this update.
        else if (flySpawnGap <= maxFlySpawnTime)
        {
            if (flyCount < maxFlies)
            {
                xSpawnPos = Random.Range(-1, flySpawnFactor);
                if (xSpawnPos > -1 && xSpawnPos < flySpawnFactor - 1)
                {
                    //Debug.Log("NO SPAWN");
                    flySpawnGap += Time.deltaTime;
                    return;
                }
                else if (xSpawnPos == -1)
                {
                    GameObject newFly = Instantiate(flyPrefab, new Vector2(-10f, ySpawnPos), Quaternion.identity);
                    newFly.transform.Rotate(0, 0, Random.Range(-flyAngleFactor, flyAngleFactor));
                    newFly.transform.parent = GameObject.Find("Flys").transform;
                    flyCount++;
                    flySpawnGap = 0;
                    return;
                }
                else
                {
                    GameObject newFly = Instantiate(flyPrefab, new Vector2(10f, ySpawnPos), Quaternion.identity);
                    flyCount++;
                    newFly.transform.Rotate(0, 0, Random.Range(180 - flyAngleFactor, 180 + flyAngleFactor));
                    newFly.transform.parent = GameObject.Find("Flys").transform;
                    flySpawnGap = 0;
                    return;
                }
            }
            else
            {
                flySpawnGap += Time.deltaTime;
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
                    flySpawnGap = 0;
                    return;
                }
                else
                {
                    GameObject newFly = Instantiate(flyPrefab, new Vector2(10f, ySpawnPos), Quaternion.identity);
                    newFly.transform.Rotate(0, 0, Random.Range(180 - flyAngleFactor, 180 + flyAngleFactor));
                    newFly.transform.parent = GameObject.Find("Flys").transform;
                    flyCount++;
                    flySpawnGap = 0;
                    return;
                }
            }
            else
            {
                flySpawnGap += Time.deltaTime;
                return;
            }
        }
    }

    //Spawns a bird at left, right, top or bottom of level.
    void SpawnBird()
    {
        int spawnChoice = Random.Range(1, birdSpawnFactor);
        Vector2 spawnPos;

        if (birdSpawnGap <= minBirdSpawnTime)
        {
            birdSpawnGap += Time.deltaTime;
            return;
        }
        else if (birdSpawnGap <= maxBirdSpawnTime)
        {
            if (birdCount < maxBirds)
            {
                if (spawnChoice == 1)
                {
                    spawnPos = new Vector2(-11f, Random.Range(-5f, 5f));
                    GameObject newBird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);
                    newBird.transform.Rotate(0f, 0f, 180f);
                    newBird.transform.parent = GameObject.Find("Birds").transform;
                    birdCount++;
                    birdSpawnGap = 0;
                    return;
                }
                else if (spawnChoice == 2)
                {
                    spawnPos = new Vector2(11f, Random.Range(-5f, 5f));
                    GameObject newBird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);
                    newBird.transform.parent = GameObject.Find("Birds").transform;
                    birdCount++;
                    birdSpawnGap = 0;
                    return;
                }
                else if (spawnChoice == 3)
                {
                    spawnPos = new Vector2(Random.Range(-4.5f, 4.5f), 13f);
                    GameObject newBird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);
                    newBird.transform.Rotate(0f, 0f, 90f);
                    newBird.transform.parent = GameObject.Find("Birds").transform;
                    birdCount++;
                    birdSpawnGap = 0;
                    return;
                }
                else if (spawnChoice == 4)
                {
                    spawnPos = new Vector2(Random.Range(-4.5f, 4.5f), -13f);
                    GameObject newBird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);
                    newBird.transform.Rotate(0f, 0f, -90f);
                    newBird.transform.parent = GameObject.Find("Birds").transform;
                    birdCount++;
                    birdSpawnGap = 0;
                    return;
                }
                else
                {
                    birdSpawnGap += Time.deltaTime;
                }
            }
            else
            {
                birdSpawnGap += Time.deltaTime;
                return;
            }
        }
        else
        {
            if (birdCount < maxBirds)
            {
                spawnChoice = Random.Range(1, 5);
                if (spawnChoice == 1)
                {
                    spawnPos = new Vector2(-11f, Random.Range(-5f, 5f));
                    GameObject newBird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);
                    newBird.transform.Rotate(0f, 0f, 180f);
                    newBird.transform.parent = GameObject.Find("Birds").transform;
                    birdCount++;
                    birdSpawnGap = 0;
                    return;
                }
                else if (spawnChoice == 2)
                {
                    spawnPos = new Vector2(11f, Random.Range(-5f, 5f));
                    GameObject newBird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);
                    newBird.transform.parent = GameObject.Find("Birds").transform;
                    birdCount++;
                    birdSpawnGap = 0;
                    return;
                }
                else if (spawnChoice == 3)
                {
                    spawnPos = new Vector2(Random.Range(-4.5f, 4.5f), 13f);
                    GameObject newBird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);
                    newBird.transform.Rotate(0f, 0f, 90f);
                    newBird.transform.parent = GameObject.Find("Birds").transform;
                    birdCount++;
                    birdSpawnGap = 0;
                    return;
                }
                else if (spawnChoice == 4)
                {
                    spawnPos = new Vector2(Random.Range(-4.5f, 4.5f), -13f);
                    GameObject newBird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);
                    newBird.transform.Rotate(0f, 0f, -90f);
                    newBird.transform.parent = GameObject.Find("Birds").transform;
                    birdCount++;
                    birdSpawnGap = 0;
                    return;
                }
                else
                {
                    birdSpawnGap += Time.deltaTime;
                }
            }
        }
    }
}
