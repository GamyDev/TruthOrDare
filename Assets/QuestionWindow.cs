using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionWindow : MonoBehaviour
{
    public List<Question> truthQuestions;
    public List<Question> dareQuestions;

    public Question currentQuestion;

    public TMP_Text title;
    public TMP_Text questionText;
    public Image avatar;
    public TMP_Text playerName;

    public GameObject timerPanel;
    public GameObject simplePanel;

    public Slider timerSlider;
    public TMP_Text maxTime;
    public TMP_Text currentTime;
    public Button playTimer;

    public int timerValue;
    public int currentTimer;

    public Image questionNumSlider;
    public TMP_Text questionNumText;
    
    public void SetQuestions(string title, bool refreshTruth = true, bool refreshDare = true)
    {
        truthQuestions.Clear();
        dareQuestions.Clear();

        var decks = GameManager.instance.decksList.deckSettings;
        for (int i = 0; i < decks.Length; i++)
        {
            if(decks[i].deckTitle == title)
            {
                for (int j = 0; j < decks[i].questions.Count; j++)
                {
                    if(decks[i].questions[j].theme == "Truth" && refreshTruth) {
                        truthQuestions.Add(new Question() { 
                            text = decks[i].questions[j].text, 
                            theme = decks[i].questions[j].theme,
                            timer = decks[i].questions[j].timer
                        });
                    } 
                    if(decks[i].questions[j].theme == "Dare" && refreshDare)
                    {
                        dareQuestions.Add(new Question()
                        {
                            text = decks[i].questions[j].text,
                            theme = decks[i].questions[j].theme,
                            timer = decks[i].questions[j].timer
                        });
                    }
                }
            }
        }
    }

    public void GetRandomQuestion(ref List<Question> questions)
    {
        int index = Random.Range(0, questions.Count);
        var truthDare = GameManager.instance.truthDareWindow.truthDare;

        while(questions[index].theme != truthDare.ToString().Trim()) {
            index = Random.Range(0, questions.Count);
        }

        currentQuestion = new Question() { 
            text = questions[index].text,
            theme = questions[index].theme,
            timer = questions[index].timer
        };

        questions.RemoveAt(index);

        TryRefreshQuestions();
    }

    public void TryRefreshQuestions()
    {
        if(truthQuestions.Count == 0)
            SetQuestions(GameManager.instance.themesWindow.selectedTheme, true, false);

        if(dareQuestions.Count == 0)
            SetQuestions(GameManager.instance.themesWindow.selectedTheme, false, true);
    }



    private void OnEnable()
    {
        var truthDare = GameManager.instance.truthDareWindow.truthDare;

        if (truthDare == TruthDare.Dare)
        {
            GetRandomQuestion(ref dareQuestions);
        }
        if (truthDare == TruthDare.Truth)
        {
            GetRandomQuestion(ref truthQuestions);
        }

        playerName.text = GameManager.instance.truthDareWindow.currentPlayer.name;
        avatar.sprite = GameManager.instance.players.avatars[GameManager.instance.truthDareWindow.currentPlayer.avatar];

        questionText.text = currentQuestion.text;

        title.text = GameManager.instance.gameMode == DeckType.ForFriends ? "For Friends" : "For Couples";

        timerPanel.SetActive(false);
        simplePanel.SetActive(true);

        if (!string.IsNullOrEmpty(currentQuestion.timer))
        {
            timerPanel.SetActive(true);
            simplePanel.SetActive(false);

            timerValue = int.Parse(currentQuestion.timer);
            timerSlider.maxValue = timerValue;
            timerSlider.value = 0;
            maxTime.text = FormatTimer(timerValue);
            currentTime.text = FormatTimer(0);
            playTimer.onClick.AddListener(async () =>
            {
                playTimer.gameObject.SetActive(false);
                await StartTimer();
            });
        }
    }
     

    private async UniTask StartTimer()
    {
        while(currentTimer < timerValue)
        {
            await UniTask.Delay(1000);
            currentTimer++;
            currentTime.text = FormatTimer(currentTimer);
            timerSlider.value = currentTimer;
        }
    }

    public string FormatTimer(int seconds)
    {
        int fullMinutes = 0;
        int fullSeconds = seconds;

        if (seconds > 59)
        {
            fullSeconds = seconds % 60;
            fullMinutes = Mathf.FloorToInt(seconds / 60);
        }

        string secs = fullSeconds < 10 ? "0" + fullSeconds.ToString() : fullSeconds.ToString();
        string mins = fullMinutes < 10 ? "0" + fullMinutes.ToString() : fullMinutes.ToString();

        return $"{mins}:{secs}";
    }
}
