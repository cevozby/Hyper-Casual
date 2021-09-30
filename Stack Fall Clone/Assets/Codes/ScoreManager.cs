using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager intance;

    public int score;
    public Text scoreTXT;

    private void Awake()
    {
        makeSingleton();
        scoreTXT = GameObject.Find("ScoreText").GetComponent<Text>();
    }



    void Start()
    {
        addScore(0);
    }



    void Update()
    {
        if (scoreTXT == null)
        {
            scoreTXT = GameObject.Find("ScoreText").GetComponent<Text>();
        }
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
