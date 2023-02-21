using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using DanielLochner.Assets.SimpleScrollSnap;
using TMPro;
using Cysharp.Threading.Tasks;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayersModel playersModel;
    [SerializeField] private Decks decks;
    [SerializeField] private string typeDeck;
    [SerializeField] private Button buttonSpin;
    [SerializeField] private GameObject spinner;
    [SerializeField] private GameObject centerCard;
    [SerializeField] private OpenCard openCard;
    [SerializeField] private GameObject rightCard;
    [SerializeField] private GameObject playerRoulette;
    [SerializeField] private GameObject playerTwoRoulette;

    [SerializeField] private TMP_Text titleDeck;
    [SerializeField] private TMP_Text titleDeckOutline;

    [SerializeField] private TMP_Text descriptionDeck;
    [SerializeField] private TMP_Text descriptionDeckOutline;

    [SerializeField] private Image deckImage;
    [SerializeField] private Image deckMiniImage;

    [SerializeField] private SimpleScrollSnap simpleScrollSnap;
    [SerializeField] private SimpleScrollSnap simpleScrollSnap2;
    [SerializeField] private StartPlayersScreen playersScreen;

    private static Action OnComplete;

    public static bool reloadGame;

    private Vector3 centerCardPos;

    public static List<Player> currentPlayer;
    public static Question currentQuestion;

    private List<Question> questions;
    public List<Player> tempPlayers;

    private ScrollRect scrollRect;
    private ScrollRect scrollRect2;
    private Sequence deckSequence;

    public static int previousPlayerIndex;
    public static int currentPlayerIndex;

    public static int previousPlayer2Index;
    public static int currentPlayer2Index;

    public void EnableSpinner()
    {

        simpleScrollSnap.gameObject.SetActive(false);
        simpleScrollSnap.gameObject.SetActive(true);

        simpleScrollSnap2.gameObject.SetActive(false);
        simpleScrollSnap2.gameObject.SetActive(true);

        if (currentQuestion.players == "1")
        {
            centerCard.SetActive(true);
            simpleScrollSnap.gameObject.SetActive(true);
            simpleScrollSnap2.gameObject.SetActive(false);
            buttonSpin.gameObject.SetActive(true);
        }

        if (currentQuestion.players == "2")
        {
            centerCard.SetActive(true);
            simpleScrollSnap2.gameObject.SetActive(true);
            simpleScrollSnap.gameObject.SetActive(false);
            buttonSpin.gameObject.SetActive(true);
        }

        if (currentQuestion.players == "A")
        {
            centerCard.SetActive(true);
            simpleScrollSnap.gameObject.SetActive(false);
            simpleScrollSnap2.gameObject.SetActive(false);
            buttonSpin.gameObject.SetActive(false);
        }
    }

    public void BackToDecks()
    {
        reloadGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetSpinners()
    {

        scrollRect = simpleScrollSnap.GetComponent<ScrollRect>();
        scrollRect2 = simpleScrollSnap2.GetComponent<ScrollRect>();
        for (int i = 0; i < scrollRect2.content.transform.childCount; i++)
        {
            simpleScrollSnap2.Remove(i);
        }


        for (int i = 0; i < scrollRect.content.transform.childCount; i++)
        {
            simpleScrollSnap.Remove(i);
        }
    }

    public void SetSettingsTwoUsers()
    {
        simpleScrollSnap2.InfiniteScrollingSpacing = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < playersModel.playerDatas.Count; j++)
            {
                tempPlayers.Add(playersModel.playerDatas[j]);
            }
        }


        for (int i = 0; i < Mathf.CeilToInt(tempPlayers.Count / 2); i++)
        {
            simpleScrollSnap2.AddToBack(playerTwoRoulette);
        }

        for (int i = 0; i < tempPlayers.Count; i++)
        {
            if (i % 2 == 0)
            {
                scrollRect2.content.GetChild(i / 2).GetChild(0).GetComponent<Image>().sprite = playersModel.avatars[tempPlayers[i].avatar];
                scrollRect2.content.GetChild(i / 2).GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i].name;
                scrollRect2.content.GetChild(i / 2).GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i].name;

                if (i + 1 < tempPlayers.Count)
                {
                    scrollRect2.content.GetChild(i / 2).GetChild(1).GetComponent<Image>().sprite = playersModel.avatars[tempPlayers[i + 1].avatar];
                    scrollRect2.content.GetChild(i / 2).GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i + 1].name;
                    scrollRect2.content.GetChild(i / 2).GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i + 1].name;
                }
            }

        }

        simpleScrollSnap2.OnPanelCentered += SelectedPlayer;
    }



    public void SetSettingsOneUser()
    {

        simpleScrollSnap.InfiniteScrollingSpacing = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < playersModel.playerDatas.Count; j++)
            {
                tempPlayers.Add(playersModel.playerDatas[j]);
            }
        }


        for (int i = 0; i < tempPlayers.Count; i++)
        {
            simpleScrollSnap.AddToBack(playerRoulette);
        }

        for (int i = 0; i < tempPlayers.Count; i++)
        {
            scrollRect.content.GetChild(i).GetComponent<Image>().sprite = playersModel.avatars[tempPlayers[i].avatar];
            scrollRect.content.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i].name;
            scrollRect.content.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i].name;
        }

        simpleScrollSnap.OnPanelCentered += SelectedPlayer;
    }

    private void OnDisable()
    {
        simpleScrollSnap.OnPanelCentered -= SelectedPlayer;
        simpleScrollSnap2.OnPanelCentered -= SelectedPlayer;
    }

    public void AnimDeck()
    {
        deckSequence = DOTween.Sequence();

        centerCard.transform.localPosition = rightCard.transform.localPosition + new Vector3(0, 0, 1);
        centerCard.transform.localScale = Vector3.zero;

        deckSequence.Append(centerCard.transform.DOScale(Vector3.one * 1.5f, 1).OnComplete(ScaleComplete));
    }



    private void OnEnable()
    {
        currentPlayer = new List<Player>();
        tempPlayers = new List<Player>();

        scrollRect = simpleScrollSnap.GetComponent<ScrollRect>();
        scrollRect2 = simpleScrollSnap2.GetComponent<ScrollRect>();

        centerCardPos = centerCard.transform.localPosition;

        questions = new List<Question>();
        questions.Clear();
        questions = GetQustions();

        GetRandomQuestion();

        AnimDeck();

        SetSettingsOneUser();

        SetSettingsTwoUsers();

        EnableSpinner();
    }

    private void ScaleComplete()
    {
        deckSequence.Append(centerCard.transform.DOLocalMove(centerCardPos, 1));
        deckSequence.Append(centerCard.transform.DOScale(Vector3.one, 1));
        Invoke("EnableAllAnimation", 1.5f);
    }

    public void EnableAllAnimation()
    {
        if (currentQuestion.players == "A")
        {
            centerCard.SetActive(false);
            simpleScrollSnap.gameObject.SetActive(false);
            simpleScrollSnap2.gameObject.SetActive(false);
            buttonSpin.gameObject.SetActive(false);
            openCard.gameObject.SetActive(true);
            openCard.GetComponent<OpenCard>().AnimateOpenCard();
        }
    }


    private void SelectedPlayer(int index, int index2)
    {
        scrollRect.StopMovement();
        scrollRect2.StopMovement();

        Debug.Log($"Players count {currentQuestion.players}. Selected {index}");

        if (currentQuestion.players == "1")
        {
            currentPlayer.Add(tempPlayers[index]);

            currentPlayerIndex = index;
        }

        if (currentQuestion.players == "2")
        {
            currentPlayer2Index = index;

            currentPlayer.Add(tempPlayers[index * 2]);
            if (index * 2 + 1 >= tempPlayers.Count)
            {
                currentPlayer.Add(tempPlayers[0]);
            }
            else
            {
                currentPlayer.Add(tempPlayers[index * 2 + 1]);
            }
        }
    }

    public void DisableButton()
    {
        StartCoroutine(StartSpin());
    }

    IEnumerator StartSpin()
    {
        currentPlayer.Clear();
        if (currentQuestion.players == "1")
        {
            currentPlayerIndex = previousPlayerIndex;
        }

        if (currentQuestion.players == "2")
        {
            currentPlayer2Index = previousPlayer2Index;
        }


        buttonSpin.interactable = false;
        buttonSpin.transform.DOScale(Vector3.zero, 0.2f);
        yield return new WaitForSeconds(5f);
        buttonSpin.interactable = true;
        buttonSpin.transform.DOScale(Vector3.one, 0.2f);

        buttonSpin.gameObject.SetActive(false);
        spinner.gameObject.SetActive(false);
        simpleScrollSnap2.gameObject.SetActive(false);
        centerCard.gameObject.SetActive(false);

        openCard.gameObject.SetActive(false);
        openCard.gameObject.SetActive(true);
    }

    public void GetRandomQuestion()
    {
        if (questions.Count == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        int randomQuestion = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[randomQuestion];
        Debug.Log($"{questions[randomQuestion].text} - {questions[randomQuestion].players}");
        questions.RemoveAt(randomQuestion);
        Debug.Log($"{currentQuestion.text} - {currentQuestion.players}");
    }

    public List<Question> GetQustions()
    {
        List<Question> questions = new List<Question>();

        typeDeck = "ScriptableObject";

        deckImage.sprite = decks.deckSettings[SelectedDeck.selectedDeck].icon;
        deckMiniImage.sprite = decks.deckSettings[SelectedDeck.selectedDeck].icon;

        titleDeck.text = decks.deckSettings[SelectedDeck.selectedDeck].deckTitle;
        titleDeckOutline.text = decks.deckSettings[SelectedDeck.selectedDeck].deckTitle;

        descriptionDeck.text = decks.deckSettings[SelectedDeck.selectedDeck].deckDescription;
        descriptionDeckOutline.text = decks.deckSettings[SelectedDeck.selectedDeck].deckDescription;

        foreach (var item in decks.deckSettings[SelectedDeck.selectedDeck].questions)
        {
            questions.Add(item);
        }

        return questions;
    }
}
