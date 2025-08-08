// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public static PlayerController Instance {get; private set;}
//     public GameObject player;
//     private Vector2 currentPosition;
//     public float moveSpeed;
    
//     void Awake()
//     {
//         if(Instance == null)
//         {
//             Instance = this;
//         }
//         else
//         {
//             Destroy(gameObject);
//             return;
//         }
        
//         DontDestroyOnLoad(gameObject);
            
//     }

//     void Start()
//     {   

//         currentPosition = player.transform.position; // 플레이어 위치 초기화 
        
//     }

//     void Update()
//     {
//         float moveSpeed = 5f;
//         Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // input 값 저장 (상하좌우 이동)
//         currentPosition += input * moveSpeed * Time.deltaTime;  // 플레이어 위치 업데이트 (이동위치값에 속도와 시간을 곱)
//         transform.position = currentPosition;  // 플레이어 위치 업데이트 (현재 위치값에 이동위치값을 더해서 이동시킴)

//         if(input.x > 0)
//         {
//             player.GetComponent<SpriteRenderer>().flipX = true;
//         }
//         else if (input.x < 0)
//         {
//             player.GetComponent<SpriteRenderer>().flipX = false;
//         }
//     }

    
    

// }
