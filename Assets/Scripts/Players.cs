using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Players : MonoBehaviour
{
    public List<Player> players;
    public GameObject container;
    public GameObject playerPrefab;
    public VerticalLayoutGroup verticalLayoutGroup;
    public Scrollbar scrollbar;
    public GameObject nextButton;

    public Sprite maleActiveIcon;
    public Sprite femaleActiveIcon;

    public Sprite maleIcon;
    public Sprite femaleIcon;

    public int currentPlayerIndex;

    public Sprite[] avatars;

    private void Start()
    {
        players = new List<Player>();
        Init();
        SpawnPlayers();
    }

    public void ResetPlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].numQuestion = 0;
        }
    }

    public Player NextPlayer()
    {
        int nextIndex = currentPlayerIndex + 1;
        players[currentPlayerIndex].numQuestion++;
        if(nextIndex > players.Count - 1)
            nextIndex = 0;

        currentPlayerIndex = nextIndex;
        return players[currentPlayerIndex];
    }

    public Player GetCurrentPlayer()
    {
        return players[currentPlayerIndex];
    }

    public void DisplayPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            var player = container.transform.GetChild(i).GetComponent<PlayerPrefab>();
            player.GetComponent<PlayerPrefab>().input.text = players[i].name;
            SetGender(player.GetComponent<PlayerPrefab>(), players[i].male);
        }
    }

    private Sprite GetAvatar(out int index)
    {
        index = 0;

        int avatar = Random.Range(0, avatars.Length);
        while (AvatarExist(avatar))
        {
            avatar = Random.Range(0, avatars.Length);
        }
        index = avatar;
        return avatars[avatar];
    }

    private bool AvatarExist(int avatar)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if(avatar == players[i].avatar)
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            var player = Instantiate(playerPrefab);
            player.transform.SetParent(container.transform);
            player.transform.localScale = Vector3.one;
            player.GetComponent<PlayerPrefab>().input.text = players[i].name;
            SetGender(player.GetComponent<PlayerPrefab>(), players[i].male);
            player.GetComponent<PlayerPrefab>().avatar.sprite = GetAvatar(out int index);
            players[i].avatar = index;
        }
    }

    public void SetGender(PlayerPrefab prefab, bool male)
    {
        prefab.male.GetComponent<Image>().sprite = male ? maleActiveIcon : maleIcon;
        prefab.female.GetComponent<Image>().sprite = !male ? femaleActiveIcon : femaleIcon;
    }

    public void RenamePlayer(int index, string name)
    {
        players[index].name = !string.IsNullOrEmpty(name) ? name : "Player";
    }

    public void RemovePlayer(int index)
    {
        if (players.Count == 0) return;

        players.RemoveAt(index);
        nextButton.SetActive(players.Count > 1);
    }

    public void AddPlayer()
    {
        if (players.Count > 7) return;

        players.Add(new Player()
        {
            name = $"Player",
            male = true
        });
        var player = Instantiate(playerPrefab);
        player.transform.SetParent(container.transform);
        player.transform.localScale = Vector3.one;
        player.GetComponent<PlayerPrefab>().input.text = players[players.Count - 1].name;
        nextButton.SetActive(players.Count > 1);
        SetGender(player.GetComponent<PlayerPrefab>(), true);
        player.GetComponent<PlayerPrefab>().avatar.sprite = GetAvatar(out int index);
        players[players.Count - 1].avatar = index;
    }

    private void Init()
    {
        players.Add(new Player()
        {
            name = "Player",
            male = true
        });

        players.Add(new Player()
        {
            name = "Player",
            male = true
        });

        players.Add(new Player()
        {
            name = "Player",
            male = true
        });
    }
}
[System.Serializable]
public class Player
{
    public string name;
    public bool male;
    public int avatar = -1;
    public int numQuestion;
}
