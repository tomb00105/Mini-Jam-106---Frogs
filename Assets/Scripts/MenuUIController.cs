using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    [SerializeField]
    DataManager dataManager;

    [SerializeField]
    Text difficultyText;

    [SerializeField]
    Slider difficultySlider;

    [SerializeField]
    Text highScoreText;

    public int highscore;

    public int newDifficulty = 1;
    
    private void Start()
    {
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
        newDifficulty = 1;
        dataManager.newDifficulty = 1;
        dataManager.highscore = highscore = PlayerPrefs.GetInt("highscore", highscore);
        highScoreText.text = "High Score: " + highscore.ToString();
    }

    private void Update()
    {
        highScoreText.text = "High Score: " + dataManager.highscore.ToString();
    }

    public void DifficultyChange()
    {
        newDifficulty = (int)difficultySlider.value;
        dataManager.newDifficulty = newDifficulty;
        difficultyText.text = "Difficulty: " + newDifficulty;
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
