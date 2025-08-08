using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum EnemyType { Ghost, Nosecow, Plant, Boss} // enum 배열 활용 

public class EnemyManager : MonoBehaviour // 몬스터 pool
{
    // 프리팹들을 보관할 변수
    // 풀 담당을 하는 리스트 
    public GameObject[] prefabs;
    public List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < prefabs.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        // 선택한 풀에 놀고있는 게임오브젝트 접근
         //  발견하면 select에 할당
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf) // 놀고있으면 
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        
        // 풀에 있는 모든 오브젝트가 다 사용되고 있으면
         // 새롭게 생성해서 select 변수에 할당
        if(!select) // null이면 해당 로직
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}

// public enum EnemyType { Ghost, Nosecow, Plant, Boss} // enum 배열 활용 

// public class EnemyManager : MonoBehaviour
// {
//     public GameObject[] prefabs;
//     public List<GameObject>[] pools;

//     void Awake()
//     {
//         pools = new List<GameObject>[prefabs.Length];
//         for (int i = 0; i < prefabs.Length; i++)
//         {
//             pools[i] = new List<GameObject>();
//         }
//     }

//     public GameObject Get(int index)
//     {
//         GameObject obj = pools[index].Find(p => !p.activeSelf);
//         if (obj == null)
//         {
//             obj = Instantiate(prefabs[index], transform);
//             pools[index].Add(obj);
//         }
//         obj.SetActive(true);
//         return obj;
//     }
// }



//     //////
//     // 프리팹 기본 설정 
//     public GameObject player; // 플레이어 인스펙터 할당
//     public List<GameObject> enemyPre; // 몹 프리팹 배열 인스펙터 할당 (다양한 종류)

//     // 스폰 범위 설정 
//     public Vector2 spawnPositionMin; // 스폰 최소 범위
//     public Vector2 spawnPositionMax; // 스폰 최대 범위 

//     public Vector2 spawnBossPositionMin;
//     public Vector2 spawnBossPositionMax;

//     // 스폰 조건 
//     private float timer;  // 시간 누적
//     public float spawnEnemy = 1.5f; // 스폰 주기 (일정 시간이 넘어가면 스폰)
//     public float minimumDistancePlayer = 5f; // 플레이어와 최소거리 스폰
//     public int spawnCountMin = 1; // 몹이 스폰 될 최소 수
//     public int spawnCountMax = 5; // 몹이 스폰 될 최대 수
//     public int attempts = 20;  // 시도 횟수

//     public GameObject bossPre;

    
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         timer += Time.deltaTime; // 타이머를 시간마다 증가
//         if(timer >= spawnEnemy) // 스폰 주기가 넘어가면 스폰 메서드 실행 (주기마다 메서드 실행하여 몹 스폰)
//         {
//             int maxIndex = 2;

//             if(UIManager.Instance.GetScore() >= 500)
//             {
//                 maxIndex = 3;
//             }
//             SpawnEnemy(maxIndex);
            
//             if(UIManager.Instance.GetScore() >= 2000)
//             {
//                 SpawnBoss();
//             }
            

//             timer = 0; // 메서드가 호출되면 0에서 다시 증가하도록 하여 스폰 주기랑 비교
//         }
//     }
    
//     public void SpawnBoss()
//     {
//         Vector2 spawnBossPosition = Vector2.zero; 
//         for( int i = 0; i < attempts; i++)
//         {
//             float x = Random.Range(spawnBossPositionMin.x, spawnBossPositionMax.x);
//             float y = Random.Range(spawnBossPositionMin.y, spawnBossPositionMax.y);
//             Vector2 randPosition = new Vector2(x, y); // 랜덤으로 선택된 위치 

//             float distanceToplayer = Vector2.Distance(randPosition, player.transform.position);

//             if(distanceToplayer >= minimumDistancePlayer)
//                 {
//                     spawnBossPosition = randPosition;
//                     // validPositionFound = true;
//                     break;
//                 }
//         }

//         // if (validPositionFound)
//         //     {
//         //         Instantiate(selectPre, spawnBossPosition, Quaternion.identity);
//         //     }
//         //     //  위치 못 찾으면 생성 안 함
//         //     else
//         //     {
//         //         Debug.LogWarning("유효한 스폰 위치를 찾지 못함");
//         //     }
        
//         Debug.Log("score 2000점 Boss 등장! ");
//     }
    
//     // 랜덤 위치에서 나와야 할 것 -> 랜덤 범위 지정 
//     // 몹이 하나씩 나오지않고 여러마리가 동시에 나올수도 있어야 할 것 -> 5~10개 랜덤 범위 지정 
//     // 범위가 플레이어랑 겹치지 않을걸 (플레이어랑 거리가 있는 스폰) -> 스폰될 지역을 플레이어 거리와 연산 
//     public void SpawnEnemy(int maxIndex)  // 몹 프리팹 스폰
//     {   
//         if(player == null || enemyPre.Count == 0)
//         {
//             Debug.Log("몹이 할당되지 않았습니다. 몹을 할당해주세요.");
//             return;
//         } 
//         int totalSpawnCount = Random.Range(spawnCountMin, spawnCountMax +1 ); // 몹스폰 숫자 키운트 1 ~ 6마리까지 
//         for (int i = 0; i < totalSpawnCount; i ++) // 몹 스폰 생성 반복 예 : 4 -> 4개가 스폰될건데 
//         {  
//             int rand = Random.Range(0, maxIndex);  // 생성할 몹의 종류를 무작위로 만들고 (현재는 3개의 종류)
//             GameObject selectPre = enemyPre[rand];       // 랜덤 할 몹 선택 

//             // 조건을 만족할 포지션을 찾는 변수
//             Vector2 spawnPosition = Vector2.zero;
//             bool validPositionFound = false;

//             // 조건을 만족할 때 까지 반복
//             for (int j = 0; i < attempts; j++)
//             {
//                 float x = Random.Range(spawnPositionMin.x, spawnPositionMax.x);
//                 float y = Random.Range(spawnPositionMin.y, spawnPositionMax.y);
//                 Vector2 randPosition = new Vector2(x, y); // 랜덤으로 선택된 위치 

//                 float distanceToplayer = Vector2.Distance(randPosition, player.transform.position);

//                 if(distanceToplayer >= minimumDistancePlayer)
//                 {
//                     spawnPosition = randPosition;
//                     validPositionFound = true;
//                     break;
//                 }
//             }
            
//             if (validPositionFound)
//             {
//                 Instantiate(selectPre, spawnPosition, Quaternion.identity);
//             }
//             //  위치 못 찾으면 생성 안 함
//             else
//             {
//                 Debug.LogWarning("유효한 스폰 위치를 찾지 못함");
//             }

//         }

//     }
// }
