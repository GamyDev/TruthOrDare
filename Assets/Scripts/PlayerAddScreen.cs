using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAddScreen : MonoBehaviour
{
    [SerializeField] private PlayersModel _playersModel;
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private GameObject checkOn;
    [SerializeField] private GameObject checkOff;
    [SerializeField] private GameObject _inputObject;
    [SerializeField] private GameObject _welcomObject;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private WelcomeScreen _welcomeScreen;
    [SerializeField] private PlayersList _playersList;
    [SerializeField] private GameObject _backInput;
    [SerializeField] private GameObject _backWelcome;


    public void CheckInputField(string value)
    {
        checkOn.SetActive(!string.IsNullOrEmpty(value));
        checkOff.SetActive(string.IsNullOrEmpty(value));
    }

    public void AddUser()
    {
        _playersModel.AddNewPlayer(new Player()
        {
            name = _name.text
        });
        _name.text = "";
    }

    public void OnSelectUserField()
    {
        _name.transform.DOLocalMoveY(_name.transform.localPosition.y * -1, 1).SetEase(Ease.OutBounce);
    }

    public void OnDeselectUserField()
    {
        _name.transform.DOLocalMoveY(_name.transform.localPosition.y * -1, 1).SetEase(Ease.OutBounce);
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(!string.IsNullOrEmpty(_inputObject.GetComponent<TMP_InputField>().text)) { 
                 _inputObject.SetActive(false);
                _welcomObject.SetActive(true);
                AddUser();
                _welcomeScreen.SetUser(); 
                _audioSource.Play();
                _backInput.SetActive(false); 
            }
        }
    }
}
