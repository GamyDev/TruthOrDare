using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class LocalizationManager : MonoBehaviour
{
    [SerializeField] private GameObject engIcon;
    [SerializeField] private GameObject rusIcon;


    public static int SelectedLanguage
    {
        get => PlayerPrefs.GetInt("SelectedLanguage", 0);
        set => PlayerPrefs.SetInt("SelectedLanguage", value);
    }

    public static event LanguageChangeHandler OnLanguageChange;
    public delegate void LanguageChangeHandler();


    private static Dictionary<string, List<string>> localization;

    [SerializeField] private TextAsset textFile;

    private void Awake()
    {
        if (localization == null)
            LoadLocalization();
        CheckIcon();

    }

    public void SetLanguage(int id)
    {
        SelectedLanguage = id;
        

        OnLanguageChange?.Invoke();
        CheckIcon();
    }

    void CheckIcon()
    {
        if (SelectedLanguage == 0)
        {
            engIcon.SetActive(true);
            rusIcon.SetActive(false);
        }

        if (SelectedLanguage == 1)
        {
            engIcon.SetActive(false);
            rusIcon.SetActive(true);
        }
    }

    private void LoadLocalization()
    {
        localization = new Dictionary<string, List<string>>();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textFile.text);

        foreach (XmlNode key in xmlDocument["Keys"]. ChildNodes)
        {
            string keyStr = key.Attributes["Name"].Value;

            var values = new List<string>();
            foreach (XmlNode translate in key["Translates"].ChildNodes)
                values.Add(translate.InnerText);
            localization[keyStr] = values;
        }
    }

    public static string GetTranslate(string key, int languageId =-1)
    {
        if (languageId == -1)
            languageId = SelectedLanguage;

        if (localization.ContainsKey(key))
            return localization[key][languageId];
    
        return key;
    }
}
