using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TruthDare
{
    Truth,
    Dare
}

public class TruthDareWindow : MonoBehaviour
{
    public TMP_Text title;
    public Image playerAvatar;
    public TMP_Text playerName;
    public Slider questionNum;
    public TMP_Text questions;
    public GameObject winWindow;

    public Button truthBtn;
    public Button dareBtn;

    public int currentQuestionNum;
    public TruthDare truthDare;

    public Player currentPlayer;

    public void ResetQuestion()
    {
        currentQuestionNum = 0;
        questionNum.value = currentQuestionNum;
        questions.text = $"{currentQuestionNum + 1}/{GameManager.instance.questionsPerRound}";

        GameManager.instance.questionWindow.questionNumSlider.value = currentQuestionNum;
        GameManager.instance.questionWindow.questionNumText.text = $"{currentQuestionNum + 1}/{GameManager.instance.questionsPerRound}";
    }

    public void NextQuestion()
    {
        currentQuestionNum++;
        questionNum.value = currentQuestionNum;
        questions.text = $"{currentQuestionNum + 1}/{GameManager.instance.questionsPerRound}";

        GameManager.instance.questionWindow.questionNumSlider.value = currentQuestionNum;
        GameManager.instance.questionWindow.questionNumText.text = $"{currentQuestionNum + 1}/{GameManager.instance.questionsPerRound}";

        if (currentQuestionNum == GameManager.instance.questionsPerRound - 1)
        {
            gameObject.SetActive(false);
            GameManager.instance.questionWindow.gameObject.SetActive(false);
            winWindow.SetActive(true);
        }
    }

    private void OnEnable()
    {
        questionNum.maxValue = GameManager.instance.questionsPerRound - 1;

        questionNum.value = currentQuestionNum; 
        questions.text = $"{currentQuestionNum + 1}/{GameManager.instance.questionsPerRound}";

        GameManager.instance.questionWindow.questionNumSlider.maxValue = GameManager.instance.questionsPerRound - 1;
        GameManager.instance.questionWindow.questionNumSlider.value = currentQuestionNum;
        GameManager.instance.questionWindow.questionNumText.text = $"{currentQuestionNum + 1}/{GameManager.instance.questionsPerRound}";

        if (GameManager.instance.gameMode == DeckType.ForFriends)
        {
            title.text = "For Friends";
        } else
        {
            title.text = "For Couples";
        }

        currentPlayer = GetRandomPlayer();
        playerName.text = currentPlayer.name;
        playerAvatar.sprite = GameManager.instance.players.avatars[currentPlayer.avatar];

        truthBtn.onClick.AddListener(() =>
        {
            truthDare = TruthDare.Truth;
            GameManager.instance.questionWindow.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });

        dareBtn.onClick.AddListener(() =>
        {
            truthDare = TruthDare.Dare;
            GameManager.instance.questionWindow.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
    }

    private Player GetRandomPlayer()
    {
        var players = GameManager.instance.players.players;
        return players[Random.Range(0, players.Count)];
    }
}
