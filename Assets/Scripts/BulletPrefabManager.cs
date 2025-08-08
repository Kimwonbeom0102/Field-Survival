using UnityEngine;

public class BulletPrefabManager : MonoBehaviour // 방향, 이동, 데미지, 제거 등
{
    public static BulletPrefabManager Instance; // 싱글톤제거 후 오브젝트 풀링 

    public int damage = 15;
    public int bulletSpeed = 5;
    private Vector2 direction;
    private int enforceDamage = 5;
    private int enforceSpeed = 1;
    public float lifeTime = 1.5f;
    
    void Awake()
    {
        if(Instance == null)
        Instance = this;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame

    public void EnforceBullet()
    {
        damage += enforceDamage;
        bulletSpeed += enforceSpeed;

        Debug.Log($"식물 발사체 공격력, 속도 강화, 공격력 : {damage}, 스피드 : {bulletSpeed}");
    }

    void Update()
    {
        // 매 프레임 이동: 방향 * 속도 * 시간(프레임 독립)
        // Translate 함수는 transform의 위치를 변경하는 함수
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // 충돌한 플레이어에게 데미지 전달
            other.GetComponent<Player>().TakeDamage(damage);

            // 투사체 제거
            Destroy(gameObject);
        }
    }
    // 방향 설정 메서드: 외부에서 방향 벡터를 전달받아 저장
    // 방향 계산 시 dir - 프리팹 생성위치 
    public void SetDirection(Vector2 dir) 
    {
        direction = dir;
    }
    
}
