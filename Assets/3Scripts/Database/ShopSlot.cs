using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerUpHandler
{
    public int slotnum;
    public Item item;
    public Image itemIcon;
    public bool soldOut = false;
    InventoryUI inventoryUI;

    public void Init(InventoryUI Iui)
    {
        inventoryUI = Iui;
    }

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
        if (soldOut )
            itemIcon.color = new Color(0.9f, 0.9f, 0.9f, 0.5f);

    }

    public void RemoveSlot()
    {
        item = null;
        soldOut = false;
        itemIcon.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (item != null)
        {
            if (ItemDatabase.instance.money >= item.itemCost && !soldOut && Inventory.Instance.items.Count <Inventory.Instance.SlotCnt)
            {
                ItemDatabase.instance.money -= item.itemCost;
                Inventory.Instance.AddItem(item);
                soldOut = true;
                inventoryUI.Buy(slotnum);
                UpdateSlotUI();
            }
        }

    }


}
