using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickDamage : MonoBehaviour
{
    public PlayerHealth playerHealth; // PlayerHealth 스크립트 참조

    void OnMouseDown()
    {
        Debug.Log("플레이어가 클릭되었습니다!"); // 디버그 메시지 추가

        if (playerHealth != null)
        {
            Debug.Log("체력 40 감소!"); // 디버그 메시지 추가
            playerHealth.GotDamage(40); // 체력을 40 감소시킴
        }
        else
        {
            Debug.Log("PlayerHealth 스크립트가 연결되지 않았습니다."); // 디버그 메시지 추가
        }
    }
}
