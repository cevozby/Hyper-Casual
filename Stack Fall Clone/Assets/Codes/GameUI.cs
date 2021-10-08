using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public Image levelSlider;
    public Image currentlevel;
    public Image nextLevel;
    public Material playerMaterial;

    public GameObject settingButton;
    public GameObject soundButtons;

    public bool buttonSettingBool;

    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public bool soundOnOffButton;

    private PlayerController player;

    public GameObject homeUI, gameUI;

    public GameObject restartBttn;
    public GameObject playText, continueText, resetText;
    public bool resetClick = false;
    public int resetScene=0;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        playerMaterial = FindObjectOfType<PlayerController>().transform.GetChild(0).GetComponent<MeshRenderer>().material;

        levelSlider.transform.GetComponent<Image>().color = playerMaterial.color + Color.gray;

        levelSlider.color = playerMaterial.color;

        currentlevel.color = playerMaterial.color;

        nextLevel.color = playerMaterial.color;

        soundOnButton.GetComponent<Button>().onClick.AddListener(call: (() => SoundManager.instance.SoundOnOff()));
        soundOffButton.GetComponent<Button>().onClick.AddListener(call: (() => SoundManager.instance.SoundOnOff()));

        //Tüm lokal data prefs sil
        //PlayerPrefs.DeleteAll();
    }



    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !ignoreUI() && player.playerstate == PlayerController.PlayerState.Prepare)
        {
            player.playerstate = PlayerController.PlayerState.Playing;
            playText.SetActive(false);
            continueText.SetActive(false);
            resetText.SetActive(false);
            gameUI.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0) && ignoreUI() && player.playerstate == PlayerController.PlayerState.Playing)
        {
            player.playerstate = PlayerController.PlayerState.Prepare;
            continueText.SetActive(true);
        }

        if (resetScene==1 && resetClick == true)
        {
            resetScene = 0;
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
        else if(resetScene==2 && resetClick==false)
        {
            resetText.SetActive(false);
            resetScene = 0;
            Time.timeScale = 1;
        }

            if (SoundManager.instance.sound)
        {
            soundOnButton.SetActive(true);
            soundOffButton.SetActive(false);
        }
        else
        {
            soundOnButton.SetActive(false);
            soundOffButton.SetActive(true);
        }
        /*if (resetClick==true && Input.GetMouseButtonDown(0))
        {
            
             
            
        }*/
    }

    private bool ignoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for(int i=0; i<raycastResults.Count; i++)
        {
            if(raycastResults[i].gameObject.GetComponent<IgnoreGameUI>() != null)
            {
                raycastResults.RemoveAt(i);
                i--;
            }
        }

        return raycastResults.Count > 0;
    }

    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }

    public void settingShow()
    {
        buttonSettingBool = !buttonSettingBool;
        soundButtons.SetActive(buttonSettingBool);
    }

    public void resetGame()
    {
        resetText.SetActive(true);
        
        Time.timeScale = 0;

    }

    public void acceptButton()
    {
        resetClick = true;
        resetScene = 1;
    }

    public void refuseButton()
    {
        resetClick = false;
        resetScene = 2;
    }
}
