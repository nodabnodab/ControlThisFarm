using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroyCrop : MonoBehaviour
{
    private float checkInterval = 10f; // 주기적으로 확인할 시간 간격 걍 private로 함. 
    public float baseDestructionProbability = 0f; // 기본 파괴 확률 (0 ~ 1 사이의 값, 0.1은 10% 확률)
    public float increasePerBoar = 0.5f; //멧돼지에 따른 파괴 확률 지정
    private BoarManager boarManager;
    //yield return new WaitForSeconds(checkInterval);

    void Start()
    {
        boarManager = FindObjectOfType<BoarManager>(); // BoarManager 찾기
        StartCoroutine(DestroyCropRoutine());
    }

    IEnumerator DestroyCropRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);


            if (boarManager != null)
            {
                float currentDestructionProbability = baseDestructionProbability + (boarManager.currentCount * increasePerBoar);
                float randomValue = Random.Range(0f, 100f);
                if (randomValue < currentDestructionProbability)
                {
                    // 농작물이 파괴되는 경우
                    Debug.Log("멧돼지가 농작물을 헤치웁니다. 꺼억. ");
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogWarning("BoarManager를 찾을 수 없습니다.");
            }


            //int boarCount = GameObject.FindObjectsOfType<BoarIdentifier>().Length;
            //float destroyChance = baseDestructionProbability * boarCount; // 멧돼지 숫자에 따라 파괴 확률 증가

            //if (Random.value < destroyChance)
            //{
            //    Destroy(gameObject);
            //    Debug.Log("이 스크립트가 멧돼지에 의해 파괴되었습니다.");
            //    yield break; // 파괴되면 코루틴을 종료합니다.
            //}
        }
    }
}