using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;

    public static ScoreManager instance;

    public TMP_Text scoreText;
    public TMP_Text highScore;

    int score =100;
    int highscore = 90;   //initial value for high score 


    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0); //Playerprefs is a class that stores Player preferences between game sessions. 
        //It can store string, float and integer values into the user’s platform registry.

        scoreText.text = "Score: " + score.ToString() ;
        highScore.text = "High Score:" + highscore.ToString();
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        isGameOver = false;
    }

    public void MinusPoints()    //reverse scoring system
    {
        score -=10;  //subtract ten points for every obstacle hit
        scoreText.text = "Score: " + score.ToString() ;

        if(highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void ReplayGame()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
