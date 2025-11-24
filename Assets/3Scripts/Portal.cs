using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal; // 연결된 포탈을 참조하는 변수
    private bool isCooldown = false; // 쿨타임 상태 변수
    public float cooldownTime = 1.0f; // 쿨타임 시간

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && linkedPortal != null && !isCooldown)
        {
            StartCoroutine(TeleportWithCooldown(other.transform));
        }
    }

    private IEnumerator TeleportWithCooldown(Transform player)
    {
        isCooldown = true; // 쿨타임 시작
        linkedPortal.isCooldown = true; // 연결된 포탈도 쿨타임 시작
        player.position = linkedPortal.transform.position; // 플레이어 위치 이동
        Debug.Log("플레이어가 포탈을 통해 이동했습니다.");
        yield return new WaitForSeconds(cooldownTime); // 쿨타임 대기
        isCooldown = false; // 쿨타임 종료
        linkedPortal.isCooldown = false; // 연결된 포탈의 쿨타임도 종료
    }
}
