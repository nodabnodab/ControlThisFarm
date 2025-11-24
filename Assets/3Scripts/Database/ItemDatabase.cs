using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    public int money = 0;
    public Text moneyText; // UI 텍스트 컴포넌트 연결

    private void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB;
    [Space(20)]
    public GameObject fieldItemPrefab;
    public Vector3[] pos;

    private void Start()
    {
        money = 1000;
        UpdateMoneyText(); // 초기 돈 보유량을 UI에 반영
    }

    private void Update()
    {
        UpdateMoneyText(); // 매 프레임마다 돈 보유량을 UI에 반영
    }

    // 돈 보유량 텍스트 업데이트 메서드
    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = money.ToString();
        }
        else
        {
            Debug.LogError("moneyText is not assigned!");
        }
    }
}
