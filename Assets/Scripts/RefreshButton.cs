using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RefreshButton : MonoBehaviour
{
    [SerializeField] private QuestionsParser parser;
    [SerializeField] private TextMeshProUGUI testText;


    public void UpdateQuestions()
    {
        GetComponent<Button>().interactable = false;
        foreach (var item in parser.QuestionData)
        {
            parser.DownloadCsv(item.nameDeck, item.codeLink, OnComplete);
        }
    }

    private void OnComplete(string deck, string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            Debug.LogError($"{deck} is empty or not downloaded succesfully!");
            return;
        }


        if(!Directory.Exists(Application.persistentDataPath + "/Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Data");
        }

        File.WriteAllText(Application.persistentDataPath + "/Data/" + deck + ".csv", data);

        Debug.Log($"{deck} downloaded succesfully! {QuestionsParser.ReadCsv(deck).Count} questions loaded!");

        GetComponent<Button>().interactable = true;

        testText.text = $"Loaded {QuestionsParser.ReadCsv("Deck-1").Count.ToString()} questions!";

}
}
