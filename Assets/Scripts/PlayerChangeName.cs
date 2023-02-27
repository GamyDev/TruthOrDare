using TMPro;
using UnityEngine;

public class PlayerChangeName : MonoBehaviour
{
    public GameObject player;

   public void SetName(string value)
   {
        var input = GetComponent<TMP_InputField>(); 

        input.text = value;
        GameManager.instance.players.RenamePlayer(player.transform.GetSiblingIndex(), value);
   }
     
}
