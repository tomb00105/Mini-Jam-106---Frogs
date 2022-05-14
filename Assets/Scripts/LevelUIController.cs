using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelUIController : MonoBehaviour
{
    [SerializeField]
    DataManager dataManager;
    
    int highscore;

    [SerializeField]
    GameObject gameUI;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    Text timeText;

    [SerializeField]
    Text pointsText;

    [SerializeField]
    Text livesText;

    [SerializeField]
    Text hpText;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text highScoreText;

    private void Start()
    {
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
    }

    public void TimeText(float time)
    {
        timeText.text = "Time: " + Mathf.CeilToInt(time).ToString();
    }

    public void PointsText(int points)
    {
        pointsText.text = "Points: " + points.ToString();
    }

    public void LivesText(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void HPText(int HP)
    {
        hpText.text = "HP: " + HP;
    }

    public void GameOver(int score)
    {
        scoreText.text = "Score: " + score.ToString();
        if (score > PlayerPrefs.GetInt("highscore", highscore))
        {
            PlayerPrefs.SetInt("highscore", score);
            highScoreText.text = "High Score: " + score.ToString();
            dataManager.highscore = score;
        }
        else
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore", highscore).ToString();
        }
        gameUI.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        dataManager.newDifficulty = 1;
        SceneManager.LoadScene(0);
    }
}
