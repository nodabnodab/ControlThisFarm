using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESC : MonoBehaviour
{
    public GameObject escPanel;
    bool activeSaveAndLoad = false;

    void Start()
    {
        escPanel.SetActive(activeSaveAndLoad);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            activeSaveAndLoad = !activeSaveAndLoad;
            escPanel.SetActive(activeSaveAndLoad);

            if (activeSaveAndLoad)
            {
                Time.timeScale = 0f; // 게임 시간 정지
            }
            else
            {
                Time.timeScale = 1f; // 게임 시간 흐름
            }
        }
    }
}
