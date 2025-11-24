using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityTools : MonoBehaviour
{
    private ToolController toolController; //툴 스크립트 참조
    public PlayerHealth playerHealth; //체력 스크립트 참조
    private bool potionCan = true; //포션 쿨타임

    void Start()
    {
        toolController = FindObjectOfType<ToolController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (toolController.currentTool == ToolController.Tool.PotionHold)
            {
                if (playerHealth.playerHp >= 100) //100 이상이라면 
                {
                    Debug.Log("이미 최대 체력입니다. ");
                    return;
                }



                else //100이하라면
                {
                    int potionCount = PlayerInventory.Instance.GetItemQuantity("Potion"); // 포션 개수 가져오기

                    if (potionCount <= 0) // 포션이 없다면
                    {
                        Debug.Log("포션이 부족합니다.");
                        return;
                    }
                    else //포션이 있음. 
                    {
                        if (potionCan) //쿨타임만 아니라면
                        {
                            DrinkingPotion();
                            StartCoroutine(PotionCoolDown());
                        }

                        else //쿨타임이라면
                        {
                            Debug.Log("대기시간 중입니다. ");
                        }
                    }

                }

            }
        }
    }

    void DrinkingPotion()
    {
        playerHealth.CureHP(30);
        PlayerInventory.Instance.AddItem("Potion", -1); // 포션 개수 감소
        Debug.Log("남은 포션: " + PlayerInventory.Instance.GetItemQuantity("Potion"));
    }

    IEnumerator PotionCoolDown()
    {
        potionCan = false;
        yield return new WaitForSeconds(4.0f); // 4초 딜레이
        potionCan = true;
    }

}
