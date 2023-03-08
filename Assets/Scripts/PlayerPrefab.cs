using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefab : MonoBehaviour
{
    public TMP_InputField input;
    public GameObject male;
    public GameObject female;
    public Image avatar;


    private void OnEnable()
    {
        input.shouldHideMobileInput = true; 
    }

    public void SetGender(bool mail)
    {  
        GameManager.instance.players.players[transform.GetSiblingIndex()].male = mail;
        GameManager.instance.players.DisplayPlayers();
    }
}
