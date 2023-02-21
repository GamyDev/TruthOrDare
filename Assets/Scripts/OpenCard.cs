using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class OpenCard : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayersModel playersModel;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject imageCard;
    [SerializeField] private GameObject playerChoice; 
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerTwo;
    [SerializeField] private GameObject playerAll;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private GameObject buttonTimer;
    [SerializeField] private GameObject buttonDone;

    private int currentSeconds;

    public void AnimateOpenCard()
    {
        imageCard.transform.eulerAngles = new Vector3(0, 180, 0);
        title.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        playerChoice.SetActive(false);

        imageCard.transform.DORotate(new Vector3(0, 0, 0), 2).SetEase(Ease.OutElastic).OnUpdate(FlipAnimCallback);
    }

    public void SetCurrentPlayer(int index)
    {
        if (GameManager.currentQuestion.players == "1")
        {
            GameManager.currentPlayer.Add(gameManager.tempPlayers[index]);
        }

        if (GameManager.currentQuestion.players == "2")
        {
            GameManager.currentPlayer.Add(gameManager.tempPlayers[index * 2]);
            if (index * 2 + 1 >= gameManager.tempPlayers.Count)
            {
                GameManager.currentPlayer.Add(gameManager.tempPlayers[0]);
            }
            else
            {
                GameManager.currentPlayer.Add(gameManager.tempPlayers[index * 2 + 1]);
            }
        }
    }

    private void OnEnable()
    {
        AnimateOpenCard();
        
        if(!string.IsNullOrEmpty(GameManager.currentQuestion.timer)) {
            currentSeconds = int.Parse(GameManager.currentQuestion.timer);
            buttonTimer.SetActive(true);
            buttonDone.SetActive(false);
            timer.text = currentSeconds.ToString();
            timer.transform.GetChild(0).GetComponent<TMP_Text>().text = currentSeconds.ToString();
        } else
        {
            buttonDone.SetActive(true);
            buttonTimer.SetActive(false);
        }

        
        title.text = GameManager.currentQuestion.name;
        string question = GameManager.currentQuestion.text;

        if (GameManager.currentQuestion.players == "1") 
        {  
            if (GameManager.currentPlayerIndex != GameManager.previousPlayerIndex)
            { 
                question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
            } else
            {
                SetCurrentPlayer(GameManager.currentPlayerIndex);
                question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
            }
        }

        if(GameManager.currentQuestion.players == "2")
        {
            if (GameManager.currentPlayer2Index != GameManager.previousPlayer2Index)
            {
                question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
                question = question.Replace("[player2]", $"<color=#DA2678>{GameManager.currentPlayer[1].name}</color>");
            } else
            {
                SetCurrentPlayer(GameManager.currentPlayer2Index);
                question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
                question = question.Replace("[player2]", $"<color=#DA2678>{GameManager.currentPlayer[1].name}</color>");
            }
        }

        text.text = question;

        if(GameManager.currentQuestion.players == "A")
        {
            playerAll.SetActive(true);
            player.SetActive(false);
            playerTwo.SetActive(false);
        } 
        
        if(GameManager.currentQuestion.players == "1")
        {
            playerAll.SetActive(false);
            player.SetActive(true);
            playerTwo.SetActive(false);
            if (GameManager.currentPlayerIndex != GameManager.previousPlayerIndex)
            {
                player.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;
                player.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;

                player.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[0].avatar];
            } else
            {
                Debug.LogError("Bug!!!!!");
                SetCurrentPlayer(GameManager.currentPlayerIndex);
                player.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;
                player.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;

                player.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[0].avatar];
            }
        }

        if (GameManager.currentQuestion.players == "2")
        {
            playerAll.SetActive(false);
            player.SetActive(true);
            playerTwo.SetActive(true);

            if (GameManager.currentPlayer2Index != GameManager.previousPlayer2Index)
            {
                player.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;
                player.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;

                player.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[0].avatar];

                playerTwo.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[1].name;
                playerTwo.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[1].name;

                playerTwo.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[1].avatar];
            } else
            {
                Debug.LogError("Bug!!!!!");
                SetCurrentPlayer(GameManager.currentPlayer2Index);
                player.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;
                player.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;

                player.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[0].avatar];

                playerTwo.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[1].name;
                playerTwo.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[1].name;

                playerTwo.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[1].avatar];
            }
        }
    }

    private void FlipAnimCallback()
    {
        if (imageCard.transform.localEulerAngles.y >= 90 && imageCard.transform.localEulerAngles.y <= 270) {
            title.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            playerChoice.SetActive(false);
        } else
        {
            title.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            playerChoice.SetActive(true);
        }
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        buttonTimer.GetComponent<Button>().interactable = false;

        while (currentSeconds > 0) { 
            yield return new WaitForSeconds(1f);
            currentSeconds--;
            timer.text = currentSeconds.ToString();
            timer.transform.GetChild(0).GetComponent<TMP_Text>().text = currentSeconds.ToString();
        }

        buttonTimer.GetComponent<Button>().interactable = true;
        buttonTimer.SetActive(false);
        buttonDone.SetActive(true);
        yield return null;
    }
}
