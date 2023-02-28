using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLanguage : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.localizationManager.SetLanguage(PlayerPrefs.GetInt("Lang"));

        if(transform.GetSiblingIndex() == PlayerPrefs.GetInt("Lang"))
            GetComponent<Toggle>().isOn = true;
    }

    public void ChangeLanguage()
    {
        GameManager.instance.localizationManager.SetLanguage(transform.GetSiblingIndex());
        PlayerPrefs.SetInt("Lang", transform.GetSiblingIndex());
    }
}
