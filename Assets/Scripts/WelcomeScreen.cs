using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField] private PlayersModel _playersModel;
    [SerializeField] private Image _avatar;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _subName;
    [SerializeField] private TMP_Text _titleName;
    [SerializeField] private TMP_Text _subTitleName;
    [SerializeField] private PlayersList _playersList;
    [SerializeField] private StartPlayersScreen _startPlayersScreen;


    public void SetUser()
    {
        _name.text = _playersModel.GetLastUser().name;
        _subName.GetComponent<TMP_Text>().text = _playersModel.GetLastUser().name;
        _titleName.text = $"Welcome,  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
        _subTitleName.GetComponent<TMP_Text>().text = $"Welcome,  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
        _avatar.sprite = _playersModel.avatars[_playersModel.GetLastUser().avatar];

        _playersList.AddPlayers();
        _playersList.ChangePosition();
        _startPlayersScreen.RefreshUsers();
    }
}
