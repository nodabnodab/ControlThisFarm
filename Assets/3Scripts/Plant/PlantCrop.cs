using UnityEngine;
using System.Collections; // 코루틴을 위해 이 줄을 추가합니다.

public class PlantCrop : MonoBehaviour
{
    public GameObject whichCrop; // 생성할 prefab 설정
    private bool plantRange = false; // 식물을 심을 수 있는 범위 내에 있는지 여부
    private static bool isPlanting = false; // 현재 식물을 심고 있는지 여부를 체크합니다. static으로 선언하여 모든 인스턴스가 공유하도록 합니다.

    // 플레이어가 영역에 진입하면 호출됩니다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            plantRange = true;
            Debug.Log("식물을 심을 수 있는 범위에 들어왔습니다.");
        }
    }

    // 플레이어가 영역을 벗어나면 호출됩니다.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            plantRange = false;
            Debug.Log("식물을 심을 수 있는 범위에서 벗어났습니다.");
        }
    }

    // 매 프레임마다 호출됩니다.
    void Update()
    {
        // plantRange가 true 상태이고, 사용자가 Ctrl 키를 누르면 실행됩니다.
        if (plantRange && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && !isPlanting)
        {
            Plant();
        }
    }

    // 실제로 식물을 심는 메서드입니다.
    void Plant()
    {
        isPlanting = true; // 식물 심기 시작을 표시합니다.

        // 심으려는 위치 주변에 이미 존재하는 오브젝트를 검사합니다.
        // 여기서 0.5f는 검사할 범위의 반지름입니다. 필요에 따라 조정해주세요.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        bool alreadyPlanted = false;
        foreach (var collider in colliders)
        {
            // 이미 같은 prefab이 심어져 있는지 확인합니다.
            if (collider.gameObject.CompareTag("Crop"))
            {
                alreadyPlanted = true;
                Debug.Log("이미 식물이 심어져 있습니다.");
                break;
            }
        }

        // 식물이 심어져 있지 않다면, 새로 식물을 심습니다.
        if (!alreadyPlanted)
        {
            if (whichCrop != null)
            {
                Instantiate(whichCrop, transform.position, Quaternion.identity);
                Debug.Log("식물이 심어졌습니다.");
            }
            else
            {
                Debug.LogError("whichCrop이 설정되지 않았습니다.");
            }
        }

        // 다른 오브젝트들이 식물을 심는 것을 방지하기 위해 잠시 대기합니다.
        StartCoroutine(ResetIsPlanting());
    }

    // Coroutine을 사용하여 isPlanting을 재설정합니다.
    private IEnumerator ResetIsPlanting()
    {
        yield return new WaitForSeconds(0.1f); // 식물을 심는 동안 다른 식물이 심어지지 않도록 잠시 대기합니다.
        isPlanting = false; // 다시 식물을 심을 수 있도록 설정합니다.
    }
}
