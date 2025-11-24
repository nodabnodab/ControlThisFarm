using UnityEngine;

public class TreeHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject itemPrefab; // 드롭 아이템 프리팹을 위한 변수
    public GameObject itemPrefab_2;
    private TreeGrowth treeGrowth; // TreeGrowth 스크립트 참조

    void Start()
    {
        currentHealth = maxHealth;
        treeGrowth = GetComponent<TreeGrowth>(); // TreeGrowth 컴포넌트 가져오기
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (treeGrowth != null && treeGrowth.GetCurrentStage() == 3) // 나무가 4번째 단계일 경우 (인덱스 3)
            {
                Utility.DropItem(itemPrefab, transform); // 드롭 아이템을 떨어트리는 메서드 호출
                Debug.Log("나무 떨어짐");

                if (Random.Range(0, 100) < 10)
                {
                    Utility.DropItem(itemPrefab_2, transform);
                    Debug.Log("올리브가 나왔다!");
                }
            }
            Destroy(gameObject);
            Debug.Log("벌목 완료");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
