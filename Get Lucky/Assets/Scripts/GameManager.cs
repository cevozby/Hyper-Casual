using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource heart, coin, blackHeart;

    float heartLevel = 20f, maxHeartLevel = 100f, coinValue = 0f, jumpingValue;
    public float speed = 3f, jumpSpeed, jumpTime;
    public Slider slider, jumpingSlider;

    public GameObject gameOver, finishPlane, jumping;

    bool jump = false, coinMultipleCheck = false, finishCheck=false;

    public static bool finish = false;

    Rigidbody playerRB;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        slider.maxValue = maxHeartLevel;
        slider.value = heartLevel;
        jumpingSlider.maxValue = 10f;
        jumpingSlider.value = 0f;
    }

    void Update()
    {
        GameObject.Find("CoinValueText").GetComponent<Text>().text = "Coin: " + coinValue.ToString();
        
        if (slider.value <= 0f)
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }

        if (Time.timeScale == 0 && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
            
        }

        FinishPlane();

        JumpingPart();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish" && finishCheck == false)
        {
            finish = true;
            finishCheck = true;
            transform.position = new Vector3(25.5f, transform.position.y, 0f);
            finishPlane.SetActive(true);
        }
        else if (collision.gameObject.tag == "3x Plane" || collision.gameObject.tag == "4x Plane" || collision.gameObject.tag == "5x Plane")
        {
            
            jump = false;
            Time.timeScale = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Heart")
        {
            slider.value += 10f;
            Destroy(other.gameObject);
            heart.Play(0);

        }
        else if (other.gameObject.tag == "Coin")
        {
            coinValue++;
            Destroy(other.gameObject);
            coin.Play(0);
        }
        else if (other.gameObject.tag == "BlackHeart")
        {
            slider.value -= 10f;
            Destroy(other.gameObject);
            blackHeart.Play(0);
        }
    }

    void FinishPlane()
    {
        if (finish == true && !jump)
        {
            jumping.SetActive(true);
            float jumpValue = Mathf.Abs(Mathf.Sin(Time.time * speed)) * 10;
            jumpingSlider.value = jumpValue;
            jumpingValue = jumpValue;
            if (Input.GetMouseButtonDown(0))
            {
                jump = true;
                jumpValue = jumpingValue;
                jumpingSlider.value = jumpingValue;
                speed = 0;
            }
        }
    }

    void JumpingPart()
    {
        if (finish == true && jump == true && jumpTime <= 180f)
        {
            finishPlane.SetActive(false);
            if (jumpingSlider.value <= 2f || jumpingSlider.value > 8f && jumpingValue <= 10f)
            {
                if (!coinMultipleCheck)
                {
                    coinValue *= 3;
                    coinMultipleCheck = true;
                }
                transform.position = Vector3.Lerp(transform.position, new Vector3(27.5f, Mathf.Sin(jumpTime) * 3f, 0f), jumpSpeed);


            }
            else if (jumpingSlider.value > 2f && jumpingValue <= 4f || jumpingSlider.value > 6f && jumpingValue <= 8f)
            {
                if (!coinMultipleCheck)
                {
                    coinValue *= 4;
                    coinMultipleCheck = true;
                }
                transform.position = Vector3.Lerp(transform.position, new Vector3(30.5f, Mathf.Sin(jumpTime) * 3f, 0f), jumpSpeed);

            }
            else if (jumpingSlider.value > 4f && jumpingValue <= 6f)
            {
                if (!coinMultipleCheck)
                {
                    coinValue *= 5;
                    coinMultipleCheck = true;
                }
                transform.position = Vector3.Lerp(transform.position, new Vector3(33.5f, Mathf.Sin(jumpTime) * 3f, 0f), jumpSpeed);
            }
            jumpTime += Time.time / 20f;
        }
    }
}
