using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    SpawnManager spawnManager;
    GameManager gameManager;

    float speed = 5f;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        if (transform.position.x < -11 || transform.position.x > 11)
        {
            DestroyFly(false);
        }
        if (transform.position.y < -7 || transform.position.y > 7)
        {
            DestroyFly(false);
        }
    }

    public void DestroyFly(bool eaten)
    {
        if (eaten)
        {
            gameManager.score += 5;
        }
        spawnManager.flyCount--;
        Destroy(gameObject);
    }
}
