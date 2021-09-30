using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
            homeUI.SetActive(false);
            gameUI.SetActive(true);


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
}
