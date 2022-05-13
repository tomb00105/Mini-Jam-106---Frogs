using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    GameObject player;

    public GameObject lilypadPrefab;

    public Transform lastLilypad;

    public List<GameObject> lilypads = new List<GameObject>();

    public int difficulty = 1;

    [Range(0, 5)]
    public int lives = 2;

    public bool isAlive = true;

    public int HP = 5;

    public float totalTime = 60;

    public int score = 0;

    private void Start()
    {
        SpawnLevel();
    }

    private void Update()
    {
        totalTime -= Time.deltaTime;
        if (totalTime <= 0)
        {
            GameOver();
        }
    }

    //Handles logic if the player finishes their jump on water.
    public void HitWater()
    {
        lives -= 1;
        if (lives <= 0)
        {
            isAlive = false;
            lives = 0;
            Destroy(player);
            //Splash effects
            GameOver();
        }
        else
        {
            //Splash effects
            player.transform.position = lastLilypad.position;
        }
    }

    //Handles logic if player hits a fence.
    public void HitFence()
    {
        lives -= 1;
        if (lives <= 0)
        {
            isAlive = false;
            lives = 0;
            Destroy(player);
            //Fence hit effects
            GameOver();
        }
        else
        {
            //Fence hit effects
            player.transform.position = lastLilypad.position;
        }
    }

    //Spawns lilypads at random locations and then randomly chooses a starting lilypad for the player.
    void SpawnLevel()
    {
        //Spawns fewer lilypads the higher the difficulty.
        for (int i = 0; i < 12 - difficulty; i++)
        {
            bool locationFound = false;
            //Takes random locations within bounds and checks that it isn't too close to an existing lilypad.
            while (!locationFound)
            {
                Vector2 possibleLocation = new Vector2(Random.Range(-4.5f, 4.5f), Random.Range(-4.5f, 4.5f));

                //Instantiates first lilypad.
                if (lilypads.Count == 0)
                {
                    GameObject newLilypad = Instantiate(lilypadPrefab, possibleLocation, Quaternion.identity);
                    newLilypad.transform.parent = GameObject.Find("Lilypads").transform;
                    lilypads.Add(newLilypad);
                    locationFound = true;
                }
                else
                {
                    for (int j = 0; j < lilypads.Count; j++)
                    {
                        //If too close to an existing lilypad, breaks and tries another random position.
                        if (Vector2.Distance(lilypads[j].transform.position, possibleLocation) <= 1.4f)
                        {
                            break;
                        }
                        //If the loop reaches the last lilypad and it isn't too close, instantiates a lilypad at the location and adds it to the list of lilypads.
                        else if (lilypads[j] == lilypads[lilypads.Count - 1])
                        {
                            GameObject newLilypad = Instantiate(lilypadPrefab, possibleLocation, Quaternion.identity);
                            newLilypad.transform.parent = GameObject.Find("Lilypads").transform;
                            lilypads.Add(newLilypad);
                            locationFound = true;
                        }
                    }
                }
            }
        }
        //Chooses a random starting lilypad and instantiates the player there.
        GameObject startLilypad = lilypads[Random.Range(0, lilypads.Count)];
        Instantiate(playerPrefab, startLilypad.transform.position, Quaternion.identity);
        player = GameObject.Find("Player(Clone)");
    }


    //Triggered when player lives reaches 0.
    void GameOver()
    {
        Time.timeScale = 0;
    }
}
