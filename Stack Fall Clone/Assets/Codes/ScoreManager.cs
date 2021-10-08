using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager intance;

    public int score;
    public static int totalScore;
    public Text scoreTXT;
    private PlayerController player;

    private void Awake()
    {
        makeSingleton();
        scoreTXT = GameObject.Find("ScoreText").GetComponent<Text>();
        //maxScoreTXT = GameObject.Find("MaxScoreText").GetComponent<Text>();
    }



    void Start()
    {
        addScore(0);
        //scoreTXT = GameObject.Find("ScoreText").GetComponent<Text>();
        //gameOverScoreText = GameObject.Find("GameOverScoreText").GetComponent<Text>();
        //maxScoreTXT = GameObject.Find("MaxScoreText").GetComponent<Text>();
    }



    void Update()
    {
        if (scoreTXT == null)
        {
            scoreTXT = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        //scoreTXT.text = score.ToString();
        
        //maxScoreTXT.text = PlayerPrefs.GetInt("HighScore").ToString();
        
            
        
    }

    public void addScore(int value)
    {
        Debug.Log("Value = " + value);
        score += value;
        Debug.Log("Score = " + score);
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            
            PlayerPrefs.SetInt("HighScore", score);
        }
        scoreTXT.text = score.ToString();
        totalScore = score;
    }

    void makeSingleton()
    {
        if (intance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            intance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void ResetScore()
    {
        score = 0;
    }
}
