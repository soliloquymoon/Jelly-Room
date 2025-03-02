using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer audioMixer;
    private Slider masterSlider, bgmSlider, sfxSlider;
    public AudioSource bgmSource, sfxSource;
    private Dictionary<string, AudioClip> sfxClips;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Load sfx resources to dictionary
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio/SFX");
        sfxClips = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in clips)
        {
            sfxClips[clip.name] = clip;
        }

        // Play bgm (loop)
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void SetSliders(Slider masterSlider, Slider bgmSlider, Slider sfxSlider)
    {
        // Set sliders to saved values
        this.masterSlider = masterSlider;
        this.bgmSlider = bgmSlider;
        this.sfxSlider = sfxSlider;

        // Set sliders to set new volume when value is changed
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        
        // Set sliders to saved values
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Set sliders to set new volume when value is changed
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.GetFloat("MasterVolume", volume);
    }

    private void SetBGMVolume(float value)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    private void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void PlaySFX(string sfxName)
    {
        sfxSource.PlayOneShot(sfxClips[sfxName]);
    }

    public void SetToDefault()
    {
        SetMasterVolume(1);
        SetBGMVolume(1);
        SetSFXVolume(1);
        masterSlider.value = 1;
        bgmSlider.value = 1;
        sfxSlider.value = 1;
        PlaySFX("Button");
    }
}