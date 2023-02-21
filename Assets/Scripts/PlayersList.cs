using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersList : MonoBehaviour
{
    [SerializeField] private GameObject[] _playersList;
     private int _countPlayers = 0;
    [SerializeField] private GameObject _addPlayersObject;
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _moveObject;
    [SerializeField] private GameObject _topTextObject;
    [SerializeField] private GameObject _topBlueObject;
    [SerializeField] private Transform[] _positionContent;
    [SerializeField] private Transform[] _positionMoveObject;
    [SerializeField] private float _screenHeight;
    [SerializeField] private float _distanceScroll;
    [SerializeField] private ScrollRect _scrollRect;


    // [SerializeField] private GameObject _distanceScroll;


    public void AddPlayers()
    {
        _countPlayers++;
        //ChangePosition();
    }

    private void Start()
    {
        _content.transform.position = new Vector3(_positionContent[0].transform.position.x, _positionContent[0].transform.position.y, _positionContent[0].transform.position.z);

        _screenHeight = Screen.height;

        if (_screenHeight >= 2532 && _screenHeight < 2960)
        {
            _distanceScroll = 0.9126536f;
        }

        if (_screenHeight >= 2436 && _screenHeight < 2532)
        {
            _distanceScroll = 0.9027101f;
        }

        if (_screenHeight >= 2340 && _screenHeight < 2436)
        {
            _distanceScroll = 0.9086636f; //
        }

        if (_screenHeight >= 2160 && _screenHeight < 2340)
        {
            _distanceScroll = 0.5328956f; //
        }

        if (_screenHeight >= 1920 && _screenHeight < 2160)
        {
            _distanceScroll = 0.6948383f; //
        }

        if (_screenHeight >= 1792 && _screenHeight < 1920)
        {
            _distanceScroll = 0.8960785f; //
        }

        if (_screenHeight >= 1334 && _screenHeight < 1792)
        {
            _distanceScroll = 0.6730404f; //
        }

        if (_screenHeight >= 1136 && _screenHeight < 1334)
        {
            _distanceScroll = 0.6748586f; //
        }

    }


    public void ChangePosition()
    {
        if (_countPlayers ==1)
        {
            _playersList[0].SetActive(true);
        }

        if(_countPlayers == 2)
        {
            _playersList[1].SetActive(true);
            _content.transform.position = new Vector3(_positionContent[1].transform.position.x, _positionContent[1].transform.position.y, _positionContent[1].transform.position.z);
            _moveObject.transform.position = new Vector3(_positionMoveObject[0].transform.position.x, _positionMoveObject[0].transform.position.y, _positionMoveObject[0].transform.position.z);
        }

        if(_countPlayers == 3)
        {
            _playersList[2].SetActive(true);
        }

        if(_countPlayers == 4)
        {
            _playersList[3].SetActive(true);
            _content.transform.position = new Vector3(_positionContent[2].transform.position.x, _positionContent[2].transform.position.y, _positionContent[2].transform.position.z);
            _moveObject.transform.position = new Vector3(_positionMoveObject[1].transform.position.x, _positionMoveObject[1].transform.position.y, _positionMoveObject[1].transform.position.z);

            _topTextObject.SetActive(false);
            _topBlueObject.SetActive(true);
            _scrollRect.enabled = true;
            _scrollbar.value = _distanceScroll;
        }

        if(_countPlayers == 5)
        {
            _playersList[4].SetActive(true);
        }

        if(_countPlayers == 6)
        {
            _playersList[5].SetActive(true);
            _scrollbar.value = 0;
        }

        if(_countPlayers == 7)
        {
            _playersList[6].SetActive(true);
        }

        if(_countPlayers == 8)
        {
            _playersList[7].SetActive(true);
            _addPlayersObject.SetActive(false);
        }

       
    }
}
