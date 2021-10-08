using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    bool carpma;

    float currentTime;

    bool invincible;

    public GameObject fireShield;

    [SerializeField]
    AudioClip win, death, idestory, destory, bounce;

    public int currentObstacleNumber;
    public int totalObstacleNumber;

    public Image InvictableSlider;
    public GameObject InvictableOBJ;

    public GameObject finishUI, gameOverUI;
    private bool totalObstacleNumberCheck;

    public Text gameOverScoreText, maxScoreText;

    public enum PlayerState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }

    [HideInInspector]
    public PlayerState playerstate = PlayerState.Prepare;

    void Start()
    {
        //totalObstacleNumber = LevelSpawner.tON;
        totalObstacleNumberCheck = true;

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentObstacleNumber = 0;
        
    }



    void Update()
    {
        //totalObstacleNumber = LevelSpawner.tON;
        if (totalObstacleNumberCheck == true)
        {
            totalObstacleNumber = FindObjectsOfType<ObstacleController>().Length;
            totalObstacleNumberCheck = false;
        }
        
        if (playerstate == PlayerState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                carpma = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                carpma = false;
            }

            if (invincible)
            {
                currentTime -= Time.deltaTime * 0.35f;
                if (!fireShield.activeInHierarchy)
                {
                    fireShield.SetActive(true);
                }
            }
            else
            {
                if (fireShield.activeInHierarchy)
                {
                    fireShield.SetActive(false);
                }

                if (carpma)
                {
                    currentTime += Time.deltaTime * 0.8f;
                }
                else
                {
                    currentTime -= Time.deltaTime * 0.5f;
                }
            }
            if(currentTime>=0.15f || InvictableSlider.color == Color.red)
            {
                InvictableOBJ.SetActive(true);
            }
            else
            {
                InvictableOBJ.SetActive(false);
            }

            if (currentTime >= 1)
            {
                currentTime = 1;
                invincible = true;
                InvictableSlider.color = Color.red;
            }
            else if (currentTime <= 0)
            {
                currentTime = 0;
                invincible = false;
                InvictableSlider.color = Color.white;
            }

            if (InvictableOBJ.activeInHierarchy)
            {
                InvictableSlider.fillAmount = currentTime / 1;
            }
        }

        
        if (playerstate == PlayerState.Finish)
        {
            FindObjectOfType<LevelSpawner>().NextLevel();
        }
        if (playerstate == PlayerState.Died)
        {
            gameOverScoreText.text = "Score: " + ScoreManager.totalScore.ToString();
            maxScoreText.text = "Max Score: " + PlayerPrefs.GetInt("HighScore").ToString();
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().SameLevel();
                RotateManager.speed = 100f;
                ScoreManager.totalScore = 0;
            }
            
        }
        
    }


    private void FixedUpdate()
    {
        if (playerstate == PlayerState.Playing)
        {
        if (carpma == true)
            {
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (carpma == false)
        {
            rb.velocity = new Vector3(0, 250 * Time.deltaTime, 0);
        }else
        {
            if (invincible)
            {
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane")
                {
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    shatterObstacles();
                    SoundManager.instance.playSoundFX(idestory, 0.5f);
                    currentObstacleNumber++;
                }
            }
            else
            {
                if (collision.gameObject.tag == "enemy")
                {
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    shatterObstacles();
                    SoundManager.instance.playSoundFX(destory, 0.5f);
                    currentObstacleNumber++;
                }
                else if (collision.gameObject.tag == "plane")
                {
                    gameOverUI.SetActive(true);
                    playerstate = PlayerState.Died;
                    RotateManager.speed = 0f;
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    ScoreManager.intance.ResetScore();
                    SoundManager.instance.playSoundFX(death, 0.5f);
                }
            }
            
        }

        FindObjectOfType<GameUI>().LevelSliderFill(currentObstacleNumber / (float)totalObstacleNumber);

        if(collision.gameObject.tag=="Finish" && playerstate == PlayerState.Playing)
        {
            playerstate = PlayerState.Finish;
            SoundManager.instance.playSoundFX(win, 0.5f);
            finishUI.SetActive(true);
            finishUI.transform.GetChild(0).GetComponent<Text>().text = "Level" + PlayerPrefs.GetInt(key: "Level", defaultValue: 1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (carpma == false || collision.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 250 * Time.deltaTime, 0);
            SoundManager.instance.playSoundFX(bounce, 0.5f);
        }
    }

    public void shatterObstacles()
    {
        if (invincible)
        {
            ScoreManager.intance.addScore(1);
        }
        else
        {
            ScoreManager.intance.addScore(1);
        }
    }
}
