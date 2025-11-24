using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    public GameObject gameOverPanel;
    public PlayerHealth playerHealth;
    bool activeGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameOverPanel.SetActive(activeGameOver);
    }

    void Update()
    {
        if (playerHealth != null && playerHealth.IsDead && !activeGameOver)
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        if (!activeGameOver)
        {
            activeGameOver = !activeGameOver;
            gameOverPanel.SetActive(activeGameOver);

            Time.timeScale = 0f; // 게임 시간 정지
        }
    }
}
