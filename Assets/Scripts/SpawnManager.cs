using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoint; // 스폰 위치
    public GameObject[] enemyPrefabs;
    float timer;
    
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 0.2f)
        {
            Spawn();
            timer = 0f;
        } 
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Instance.pool.Get(Random.Range(0, 2)); 
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // 트랜스폼은 1부터
    }

    void SpawnBoss()
    {
        // 추후 보스 로직 추가 가능
        Debug.Log("보스 스폰 로직 필요");
    }
}

// public class SpawnManager : MonoBehaviour
// {
//     public Transform[] spawnPoints;
//     public float spawnDelay = 1.5f;
//     private float timer;

//     public float minDistanceFromPlayer = 5f;
//     public int minSpawnCount = 1;
//     public int maxSpawnCount = 4;

//     public Vector2 spawnRangeMin;
//     public Vector2 spawnRangeMax;

//     public Player player;

//     void Start()
//     {
//         player = GameManager.Instance.player;
//     }

//     void Update()
//     {
//         timer += Time.deltaTime;

//         if (timer >= spawnDelay)
//         {
//             int maxIndex = UIManager.Instance.GetScore() >= 500 ? 3 : 2;
//             SpawnEnemies(maxIndex);

//             if (UIManager.Instance.GetScore() >= 2000)
//                 SpawnBoss();

//             timer = 0f;
//         }
//     }

//     void SpawnEnemies(int maxIndex)
//     {
//         int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1);

//         for (int i = 0; i < spawnCount; i++)
//         {
//             Vector2 spawnPos = Vector2.zero;
//             bool found = false;

//             for (int j = 0; j < 20; j++)
//             {
//                 float x = Random.Range(spawnRangeMin.x, spawnRangeMax.x);
//                 float y = Random.Range(spawnRangeMin.y, spawnRangeMax.y);
//                 Vector2 pos = new Vector2(x, y);

//                 if (Vector2.Distance(pos, player.transform.position) >= minDistanceFromPlayer)
//                 {
//                     spawnPos = pos;
//                     found = true;
//                     break;
//                 }
//             }

//             if (found)
//             {
//                 int index = Random.Range(0, maxIndex);
//                 GameObject enemy = GameManager.Instance.pool.Get(index);
//                 enemy.transform.position = spawnPos;
//             }
//         }
//     }

// }





