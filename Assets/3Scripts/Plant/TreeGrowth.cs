using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    public Sprite[] growthStages; // 나무 성장 단계 이미지
    private int currentStage = 0; // 현재 성장 단계
    private SpriteRenderer spriteRenderer; // Sprite Renderer 참조
    public float waitTime = 5f; // 성장 간 대기 시간 (public으로 선언하여 Unity 인스펙터에서 수정 가능)
    private SpawnTree spawnTree; //나무 생성 변수

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Sprite Renderer 컴포넌트 가져오기
        StartCoroutine(Grow()); // 성장 코루틴 시작
    }

    IEnumerator Grow()
    {
        while (currentStage < growthStages.Length)
        {
            spriteRenderer.sprite = growthStages[currentStage]; // 현재 단계의 이미지로 업데이트
            yield return new WaitForSeconds(waitTime); // 설정된 대기 시간만큼 대기 (인스펙터에서 수정 가능)
            if (currentStage < growthStages.Length - 1) // 마지막 단계에 도달하지 않았을 때만 증가
            {
                currentStage++; // 다음 성장 단계로
            }
        }
    }

    public void Initialize(SpawnTree spawnTree)
    {
        this.spawnTree = spawnTree;
    }

    void OnDestroy()
    {
        if (spawnTree != null)
        {
            spawnTree.RemoveOccupiedPosition(transform.position);
        }
    }

    public int GetCurrentStage()
    {
        return currentStage;
    }


}
