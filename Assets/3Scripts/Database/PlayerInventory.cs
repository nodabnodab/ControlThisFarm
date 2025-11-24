using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    private Dictionary<string, int> items = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeInventory();
            Debug.Log("PlayerInventory: Instance가 설정되었습니다.");
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("PlayerInventory: 중복된 인스턴스가 발견되어 제거되었습니다.");
        }
    }

    public void InitializeInventory()
    {
        items["GoldCoin"] = 1000;
        items["Potion"] = 3;
        items["Potato"] = 1;

    }

    public Dictionary<string, int> GetItems()
    {
        return new Dictionary<string, int>(items);
    }




    public void AddItem(string itemName, int quantity = 1)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("AddItem: itemName이 null이거나 빈 문자열입니다.");
            return;
        }

        if (items.ContainsKey(itemName))
        {
            items[itemName] += quantity;
        }
        else
        {
            items[itemName] = quantity;
        }

        // UI 업데이트
        //InventoryUIManager inventoryUIManager = FindObjectOfType<InventoryUIManager>();
        //if (inventoryUIManager != null)
        //{
        //    inventoryUIManager.UpdateInventoryUI();
        //}

    }

    public int GetItemQuantity(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError("GetItemQuantity: itemName이 null이거나 빈 문자열입니다.");
            return 0;
        }
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }
}