using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void ResetAndStartGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Tutorial", 1); // Do not repeat tutorial (about)
        PlayerPrefs.Save();
        StartGame();
    }
}