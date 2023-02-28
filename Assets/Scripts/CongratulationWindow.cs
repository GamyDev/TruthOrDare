using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CongratulationWindow : MonoBehaviour
{
    public TMP_Text title;

    private void OnEnable()
    {
        if (GameManager.instance.gameMode == DeckType.ForFriends)
        {
            title.text = "For Friends_key";
        }
        else
        {
            title.text = "For Couples_key";
        }
    }
}
