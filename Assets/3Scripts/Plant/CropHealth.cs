using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropHealth : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;
    public GameObject itemPrefab; // 드롭 아이템 프리팹을 위한 변수

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Utility.DropItem(itemPrefab, transform); // 드롭 아이템을 떨어트리는 메서드 호출
            Destroy(gameObject);
            Debug.Log("수확 완료");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }



}
