using UnityEngine;

public class AttackPrefab : MonoBehaviour
{
    // public static AttackPrefab Instance;  // 인스턴스 제거하고 오브젝트 풀링 
    // public GameObject prefabObj;
    public int lifeTime = 2; 
    public UIManager scoreUI;
    private int damage;

    
    void Start()
    {
        // Destroy(gameObject, lifeTime);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    void OnTriggerEnter2D(Collider2D other) // 적에게 데미지를 전달
    {
        if (other.CompareTag("Enemy"))  // other로 태그 확인 "Enemy" -> Enemy의 스크립트나 컴포넌트 접근 가능 
        {
            // Destroy(gameObject);
            other.GetComponent<EnemyPrefabs>().GetDamage(damage);
            // 적 처치 처리
            gameObject.SetActive(false);
            
        }
    }
    
    public void UpgradePower() // 공격력 업그레이드 
    {
        damage += 5;
    }

    
}
