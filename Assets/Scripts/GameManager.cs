using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DecksList decksList;
    public Players players;
    public ThemesWindow themesWindow;
    public TruthDareWindow truthDareWindow;
    public QuestionWindow questionWindow;
    public RaitingWindow raitingWindow;
    public GameObject adultPopup;
    public GameObject selectModeWindow;
    public GameObject adultCheckbox;
    public LocalizationManager localizationManager;

    public DeckType gameMode;
    
    public bool isSubscribe;

    public GameObject onBoarding;
    public GameObject startPanel;

    public int questionsPerRound;
    
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeAdultCheckbox()
    {
        adultCheckbox.SetActive(!adultCheckbox.activeSelf);
        PlayerPrefs.SetInt("AdultPopup", adultCheckbox.activeSelf ? 1 : 0);
    }

    public void AdultAnswerYes()
    {
        PlayerPrefs.SetInt("AdultPopup", 1);
        selectModeWindow.SetActive(false);
        themesWindow.gameObject.SetActive(true);
        adultCheckbox.SetActive(true);
    }

    public void AdultAnswerNo()
    {
        PlayerPrefs.SetInt("AdultPopup", 0);
        selectModeWindow.SetActive(false);
        themesWindow.gameObject.SetActive(true);
        adultCheckbox.SetActive(false);
    }

    public void ShowAdultPopup()
    {
        if(!PlayerPrefs.HasKey("AdultPopup"))
        {
            adultPopup.SetActive(true);
            return;
        }

        selectModeWindow.SetActive(false);
        themesWindow.gameObject.SetActive(true);
    }

    private async void Start()
    {
        onBoarding.SetActive(!PlayerPrefs.HasKey("FirstStart"));
        startPanel.SetActive(PlayerPrefs.HasKey("FirstStart"));

        PlayerPrefs.SetInt("FirstStart", 1);

        if(PlayerPrefs.HasKey("AdultPopup") && PlayerPrefs.GetInt("AdultPopup") == 1)
        {
            adultCheckbox.SetActive(true);
        } else
        {
            adultCheckbox.SetActive(false);
        }

        if(!PlayerPrefs.HasKey("RateIt")) { 
            await raitingWindow.DisplayWindow();
        }
    }

    public void SetGameMode(int index)
    {
        gameMode = (DeckType)index;
    }
}
