using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivestockHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject itemPrefab; // 아이템 프리팹을 위한 변수

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Utility.DropItem(itemPrefab, transform); // 아이템을 떨어트리는 메서드 호출
            Destroy(gameObject);
            Debug.Log("도축 완료");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

