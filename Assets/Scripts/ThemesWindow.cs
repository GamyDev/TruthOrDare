using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemesWindow : MonoBehaviour
{
    public TMP_Text text;
    public GameObject decksContainer;
    public string selectedTheme;
    public TruthDareWindow truthDareWindow;
    public GameObject subscriptionWindow;
    public IAPCore iAPCore;

    private void SetTitle()
    {
        if(GameManager.instance.gameMode == DeckType.ForFriends)
        {
            text.text = "For Friends_key";
        } else
        {
            text.text = "For Couples_key";
        }
    }

    public void DisplayThemes(bool updateText = true)
    {
        var decks = GetDeckSettings(GameManager.instance.gameMode);
        for (int i = 0; i < decksContainer.transform.childCount; i++)
        {
            if(decks[i].adult && (!PlayerPrefs.HasKey("AdultPopup") || PlayerPrefs.GetInt("AdultPopup") == 0))
            {
                decksContainer.transform.GetChild(i).gameObject.SetActive(false);
                continue;
            }

            decksContainer.transform.GetChild(i).gameObject.SetActive(true);
            var themeItem = decksContainer.transform.GetChild(i).GetComponent<ThemeItem>();

            if(updateText)
            {
                themeItem.title.text = decks[i].deckTitle;
            }

            themeItem.lockIcon.SetActive(!decks[i].free && !GameManager.instance.isSubscribe); 
            string title = decks[i].deckTitle;
            themeItem.icon.sprite = decks[i].icon;

            themeItem.GetComponent<Button>().onClick.RemoveAllListeners();

            if (decks[i].free) { 
                themeItem.GetComponent<Button>().onClick.AddListener(() => { 
                    selectedTheme = title;
                    GameManager.instance.questionWindow.SetQuestions(title);
                    truthDareWindow.gameObject.SetActive(true); 
                    gameObject.SetActive(false);
                    GameManager.instance.truthDareWindow.ResetQuestion();
                    GameManager.instance.players.ResetPlayer();
                });
            } else
            {
                if(GameManager.instance.isSubscribe)
                {
                    themeItem.GetComponent<Button>().onClick.AddListener(() => {
                        selectedTheme = title;
                        GameManager.instance.questionWindow.SetQuestions(title);
                        truthDareWindow.gameObject.SetActive(true);
                        gameObject.SetActive(false);
                        GameManager.instance.truthDareWindow.ResetQuestion();
                        GameManager.instance.players.ResetPlayer();
                    });
                } else
                {
                    themeItem.GetComponent<Button>().onClick.AddListener(() => {
                      //  gameObject.SetActive(false);
                        subscriptionWindow.SetActive(true);
                        iAPCore.TextInvoke();
                    });
                    
                }
            }
        }
    }

    private List<DeckSettings> GetDeckSettings(DeckType deckType)
    {
        List<DeckSettings> deckSettings = new List<DeckSettings>();

        var decks = GameManager.instance.decksList.deckSettings;
        for (int i = 0; i < decks.Length; i++)
        {
            if (decks[i].type == deckType)
            {
                deckSettings.Add(decks[i]);
            }
        }

        return deckSettings;
    }

    private void OnEnable()
    {
        SetTitle();
        DisplayThemes();
    }
}
