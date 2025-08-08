using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    public Player player;
    public EnemyManager pool;

    public bool isGameOver = false;
    public bool isPaused = false;
    public bool isActive = false;

    public int score = 0;

    // public GameObject gameOverUI;
    // public GameObject pauseUI;
    public GameObject escPanel;
    public GameObject gameOverPanel;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindFirstObjectByType<GameManager>();

                if(instance == null)
                {
                    Debug.LogError("씬에 GamaManager가 없습니다.");
                }
            }
            // 보험용 코드 
            if(instance != null && instance.player == null )
            {
                instance.player = FindFirstObjectByType<Player>();
            }
            return instance;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // 보험용 코드 
        // if(player == null)
        // {
        //     player = FindFirstObjectByType<Player>();
        // }
        
    }

    void Start()
    {
        StartGame();
        if(SoundManager.instance != null)
        {
            SoundManager.instance.PlayGameBGM();
        }
        else
        {
            Debug.LogWarning("SoundManager가 아직 생성되지 않았습니다.");
        }
    }

    void Update()
    {
        // ESC 키로 일시정지 토글
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseGame();
            }
                
            else
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        // if (pauseUI) pauseUI.SetActive(false);
        escPanel.SetActive(false);
        Debug.Log("게임 재개됨");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        isPaused = false;
        score = 0;

        // if (pauseUI) pauseUI.SetActive(false);
        if (escPanel) escPanel.SetActive(false);
        // if (gameOverUI) gameOverUI.SetActive(false);
    }

    public void GameOver()  // 플레이어 사망하면 게임오버 메서드 호출 
    {
        isActive = gameOverPanel.activeSelf;
        gameOverPanel.SetActive(true);
        isGameOver = true;
        Time.timeScale = 0f;
        // if (gameOverUI) gameOverUI.SetActive(true);

        Debug.Log("게임 오버!");
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        // if (pauseUI) pauseUI.SetActive(true);
        escPanel.SetActive(true);
        Debug.Log("게임 일시정지됨");
    }

    public void RestartGame()
    {
        // Time.timeScale = 1f;
        // isGameOver = false;
        // isPaused = false;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // 메인메뉴 씬으로 전환
    }
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("점수 획득: " + score);
        // UI 갱신 필요시 추가
    }



}
