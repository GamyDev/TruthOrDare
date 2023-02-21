using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlayersModel : MonoBehaviour
{
    public List<Player> playerDatas;
    public List<Sprite> avatars;

    public static Action<List<Player>> OnPlayersAdd;
    public static Action<List<Player>> OnPlayersRemove;

    public static PlayersModel playersModel;

    private void Awake()
    {
        playersModel = this;
        playerDatas = new List<Player>();
    }

    public void AddNewPlayer(Player player)
    {
        if (player == null)
            throw new System.ArgumentNullException();

        player.avatar = UnityEngine.Random.Range(0, avatars.Count);
        playerDatas.Add(player);

        OnPlayersAdd?.Invoke(playerDatas); 
    }

    public Player GetLastUser()
    {
        if(playerDatas.Count > 0)
            return playerDatas[playerDatas.Count - 1];

        return null;
    }

    public void RemoveUser(int index)
    {
        //playerDatas.RemoveAt(index);
        playerDatas.RemoveAt(playerDatas.Count - 1 - index);
        OnPlayersRemove?.Invoke(playerDatas);
    }
}


[System.Serializable]
public class Player
{
    public string name;
    public int avatar;
}