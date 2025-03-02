using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private bool delete = false;
    public Text deleteBtnText;
    public GameObject about;

    void Start()
    {
        // Assign volume sliders before setting the settings inactive
        AudioManager.instance.SetSliders(GameObject.Find("MasterSlider").GetComponent<Slider>(),
        GameObject.Find("BGMSlider").GetComponent<Slider>(), GameObject.Find("SFXSlider").GetComponent<Slider>());

        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            about.SetActive(true);
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else
        {
            about.SetActive(false);
        }
        
        this.gameObject.SetActive(false);
    }

    public void OpenAbout()
    {
        AudioManager.instance.PlaySFX("Button");
        this.about.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void SetToDefaultSettings()
    {
        AudioManager.instance.SetToDefault();
    }

    public void DeleteSaveData()
    {
        if (delete)
        {
            AudioManager.instance.PlaySFX("Clear");
            SceneLoader.instance.ResetAndStartGame();
        }
        else
        {
            deleteBtnText.text = "Click again to confirm";
            delete = true;
        }
    }

    public void OpenSettings()
    {
        this.gameObject.SetActive(true);
        AudioManager.instance.PlaySFX("Button");
    }

    public void CloseSettings()
    {
        delete = false;
        this.gameObject.SetActive(false);
    }
}