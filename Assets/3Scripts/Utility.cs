using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static void DropItem(GameObject itemPrefab, Transform transform)
    {
        if (itemPrefab != null)
        {
            // 현재 위치에 아이템 생성
            Object.Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
