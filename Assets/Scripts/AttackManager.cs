using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum WeaponType {BasicAttack, Fire };

public class AttackManager : MonoBehaviour
{

    public List<GameObject> bulletPrefab;
    public List<List<GameObject>> bulletPool = new List<List<GameObject>>();
    public int poolsize = 1000;
    // public GameObject bulletPrefab;
   
    // public AttackPrefab attackScript;
    public UIManager uiManager;
    // public GameObject attackPrefab; // 어택프리팹 담아줌 (인스펙터창에서 할당)
    public float attackPrefabSpeed = 15f; // 프리팹 속도는 10으로 고정 
    public int damage = 10;
    public Transform playerPrefab; // 프리팹 위치값 (플레이어 할당)
    public float fireTimer = 0f;
    public float fireCoolTime = 0.1f;

    public List<GameObject> weaponPre;
    private int currentWeaponIndex = 0;
    

    void Start()
    {
        foreach (GameObject prefab in bulletPrefab)
        {
            List<GameObject> pool = new List<GameObject>();

            for (int i = 0; i< poolsize; i++)
            {
                // GameObject bullet = Instantiate(bulletPrefab);
                GameObject bullets = Instantiate(prefab);
                // bullet.SetActive(false);
                // bulletPool.Add(bullet);
                bullets.SetActive(false);
                pool.Add(bullets);
            }

            bulletPool.Add(pool);
        }
        
        

        SwapWeapon(0);
    }

    void SwapWeapon(int index) // 무기 강화, 교환 
    {
        if(index >= 0 && index < weaponPre.Count)
        {
            currentWeaponIndex = index;

            switch(index)
            {
                case 0:
                    damage = 15;
                    attackPrefabSpeed = 20f;
                    fireCoolTime = 0.09f;
                    break;

                case 1:
                    damage = 20;
                    attackPrefabSpeed = 21f;
                    fireCoolTime = 0.08f;
                    break;

                // case 2:
                //     damage = 30;
                //     attackPrefabSpeed = 9f;
                //     break;
            }

            Debug.Log($"무기 변경됨! 현재 공격력 {damage}");
        }
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if ( fireTimer >= fireCoolTime)
        {
            Fire();

            fireTimer = 0f;
        }

        if(UIManager.Instance.GetScore() >= 1000 && currentWeaponIndex == 0)
        {
            SwapWeapon(1);
        }
    }
    
    GameObject GetpooledBullet(int weaponIndex)
    {
        List<GameObject> pool = bulletPool[weaponIndex];
        {
            foreach (GameObject bullet in pool)
            {
                if(!bullet.activeInHierarchy)
                {
                    return bullet;
                }

            }
            
        }
        return null;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    void Fire() // 발사체 생성 후 발 사
    {
        Vector3 prefabPos = playerPrefab.position; // 프리팹 포지션 생성 (인스펙터에서 초기위치에 플레이어 할당)
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPosition - prefabPos).normalized;

        GameObject currentPrefab = weaponPre[currentWeaponIndex];
        GameObject attackObject = GetpooledBullet(currentWeaponIndex); // 프리팹을 생성하고 
        if(attackObject == null)
        {
            Debug.LogWarning("풀링된 총알이 부족합니다.");
            return;
        }
        attackObject.transform.position = prefabPos;
        attackObject.transform.rotation = Quaternion.identity;
        attackObject.SetActive(true);

        AttackPrefab attackScript = attackObject.GetComponent<AttackPrefab>();
        attackScript.SetDamage(damage);

        Rigidbody2D rb = attackObject.GetComponent<Rigidbody2D>();  // 물리엔진을 이용해서 충돌처리하기 위한 컴포넌트 변수 
        rb.linearVelocity = direction * attackPrefabSpeed; // 이동구현, transform.position이 아닌 rigidbody로 움직임 구현
    }
    
}
