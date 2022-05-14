using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
    SpawnManager spawnManager;
    GameManager gameManager;
    LevelUIController levelUIController;

    public float speed = 5f;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        levelUIController = GameObject.Find("Level UI Controller").GetComponent<LevelUIController>();
    }

    private void Update()
    {
        //Moves fly forward.
        transform.position += transform.right * speed * Time.deltaTime;
        //Ensures any flies out of bounds are destroyed.
        if (transform.position.x < -11 || transform.position.x > 11)
        {
            DestroyFly(false);
        }
        if (transform.position.y < -7 || transform.position.y > 7)
        {
            DestroyFly(false);
        }
    }

    //Destroys a fly and adds points to total score if caught with tongue.
    public void DestroyFly(bool eaten)
    {
        if (eaten)
        {
            gameManager.score += 5;
            levelUIController.PointsText(gameManager.score);
            gameManager.fliesCaught++;
            //For each 5 flies the player catches, gives them one extra life if they don't already have 5 lives.
            if (gameManager.fliesCaught >= 5)
            {
                int remainder = gameManager.fliesCaught - 5;
                if (gameManager.lives < 5)
                {
                    gameManager.lives++;
                }
                gameManager.fliesCaught = 0 + remainder;
            }
        }
        spawnManager.flyCount--;
        Destroy(gameObject);
    }
}
