using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroyTree : MonoBehaviour
{
    public float baseDestructionProbability = 0f; // 기본 파괴 확률 (0-100)
    public float increasePerRabbit = 0.2f; // 토끼 한 마리당 증가하는 파괴 확률
    private SpawnTree spawnTreeScript; // SpawnTree 스크립트 참조
    private RabbitManager rabbitManager; // RabbitManager 스크립트 참조

    void Start()
    {
        spawnTreeScript = FindObjectOfType<SpawnTree>();
        rabbitManager = FindObjectOfType<RabbitManager>();
        StartCoroutine(DestroyTreeRoutine());
    }

    IEnumerator DestroyTreeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            if (rabbitManager != null)
            {
                float currentDestructionProbability = baseDestructionProbability + (rabbitManager.currentCount * increasePerRabbit);
                float randomValue = Random.Range(0f, 100f);
                if (randomValue < currentDestructionProbability)
                {
                    // 나무가 파괴되는 경우
                    Debug.Log("토끼가 나무를 뽀개 먹었습니다!");
                    spawnTreeScript.RemoveOccupiedPosition(transform.position);
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogWarning("RabbitManager를 찾을 수 없습니다.");
            }
        }
    }
}