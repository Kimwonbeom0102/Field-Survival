using UnityEngine;
using UnityEngine.Audio;


public class Player : MonoBehaviour
{
    public static Player Instance;
    public int health = 100;
    public int currentHealth = 0;
    public int nextStageScore = 300;
    public int intervalScore = 300;
    [SerializeField]public UIManager uiManager;
    // private bool stageLvup = false;
    public int stageLevel = 0;

    [SerializeField] private EnemyPrefabs[] allEnemies;

    private Vector2 currentPosition;
    public float moveSpeed;
    public Vector2 input;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPosition = transform.position; // 플레이어 위치 초기화 
        currentHealth = health;
        uiManager.UpdateHealth(currentHealth, health);
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 5f;
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // input 값 저장 (상하좌우 이동)
        currentPosition += input * moveSpeed * Time.deltaTime;  // 플레이어 위치 업데이트 (이동위치값에 속도와 시간을 곱)
        transform.position = currentPosition;  // 플레이어 위치 업데이트 (현재 위치값에 이동위치값을 더해서 이동시킴)

        if(input.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (input.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if(uiManager != null && UIManager.Instance.GetScore() >= nextStageScore)
        {
            stageLevel++;

            Debug.Log($"다음 단계로 넘어갑니다. 현재 스테이지 : {stageLevel}");
            
            StageLevelUp();

            nextStageScore += intervalScore;

        }

    }
    
    void StageLevelUp()
    {
        if(stageLevel == 4)
        {       
            foreach (EnemyPrefabs enemyPrefab in allEnemies)
            {
                GameObject moster = Instantiate(enemyPrefab.gameObject); // 프리팹에서 복사본을 생성
                EnemyPrefabs stats = moster.GetComponent<EnemyPrefabs>(); // 복사본의 스크립트를 가져와서
                
                stats.Enforce(); // 복사본에게 Enforce 호출 
            }
            
            Heal(20);
            SpeedUp(2);
        }

    }

    void Heal(int heal)  // 힐 
    {
        if( currentHealth < 100){currentHealth += heal;}
        
        if( currentHealth >= 100)
        {
            currentHealth = 100;
        }

        Debug.Log($"체력이 {heal} 만큼 회복되었습니다. 현재 체력 {currentHealth}");
    }

    void SpeedUp(float speed)
    {
        moveSpeed += speed;
    }

    public void TakeDamage(int damage) // 몹으로부터 공격받아 체력이 깎임 
    {
        currentHealth -= damage;
        if (uiManager != null)
        {
            uiManager.UpdateHealth(currentHealth, health);
        }
            
        Debug.Log($"입은 피해 -{damage} 현재 체력 : {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            uiManager.UpdateHealth(currentHealth, health);
            // Destroy(this.gameObject);
            Die(); 
            // 플레이어 사망 후 게임오버 패널 (게임재시작 / 메인메뉴) 활성화 (호출)
        }
    }

    public void Die() // 플레이어 사망
    {
        Debug.Log("플레이어 사망");
        
        // Destroy(gameObject);
        // 게임 종료 로직 추가
    }
}
