using UnityEngine;
using System.Collections;

public class Reposition : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"OnTriggerExit2D 호출: {collision.name}");
        if (!collision.CompareTag("Area"))
        {
            Debug.Log("Area 빠져나감");

            // if(!collision.CompareTag("AreaSecond"))
            // {
            //     Debug.Log("Aread2!!! 태그 안함");
            // }
            return;
        }           
        

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;

        float diffx = Mathf.Abs(playerPos.x - myPos.x);
        float diffy = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.Instance.player.input;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                Debug.Log("Ground 재배치 실행");
                if (diffx > diffy)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffx < diffy)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy": // 태그 이름 오타 확인 필요
                // 추후 구현
                break;
        }
    }
}
