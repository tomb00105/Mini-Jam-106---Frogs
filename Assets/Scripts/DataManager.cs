using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int newDifficulty;

    public int highscore;
    
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Data Manager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", highscore);
    }
}
