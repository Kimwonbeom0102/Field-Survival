using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SoundManagerUI : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        bgmSlider.onValueChanged.AddListener(SoundManager.instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.instance.SetSFXVolume);
        bgmSlider.value = SoundManager.instance.bgmSource.volume;
        sfxSlider.value = SoundManager.instance.sfxSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
