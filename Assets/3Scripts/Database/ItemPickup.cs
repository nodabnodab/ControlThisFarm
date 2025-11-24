using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
    public GameObject itemName; // 이제 GameObject를 참조합니다.
    public float destroyTime = 180.0f; // 아이템이 파괴될 시간 (초 단위)
    public SpriteRenderer image;


    private void Start()
    {
        StartCoroutine(DestroyAfterTime(destroyTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            // 플레이어의 인벤토리 관련 스크립트에 접근하여 아이템 추가 로직을 수행합니다.
            // 예를 들어, PlayerInventory 클래스의 AddItem 메소드를 호출합니다.
            // 이 예제에서는 GameObject를 인자로 넘기는 방식으로 변경했습니다.
        if (other.CompareTag("Player")) // 플레이어와 충돌하는지 확인합니다.
        {
            if (PlayerInventory.Instance != null)
            {
                PlayerInventory.Instance.AddItem(itemName.name); // 메소드가 GameObject를 받도록 수정해야 합니다. GameObject의 이름을 전달합니다.
                Debug.Log("ItemPickup: Item picked up and destroyed.");
                Destroy(gameObject); // 아이템 객체를 제거합니다.
            }
            else
            {
                Debug.LogError("ItemPickup: PlayerInventory 인스턴스를 찾을 수 없습니다.");
            }
        }

    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (this != null) // 오브젝트가 null 상태가 아니라면 파괴
        {
            Destroy(gameObject);
        }
    }


}
