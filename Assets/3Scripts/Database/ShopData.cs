using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopData : MonoBehaviour
{
    public List<Item>stocks = new List<Item>();
    public bool[] soldOuts;

    void Start()
    {
        stocks.Add(ItemDatabase.instance.itemDB[14]); //비료
        stocks.Add(ItemDatabase.instance.itemDB[18]); //사료 18번이어야하는데 비워두고. 일단 물약으로 대체
        stocks.Add(ItemDatabase.instance.itemDB[15]); //석회가루
        stocks.Add(ItemDatabase.instance.itemDB[16]); //전기톱
        stocks.Add(ItemDatabase.instance.itemDB[0]); //물약
        stocks.Add(ItemDatabase.instance.itemDB[17]); //창

        soldOuts = new bool[stocks.Count];
        for (int i = 0; i < soldOuts.Length; i++)
        {
            soldOuts[i] = false;
        }

    }
}
