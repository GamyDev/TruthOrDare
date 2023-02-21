using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


public class QuestionsParser : MonoBehaviour
{
    [SerializeField] private CSVQuestionData[] questionData;


    public CSVQuestionData[] QuestionData => questionData;

    public void DownloadCsv(string fileName, string url, Action<string, string> OnComplete)
    {
        StartCoroutine(RequestCsvData(fileName, url, OnComplete)); 
    }

    private IEnumerator RequestCsvData(string fileName, string code, Action<string, string> OnComplete)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(GetDownloadLink(code)))
        {
            yield return webRequest.SendWebRequest();
             
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Can`t download data!");
            } 

            OnComplete?.Invoke(fileName, webRequest.downloadHandler.text);
        }
    }

    private string GetDownloadLink(string codeLink)
    {
        return "https://docs.google.com/spreadsheets/d/" + codeLink + "/export?format=csv";
    }

    public static List<CSVQuestion> ReadCsv(string deck)
    {
        if(!File.Exists(Application.persistentDataPath + "/Data/" + deck + ".csv"))
        {
            return new List<CSVQuestion>();
        }

        List<CSVQuestion> questions = new List<CSVQuestion>();

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };

        using (var reader = new StreamReader(Application.persistentDataPath + "/Data/" + deck + ".csv"))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Read();
            while (csv.Read())
            {
                var record = csv.GetRecord<CSVQuestion>();
                questions.Add(record);
            }
        }

        return questions;
    }
}


[System.Serializable]
public class CSVQuestionData
{
    public string nameDeck;
    public string codeLink;
}

[System.Serializable]
public class CSVQuestion
{ 
    [Index(0)]
    public string text { get; set; }
    [Index(1)]
    public string count { get; set; }
    [Index(2)]
    public string timer { get; set; }
}

 


