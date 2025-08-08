using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Health")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    [Header("Score")]
    public TextMeshProUGUI scoreText;

    private int current = 0;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        EnemyPrefabs.OnEnemyDied += AddScore;
    }

    public void UpdateHealth(int current, int max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = current;

        if (healthText != null)
            healthText.text = $"HP {current}/{max}";
    }
    public void AddScore(int score)
    {
        current += score;

        if(scoreText != null)
            scoreText.text = "점수 : " + current;
    }

    public int GetScore()
    {
        return current;
    }
}

