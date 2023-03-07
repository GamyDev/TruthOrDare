using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(TextMeshProUGUI))]

public class LocalizedText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private string key;
    public bool dynamicText;
    

    private async void OnEnable()
    {
        if(dynamicText)
            await Localize();
    }


    private async void Start()
    {
        if (dynamicText)
            await Localize();
        else
            LocalizeText();

        LocalizationManager.OnLanguageChange += OnLanguageChange;
    }

    private void OnDestroy()
    {
        LocalizationManager.OnLanguageChange -= OnLanguageChange;
    }

    private void OnLanguageChange()
    {
        LocalizeText();
    }

    private void Init()
    {
        text = GetComponent<TextMeshProUGUI>();
        if(text.text.Contains("_key"))
            key = text.text;
    }

    public void LocalizeText()
    {
        if (text == null)
            Init();
        text.text = LocalizationManager.GetTranslate(key);
    }

    public async UniTask Localize(string newKey = null)
    {
        await UniTask.Delay(10);
        Init();

        if (newKey != null)
            key = newKey;

        text.text = LocalizationManager.GetTranslate(key);
    }
}
