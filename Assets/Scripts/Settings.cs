using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider music;
    public TMP_Text musicText;

    public static int currentLanguage;
    public ToggleGroup toggleGroupLanguage;


    private void Start()
    {
        music.value = PlayerPrefs.GetInt("Music", 1);
        AudioListener.volume = PlayerPrefs.GetInt("Music", 1);
        musicText.text = PlayerPrefs.GetInt("Music", 1) == 1 ? "Music: On_key" : "Music: Off_key";
    }


    public void SwitchMusic(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetInt("Music", (int)value);
       
        musicText.text = value == 1 ? "Music: On_key" : "Music: Off_key";
    }

    public void SwitchMusic()
    {
        if(music.value == 0) { 
            AudioListener.volume = 1;
            music.value = 1;
            PlayerPrefs.SetInt("Music", (int)1);
            musicText.text = "Music: On";
        } else
        {
            music.value = 0;
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Music", (int)0);
            musicText.text = "Music: Off";
        }
    }
}
