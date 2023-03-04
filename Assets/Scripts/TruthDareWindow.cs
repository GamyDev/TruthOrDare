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
    public Image questionNum;
    public TMP_Text questions; 

    public Button truthBtn;
    public Button dareBtn;

    public int currentQuestionNum;
    public TruthDare truthDare;

    public Player currentPlayer;

    public void ResetQuestion()
    {
        GameManager.instance.players.ResetPlayer();
        currentQuestionNum = 0;
        questionNum.fillAmount = (float)GameManager.instance.players.GetCurrentPlayer().numQuestion / (float)(GameManager.instance.questionsPerRound - 1);
        questions.text = $"{GameManager.instance.players.GetCurrentPlayer().numQuestion + 1}/{GameManager.instance.questionsPerRound}";

        GameManager.instance.questionWindow.questionNumSlider.fillAmount = (float)GameManager.instance.players.GetCurrentPlayer().numQuestion / (float)(GameManager.instance.questionsPerRound - 1);
        GameManager.instance.questionWindow.questionNumText.text = $"{GameManager.instance.players.GetCurrentPlayer().numQuestion + 1}/{GameManager.instance.questionsPerRound}";
    }

    public void NextQuestion()
    {
        currentPlayer = GameManager.instance.players.NextPlayer();
        currentQuestionNum++;
        questionNum.fillAmount = (float)GameManager.instance.players.GetCurrentPlayer().numQuestion / (float)(GameManager.instance.questionsPerRound - 1);
        questions.text = $"{GameManager.instance.players.GetCurrentPlayer().numQuestion + 1}/{GameManager.instance.questionsPerRound}";

        GameManager.instance.questionWindow.questionNumSlider.fillAmount = (float)GameManager.instance.players.GetCurrentPlayer().numQuestion / (float)(GameManager.instance.questionsPerRound - 1);
        GameManager.instance.questionWindow.questionNumText.text = $"{GameManager.instance.players.GetCurrentPlayer().numQuestion + 1}/{GameManager.instance.questionsPerRound}";
    }

    public void ShowWin()
    {
        if (currentQuestionNum == (GameManager.instance.questionsPerRound * GameManager.instance.players.players.Count))
        {
            GameManager.instance.questionWindow.gameObject.SetActive(false);
            GameManager.instance.winPanel.SetActive(true);
            GameManager.instance.truthDareWindow.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        questionNum.fillAmount = 1;

        questionNum.fillAmount = (float)GameManager.instance.players.GetCurrentPlayer().numQuestion / (float)(GameManager.instance.questionsPerRound - 1); 
        questions.text = $"{(float)GameManager.instance.players.GetCurrentPlayer().numQuestion + 1} / {GameManager.instance.questionsPerRound}";

        GameManager.instance.questionWindow.questionNumSlider.fillAmount = 1;
        GameManager.instance.questionWindow.questionNumSlider.fillAmount = (float)GameManager.instance.players.GetCurrentPlayer().numQuestion / (float)(GameManager.instance.questionsPerRound - 1);
        GameManager.instance.questionWindow.questionNumText.text = $"{(float)GameManager.instance.players.GetCurrentPlayer().numQuestion + 1} / {GameManager.instance.questionsPerRound}";

        if (GameManager.instance.gameMode == DeckType.ForFriends)
        {
            title.text = "For Friends_key";
        } else
        {
            title.text = "For Couples_key";
        }

        currentPlayer = GameManager.instance.players.GetCurrentPlayer();
        playerName.text = currentPlayer.name;
        playerAvatar.sprite = GameManager.instance.players.avatars[currentPlayer.avatar];

        truthBtn.onClick.AddListener(() =>
        {
            TruthAction();
        });

        dareBtn.onClick.AddListener(() =>
        {
            DareAction();
        });
    }

    public void TruthAction()
    {
        truthDare = TruthDare.Truth;
        GameManager.instance.questionWindow.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void DareAction()
    {
        truthDare = TruthDare.Dare;
        GameManager.instance.questionWindow.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private Player GetRandomPlayer()
    {
        var players = GameManager.instance.players.players;
        return players[Random.Range(0, players.Count)];
    }
}
