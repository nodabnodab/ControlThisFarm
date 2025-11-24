using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject panel; // Panel 오브젝트를 드래그하여 할당
    public GameObject itemTextPrefab; // Text 프리팹을 드래그하여 할당


    //private void Start()
    //{
    //    UpdateInventoryUI();
    //}

    //public void UpdateInventoryUI()
    //{
    //    // 현재 패널의 모든 자식 오브젝트를 제거합니다.
    //    foreach (Transform child in panel.transform)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    // 아이템 목록을 가져와 UI를 업데이트합니다.
    //    Dictionary<string, int> items = PlayerInventory.Instance.GetItems();
    //    foreach (var item in items)
    //    {
    //        GameObject itemTextObj = Instantiate(itemTextPrefab, panel.transform);
    //        Text itemText = itemTextObj.GetComponent<Text>();
    //        itemText.text = $"{item.Key}: {item.Value}";
    //    }
    //}
}
