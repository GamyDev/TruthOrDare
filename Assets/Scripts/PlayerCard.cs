using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    public void RemoveCard()
    {
        PlayersModel.playersModel.RemoveUser(transform.GetSiblingIndex());
    }

    public void DestroyCard()
    {
        Destroy(gameObject);
    }
}
