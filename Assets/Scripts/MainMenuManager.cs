using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private SoundManagerUI soundManagerUI;
    public GameObject soundMenuPanel;
    
    public void ToggleMenu()
    {
        bool isActive = soundMenuPanel.activeSelf;
        soundMenuPanel.SetActive(!isActive);
    }

    public void OpenSoundMenuPanelMenu()
    {
        soundMenuPanel.SetActive(true);
    }

    public void CloseSoundMenuPanelMenu() // 패널에 들어가서 닫기 버튼에 연결 
    {
        soundMenuPanel.SetActive(false);
        SoundManager.instance.ButtonClickSFX();
    }


    public void OnClickStartGame()
    {
        SceneManager.LoadScene("GameScene"); // 게임 플레이 씬으로 전환
        SoundManager.instance.ButtonClickSFX();
    }

    public void OnClickSettings()
    {
        // 설정 UI Panel을 활성화
        // 설정 관련 기능은 별도 패널로 관리 (UI로만 구성)
        OpenSoundMenuPanelMenu();
        SoundManager.instance.ButtonClickSFX();
        
    }

    // public void OnClickQuitSoundMenu()
    // {
    //     soundMenuPanel.SetActive(false);
    // }

    public void OnClickQuitGame()
    {
        Application.Quit(); // 빌드 시 종료
        SoundManager.instance.ButtonClickSFX();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 종료
#endif
    }
}
