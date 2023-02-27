using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaitingWindow : MonoBehaviour
{
    public GameObject starContainer;
    public string url;
    public int currentRaiting;
    public int timer;

    private void OnEnable()
    {
        RaitingStar.selectedStar += RaitingStar_selectedStar;
    }

    private void OnDisable()
    {
        RaitingStar.selectedStar -= RaitingStar_selectedStar;
    }

    public void Rate()
    {
        if (currentRaiting > 3)
        {
            Application.OpenURL(url);
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("RateIt", 1);
        }
        else
        {
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("RateIt", 1);
        }
    }

    private void RaitingStar_selectedStar(int index)
    {
        currentRaiting = index + 1;

        for (int i = 0; i < starContainer.transform.childCount; i++)
        {
            if(i <= index)
            {
                starContainer.transform.GetChild(i).GetComponent<RaitingStar>().activeStar.SetActive(true);
                starContainer.transform.GetChild(i).GetComponent<RaitingStar>().disableStar.SetActive(false);
            } else
            {
                starContainer.transform.GetChild(i).GetComponent<RaitingStar>().activeStar.SetActive(false);
                starContainer.transform.GetChild(i).GetComponent<RaitingStar>().disableStar.SetActive(true);
            }
        }
    }

    public async UniTask DisplayWindow()
    {
        await UniTask.Delay(timer);
        gameObject.SetActive(true);
    }

    public async void Display()
    {
        await DisplayWindow();
    }
}
