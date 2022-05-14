using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    SpawnManager spawnManager;
    GameManager gameManager;

    public float speed = 10f;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager.difficulty == 1)
        {
            speed = 10f;
        }
        else if (gameManager.difficulty == 2)
        {
            speed = 12f;
        }
        else if (gameManager.difficulty == 3)
        {
            speed = 13f;
        }
        else if (gameManager.difficulty == 4)
        {
            speed = 14f;
        }
        else if (gameManager.difficulty == 5)
        {
            speed = 15f;
        }
        else
        {
            speed = 0f;
        }
    }

    private void Update()
    {
        //Moves bird forward.
        transform.position += -transform.right * speed * Time.deltaTime;
        //Ensures any bird out of bounds are destroyed.
        if (transform.position.x < -13 || transform.position.x > 13)
        {
            DestroyBird();
        }
        if (transform.position.y < -14 || transform.position.y > 14)
        {
            DestroyBird();
        }
    }

    //Destroys a bird.
    public void DestroyBird()
    {
        spawnManager.birdCount--;
        Destroy(gameObject);
    }
}
