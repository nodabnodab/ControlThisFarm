using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    public enum Tool
    {
        None,
        SwordMode,
        PotionHold,
        AxeMode,
        SickleMode
    }

    public Tool currentTool = Tool.None;

    public GameObject ToolOn1;
    public GameObject ToolOn2;
    public GameObject ToolOn3;
    public GameObject ToolOn4;

    private Tool previousTool = Tool.None; // 이전 상태를 저장할 변수

    void Update()
    {
        CheckInput();
        if (previousTool != currentTool) // 상태가 변경되었을 때만 DisplayCurrentTool 호출
        {
            DisplayCurrentTool();
            previousTool = currentTool; // 이전 상태 업데이트
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTool = Tool.SwordMode;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTool = Tool.PotionHold;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentTool = Tool.AxeMode;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentTool = Tool.SickleMode;
        }
    }

    private void DisplayCurrentTool()
    {
        ToolOn1.SetActive(false);
        ToolOn2.SetActive(false);
        ToolOn3.SetActive(false);
        ToolOn4.SetActive(false);

        switch (currentTool)
        {
            case Tool.SwordMode:
                ToolOn1.SetActive(true);
                Debug.Log("허름한 칼");
                break;
            case Tool.PotionHold:
                ToolOn2.SetActive(true);
                Debug.Log("응급 조치");
                break;
            case Tool.AxeMode:
                ToolOn3.SetActive(true);
                Debug.Log("괜찮은 벌목 도끼");
                break;
            case Tool.SickleMode:
                ToolOn4.SetActive(true);
                Debug.Log("조선 낫");
                break;
            default:
                Debug.Log("아무 것도 들고있지 않습니다. ");
                break;
        }
    }
}
