using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearManager : MonoBehaviour
{
    public GameObject gameClearPanel;

    bool activeGameClear = false;

    void Start()
    {
        gameClearPanel.SetActive(activeGameClear);
    }

    void Update()
    {
        if (ItemDatabase.instance.money >= 3000 && !activeGameClear)
        {
            activeGameClear = !activeGameClear;
            gameClearPanel.SetActive(activeGameClear);

            Time.timeScale = 0f; // 게임 시간 정지

        }
    }
}
