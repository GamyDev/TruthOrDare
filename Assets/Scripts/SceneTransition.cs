using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        ResetScene();
    }
}
