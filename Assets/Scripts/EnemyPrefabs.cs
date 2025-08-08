using UnityEngine;

public class EnemyPrefabs : MonoBehaviour // 몬스터 스탯 
{   
    // public float speed;
    // pbulic Rigidbody2D target;

    // bool isLive;

    // Rigidbody2D rigid; rigd;
    // SpriteRenderer spriter;

    // void Awake()
    // {
    //     rigid = GetComponent<Rigidbody2D>();
    //     spriter = GetComponent<SpriteRenderer>();
        
    // }

    // void FixedUpdate()
    // {
    //     Vector2 dirVec = target.position - rigid.position;
    //     Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
    //     rigid.MovePosition(rigid.position + nextVec); // 순간이동처럼 움직임
    //     rigid.velocity = Vector2.zero; // 0으로 고정시킴
    // }

    ///////
    public Vector2 targetPosition;
    public Player player;
    public float moveSpeed = 2.5f;
    public int health =  20;
    public int damage = 4;
    public int score = 10;
    // [SerializeField] private BulletPrefabManager bulletPrefabManager;

    public EnemyType enemyType;

    [SerializeField] private GameObject bulletPrefab;
    public Transform bulletPosition;
    private float timer;
    public float bulletCooltime = 3f;

    public delegate void EnemyDiedHandler(int score);
    public static event EnemyDiedHandler OnEnemyDied; 

    public UIManager uiManager;
    public int plantUpScore = 550;
    public int scoreStack;


    void Start()
    {
        player = Player.Instance; // 플레이어 싱글톤 연결 (플레이어매니저에서 싱글톤을 생성하고 start메서드에서 연결)
        // playerManager = FindObjectOfType<PlayerController>();
        targetPosition = gameObject.transform.position;
        uiManager = UIManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyType == EnemyType.Ghost || enemyType == EnemyType.Nosecow)
        {
            Movement();
        }

        UpdateDirection();
        
        timer += Time.deltaTime; // 타이머를 증가시킴
        if(timer >= bulletCooltime)
        {
            // plant 타입만 Fire() 조건 분기
            if(enemyType == EnemyType.Plant && UIManager.Instance.GetScore() >= plantUpScore)
            {
                Debug.Log($"스코어 {plantUpScore} 돌파, Plant 몹이 원거리 공격!");
                Fire();
            }
            timer = 0; // timer 초기화
        }
        
    }

    // void OnEnable()
    // {
    //     player = GameManager.Instance.player.GetComponent<Rigidbody2D>();
    // }

    void UpdateDirection()
    {
        if(player == null) return;

        if(player.transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void Fire() // 생성된 프리팹이 발사하는 로직 (데미지는 프리팹에서 관리)
    {
        if(bulletPrefab == null ) return; 
        GameObject bulletPos = Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);
        
        if(bulletPos.TryGetComponent(out BulletPrefabManager bulletScript))
        {
            Vector2 dir = (player.transform.position - bulletPosition.position).normalized;
            // bulletPos.GetComponent<BulletPrefabManager>().SetDirection(dir);
            bulletScript.SetDirection(dir);

        }
        
    }

    void OnTriggerEnter2D(Collider2D other) // 몸으로 공격 -> 몹 발사체로 만들 예정 
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.TakeDamage(damage);
            }

            // other.GetComponent<Player>().TakeDamage(damage);
        }
    }


    public void GetDamage(int damage) // 발사체로부터 데미지를 전달받아 체력이 깎임 
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnEnemyDied?.Invoke(score);
        gameObject.SetActive(false);
    }

    public void Enforce()
    {
        switch (enemyType)
        {
            case EnemyType.Ghost:
                moveSpeed = 4f;
                health = 30;
                damage = 5;
                score = 11;
                break;
            case EnemyType.Nosecow:
                moveSpeed = 5f;
                health = 35;
                damage = 6;
                score = 12;
                break;
            case EnemyType.Plant:
                moveSpeed = 0f;
                health = 30;
                damage = 8;
                score = 15;

                if(bulletPrefab != null && bulletPrefab.TryGetComponent(out BulletPrefabManager bulletScript))
                {
                    bulletScript.EnforceBullet();
                }
                // bulletPrefab.GetComponent<BulletPrefabManager>().EnforceBullet();
                // bulletPrefabManager.EnforceBullet();
                break;
        }   
          Debug.Log($"{enemyType} 강화됨! moveSpeed:{moveSpeed}, health:{health}, damage:{damage}");
        
    }

    // 몹이 생성된 후에 플레이어를 추격하는 메서드
    // 현재 몹 위치
    // 플레이어 위치
    // 조건을 주고나서 (조건을 줄 필요가 있을까? 움직임만 구현하는건데) 조건주는 로직은 업데이트에서 하면 된다 생각함 
    public void Movement() // 몹의 움직임 구현 
    {
        if (player != null)
        {
            Vector3 playePos = player.transform.position;
            Vector2 direction = (player.transform.position - transform.position).normalized; 
            
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
    }
}
