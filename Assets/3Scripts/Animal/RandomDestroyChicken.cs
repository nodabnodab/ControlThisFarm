using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroyChicken : MonoBehaviour
{
    public float baseDestructionProbability = 0f; // 기본 파괴 확률
    public float increasePerCat = 0.005f; // 고양이 한 마리당 증가하는 파괴 확률
    public float checkInterval = 10f; // 닭 파괴를 체크하는 간격 시간

    private CatManager catManager; // CatManager 참조

    void Start()
    {
        catManager = FindObjectOfType<CatManager>(); // CatManager 스크립트를 찾습니다.
        StartCoroutine(DestroyChickenRoutine());
    }

    IEnumerator DestroyChickenRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            TryDestroySelf();
        }
    }

    void TryDestroySelf()
    {
        // 현재 고양이 수를 가져옵니다.
        int currentCatCount = catManager.currentCount;

        // 파괴 확률을 계산합니다.
        float destroyProbability = baseDestructionProbability + (increasePerCat * currentCatCount);

        // 랜덤 값에 따라 자신을 파괴합니다.
        if (Random.value < destroyProbability)
        {
            Debug.Log("불쌍한 닭이 고양이에게 무참히 잡아먹혔습니다. ");
            Destroy(gameObject);
        }

    }
}
