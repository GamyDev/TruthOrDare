using System.IO;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(QuestionsParser))]
public class QuestionsParserEditor : Editor
{
    private QuestionsParser importQuestions;

    private void OnEnable()
    {
        importQuestions = (QuestionsParser)target;    
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Update Data"))
        { 
            foreach (var item in importQuestions.QuestionData)
            {
                importQuestions.DownloadCsv(item.nameDeck, item.codeLink, OnComplete);
            }
        }
    }

    private void OnComplete(string deck, string data)
    { 
        if(string.IsNullOrEmpty(data)) { 
            Debug.LogError($"{deck} is empty or not downloaded succesfully!");
            return;
        }

        if(!Directory.Exists(Application.persistentDataPath + "/Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Data");
        }
        Debug.Log(deck);
        File.WriteAllText(Application.persistentDataPath + "/Data/" + deck + ".csv", data);
        
        DeckSettings deckSettings = Resources.Load<DeckSettings>("Decks/" + deck);
        deckSettings.questions.Clear();
        try
        {
            var questionsCsv = QuestionsParser.ReadCsv<CSVQuestionRU>(deck);
            foreach (var item in questionsCsv)
            {
                deckSettings.questions.Add(new Question()
                {
                    theme = item.theme,
                    text = item.text,
                    timer = item.timer,
                    textRU = item.textRU
                });
            }
        }
        catch (System.Exception)
        {
            var questionsCsv = QuestionsParser.ReadCsv<CSVQuestion>(deck);
            foreach (var item in questionsCsv)
            {
                deckSettings.questions.Add(new Question()
                {
                    theme = item.theme,
                    text = item.text,
                    timer = item.timer,
                });
            }
        }

        EditorUtility.SetDirty(deckSettings);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
