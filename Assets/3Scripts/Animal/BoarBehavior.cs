using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarBehavior : MonoBehaviour
{
    public float speed = 5.0f;  // 멧돼지의 이동 속도
    public float chargeSpeed = 10.0f;  // 멧돼지의 돌진 속도
    public float attackRange = 1.5f;  // 공격 범위
    public float detectionRange = 10.0f;  // 멧돼지가 플레이어를 감지할 수 있는 범위
    public int damage = 10;  // 공격 시 가할 피해량
    public float chargeDuration = 1.0f;  // 돌진 지속 시간
    public float cooldownDuration = 2.0f;  // 돌진 후 대기 시간

    private GameObject player;
    private PlayerHealth playerHealth;
    private bool isCharging = false;
    private bool canDealDamage = false;  // 데미지 입힐 수 있는 상태
    private bool isAngry = false;  // 멧돼지가 플레이어를 쫓아갈지 여부
    private Vector3 targetPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (player != null && playerHealth != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= detectionRange)
            {
                isAngry = true;
            }
            else
            {
                isAngry = false;
            }

            if (isAngry)
            {
                if (distance > attackRange && !isCharging)
                {
                    ChasePlayer();
                }
                else if (!isCharging)
                {
                    targetPosition = player.transform.position;  // 플레이어의 현재 위치를 기록
                    StartCoroutine(ChargeAttack());
                }
            }
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    IEnumerator ChargeAttack()
    {
        isCharging = true;
        canDealDamage = true;  // 돌진 시작 시 데미지 입힐 수 있는 상태로 전환
        Vector3 chargeDirection = (targetPosition - transform.position).normalized;

        float chargeEndTime = Time.time + chargeDuration;
        while (Time.time < chargeEndTime)
        {
            transform.position += chargeDirection * chargeSpeed * Time.deltaTime;
            yield return null;  // 다음 프레임까지 대기
        }

        canDealDamage = false;  // 돌진 종료 시 데미지 입힐 수 없는 상태로 전환
        yield return new WaitForSeconds(cooldownDuration);
        isCharging = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canDealDamage)
            {
                playerHealth.GotDamage(damage);
            }
        }
    }
}
