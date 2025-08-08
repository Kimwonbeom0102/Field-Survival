using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // 사운드 종류
    // 1. 사운드가 들어갈 공간 (종류뱔)
    // 2. bgm, sfx , 볼륨조절(슬라이더)
    // 3. 설계
    // 싱글톤, 오디오소스나 오디오클립 저장
    // 메서드로 사운드 재생/중지/볼륨조절
    // ui와 슬라이더 연동하여 볼륨 조절 
    // 메뉴씬에서 게임씬으로 바뀌면 게임씬 사운드 재생 (메뉴씬 사운드 중지) 

    public static SoundManager instance;

    // 오디오소스 (카테고리) 스피커역할
    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    // 오디오클립 목록(사운드 리소스)
     [Header("Audio Clips")]
    public AudioClip menuBGM;
    public AudioClip gameBGM;
    public AudioClip buttonClick;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMainMenuBGM()
    {
        PlayBGM(menuBGM);
    }

    public void PlayGameBGM()
    {
        PlayBGM(gameBGM);
    }

    public void PlayBGM(AudioClip clip)
    {
        if(bgmSource.clip == clip) return;
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
        Debug.Log("Play BGM 재생");
    }
    
    public void PlaySFX(AudioClip clip)
    {
        if(sfxSource != null & clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void ButtonClickSFX()
    {
        PlaySFX(buttonClick);
    }
    public void SetBGMVolume(float volume)
    {
        if(bgmSource != null)
        {
            bgmSource.volume = volume;
        }
        
    }

    public void SetSFXVolume(float volume)
    {
        if(sfxSource != null)
        {
            sfxSource.volume = volume;
        }
        
    }

    void Start()
    {
        PlayMainMenuBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
