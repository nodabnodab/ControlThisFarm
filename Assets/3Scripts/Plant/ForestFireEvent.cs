using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestFireEvent : MonoBehaviour
{
    public SpawnTree spawnTreeScript; // SpawnTree 스크립트 참조
    public float minInterval = 100f; // 산불 발생 최소 간격
    public float maxInterval = 240f; // 산불 발생 최대 간격

    void Start()
    {
        StartCoroutine(FireEventRoutine());
    }

    IEnumerator FireEventRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
            TryTriggerFireEvent();
        }
    }

    void TryTriggerFireEvent()
    {
        int treeCount = spawnTreeScript.GetCurrentTreeCount();
        int maxTreeCount = spawnTreeScript.max;

        // 나무 수에 따라 산불 발생 확률 계산 (단순 비례)
        float fireProbability = 0.5f * (float)treeCount / maxTreeCount;

        Debug.Log($"현재 나무 수: {treeCount}, 산불 발생 확률: {fireProbability * 100}%");

        if (Random.value < fireProbability)
        {
            TriggerFireEvent();
        }
        else
        {
            Debug.Log("");
        }
    }

    void TriggerFireEvent()
    {
        Transform[] spawnPoints = spawnTreeScript.spawnPoints;
        List<Transform> treesToRemove = new List<Transform>();

        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnTreeScript.occupiedPositions.Contains(spawnPoint.position))
            {
                if (Random.value < 0.80f) // % 확률로 파괴
                {
                    treesToRemove.Add(spawnPoint);
                }
            }
        }

        GameObject[] treeObjects = GameObject.FindGameObjectsWithTag("Tree"); // Tree 태그를 사용한다고 가정

        foreach (var tree in treesToRemove)
        {
            spawnTreeScript.RemoveOccupiedPosition(tree.position);

            foreach (var treeObject in treeObjects)
            {
                if (Vector3.Distance(treeObject.transform.position, tree.position) < 0.1f) // 위치 비교에 약간의 오차 허용
                {
                    Destroy(treeObject);
                    break; // 나무를 찾았으므로 더 이상 반복할 필요 없음
                }
            }
        }

        Debug.Log($"산불이 발생하여 {treesToRemove.Count}개의 나무가 파괴되었습니다.");
    }
}
