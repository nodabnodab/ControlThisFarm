using UnityEngine.UI;
using UnityEngine;

public class BeastHealth : MonoBehaviour
{
    public int maxHealth = 70;
    private int currentHealth;
    public GameObject itemPrefab; // 아이템 프리팹을 위한 변수
    public GameObject itemPrefab_2;
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

            if (Random.Range(0, 100) < 10)
            {
                Utility.DropItem(itemPrefab_2, transform);
                Debug.Log("이것은?");
            }
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}