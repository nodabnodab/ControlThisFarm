using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoarHealth : MonoBehaviour
{
    public int maxHealth = 70;
    private int currentHealth;
    public GameObject itemPrefab; // 아이템 프리팹을 위한 변수
    public RawImage imgBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        float healthPercentage = (float)currentHealth / maxHealth; // 현재 체력의 비율을 계산
        imgBar.transform.localScale = new Vector3(healthPercentage, 1, 1); // 비율에 따라 체력 바의 크기 조절

        if (currentHealth <= 0)
        {
            Utility.DropItem(itemPrefab, transform); // 아이템을 떨어트리는 메서드 호출
            Destroy(gameObject);
            Debug.Log("사냥 성공");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}