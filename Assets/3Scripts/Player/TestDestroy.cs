using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDestroy : MonoBehaviour
{


    public float destroyRadius = 1f; // 파괴 반경
    public int Damage = 35; // 통상적인 데미지

    private bool swordCan = true; // 칼 공격 가능 여부
    private bool axeCan = true; //도끼 가능 여부
    private bool sickleCan = true; //낫 가능 여부
    private ToolController toolController; // ToolController 참조 변수

    public GameObject itemPrefab;


    private void Start()
    {
        toolController = FindObjectOfType<ToolController>();
    }



    void Update()
    {
        // 농작물 파괴 또는 나무 공격
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (toolController.currentTool == ToolController.Tool.SwordMode)
            {
                if (swordCan)
                {
                    SwordDestroyNearby();
                    StartCoroutine(SwordCooldown());
                }
            }
            else if (toolController.currentTool == ToolController.Tool.AxeMode)
            {
                if (axeCan)
                {
                    AxeDestroyNearby();
                    StartCoroutine(AxeCooldown());
                }
            }
            else if (toolController.currentTool == ToolController.Tool.SickleMode)
            {
                if (sickleCan)
                {
                    SickleDestroyNearby();
                    StartCoroutine(SickleCooldown());
                }
            }

        }
    }

    void SwordDestroyNearby()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, destroyRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Livestock")) // Livestock 태그가 있는 오브젝트를 찾습니다.
            {

                    LivestockHealth livestockHP = hitCollider.GetComponent<LivestockHealth>();
                    if (livestockHP != null)
                    {
                        livestockHP.TakeDamage(Damage);
                        Debug.Log("가축에 데미지를 입혔습니다. 현재 체력: " + livestockHP.GetCurrentHealth());
                    }
                    break;

            }
            else if (hitCollider.CompareTag("Beast")) // Beast 태그가 있는 오브젝트를 찾습니다.
            {

                    BeastHealth BeastHP = hitCollider.GetComponent<BeastHealth>();
                    if (BeastHP != null)
                    {
                        BeastHP.TakeDamage(Damage);
                        Debug.Log("짐승에 데미지를 입혔습니다. 현재 체력: " + BeastHP.GetCurrentHealth());
                    }
                    break;

            }
        }
    }


    void AxeDestroyNearby()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, destroyRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Tree")) // Tree 태그가 있는 오브젝트를 찾습니다.
            {

                    TreeHealth treeHealth = hitCollider.GetComponent<TreeHealth>();
                    if (treeHealth != null)
                    {
                        treeHealth.TakeDamage(Damage);
                        Debug.Log("나무에 데미지를 입혔습니다. 현재 체력: " + treeHealth.GetCurrentHealth());
                    }
                    break; // 가장 가까운 나무만 검사합니다.

            }
        }
    }



    void SickleDestroyNearby()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, destroyRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Crop")) // Crop 태그가 있는 오브젝트를 찾습니다.
            {

                CropGrowth cropGrowth = hitCollider.GetComponent<CropGrowth>();
                CropHealth cropHealth = hitCollider.GetComponent<CropHealth>();
                
                if (cropGrowth.GetCurrentStage() == cropGrowth.growthStages.Length - 1)
                {
                    cropHealth.TakeDamage(Damage);
                }
                else
                {
                    Debug.Log("미성숙한 작물입니다.");
                    break; //미성숙한 작물이거나 작물이 멀리 있으면 그냥 break;
                }

                

            }
        }
    }


    IEnumerator SwordCooldown()
    {
        swordCan = false;
        yield return new WaitForSeconds(0.8f); // 0.8초 딜레이
        swordCan = true;
    }
    IEnumerator AxeCooldown()
    {
        axeCan = false;
        yield return new WaitForSeconds(1.5f); // 1.5초 딜레이
        axeCan = true;
    }
    IEnumerator SickleCooldown()
    {
        sickleCan = false;
        yield return new WaitForSeconds(0.05f); // 0.05초 딜레이
        sickleCan = true;
    }

    void OnDrawGizmosSelected()
    {
        // 파괴 반경을 시각적으로 표시합니다.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, destroyRadius);
    }





}
