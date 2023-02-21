using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource spinSound;
    [SerializeField] private AudioSource musicSound;

    [SerializeField] private GameObject musicOn , musicOn2;
    [SerializeField] private GameObject musicOff, musicOff2;

    [SerializeField] private GameObject soundOn, soundOn2;
    [SerializeField] private GameObject soundOff, soundOff2;

    private int soundValue;
    private int musicValue;

    void Start()
    {
        musicValue = PlayerPrefs.GetInt("musicValue", musicValue);
        soundValue = PlayerPrefs.GetInt("soundValue", soundValue);

        if (musicValue == 0)
        {
            musicSound.Play();
            musicOn.SetActive(true);
            musicOff.SetActive(false);
            musicOn2.SetActive(true);
            musicOff2.SetActive(false);
        }

        if (musicValue == 1)
        {
            musicSound.Stop();
            musicOn.SetActive(false);
            musicOff.SetActive(true);
            musicOn2.SetActive(false);
            musicOff2.SetActive(true);
        }

        if (soundValue == 0)
        {
            buttonSound.volume = 1;
            spinSound.volume = 1;
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            soundOn2.SetActive(true);
            soundOff2.SetActive(false);
        }

        if (soundValue == 1)
        {
            buttonSound.volume = 0;
            spinSound.volume = 0;
            soundOn.SetActive(false);
            soundOff.SetActive(true);
            soundOn2.SetActive(false);
            soundOff2.SetActive(true);
        }
    }

    public void MusicOn()
    {
        if (musicValue == 0)
        {
            musicValue += 1;
            PlayerPrefs.SetInt("musicValue", musicValue);
            musicSound.Stop();
            musicOn.SetActive(false);
            musicOff.SetActive(true);
            musicOn2.SetActive(false);
            musicOff2.SetActive(true);

        }
    }

    public void MusicOff()
    {

        if (musicValue == 1)
         {
             musicValue -= 1;
             PlayerPrefs.SetInt("music", musicValue);
             musicSound.Play();
             musicOn.SetActive(true);
             musicOff.SetActive(false);
            musicOn2.SetActive(true);
            musicOff2.SetActive(false);
        }
    }

    public void SoundOn()
    {
        if (soundValue == 0)
        {
            soundValue += 1;
            PlayerPrefs.SetInt("soundValue", soundValue);
            buttonSound.volume = 0;
            spinSound.volume = 0;
            soundOn.SetActive(false);
            soundOff.SetActive(true);
            soundOn2.SetActive(false);
            soundOff2.SetActive(true);

        }
    }

    public void SoundOff()
    {
        if (soundValue == 1)
        {
            soundValue -= 1;
            PlayerPrefs.SetInt("soundValue", soundValue);
            buttonSound.volume = 1;
            spinSound.volume = 1;
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            soundOn2.SetActive(true);
            soundOff2.SetActive(false);

        }
    }
}
