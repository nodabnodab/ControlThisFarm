using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RabbitManager : MonoBehaviour
{
    public Transform[] points;
    public GameObject prefab;
    public float initialSpawnTime;
    public int max;
    public int firstThreshold = 6;
    public int secondThreshold = 14;
    public Slider rabbitSlider; // 슬라이더 UI 참조

    private float spawnTime;
    public int currentCount;

    public bool rabbitMaxDanger = false; //패배조건
    private float rabbitMaxDangerStartTime; //60초 경과
    public RabbitWarning rabbitWarning; // RabbitWarning 참조

    void Start()
    {
        spawnTime = initialSpawnTime;
        rabbitSlider.minValue = 0;
        rabbitSlider.maxValue = max;
        UpdateRabbitUI();
        StartCoroutine(SpawnBeastCoroutine());
    }

    private void Update()
    {
        if (rabbitMaxDanger)
        {
            if (Time.time - rabbitMaxDangerStartTime >= 60.0f)
            {
                GameOverManager.Instance.TriggerGameOver();
            }

            // SliderOutlineController의 outline 효과 발동
            rabbitWarning.StartOutlineEffect();
        }
        else
        {
            // SliderOutlineController의 outline 효과 비활성화
            rabbitWarning.StopOutlineEffect();
        }
    }

    IEnumerator SpawnBeastCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnRabbit();
            AdjustSpawnTime();
        }
    }

    void SpawnRabbit()
    {
        currentCount = GameObject.FindObjectsOfType<RabbitIdentifier>().Length;

        if (currentCount >= 2 && currentCount < max)
        {
            int i = Random.Range(0, points.Length);
            Instantiate(prefab, points[i].position, points[i].rotation);
            UpdateRabbitUI();
        }

        RabbitMax();

    }

    void AdjustSpawnTime()
    {
        currentCount = GameObject.FindObjectsOfType<RabbitIdentifier>().Length;

        if (currentCount >= secondThreshold)
        {
            spawnTime = initialSpawnTime / 3;
        }
        else if (currentCount >= firstThreshold)
        {
            spawnTime = initialSpawnTime / 2;
        }
        else
        {
            spawnTime = initialSpawnTime;
        }
    }

    void UpdateRabbitUI()
    {
        currentCount = GameObject.FindObjectsOfType<RabbitIdentifier>().Length;
        rabbitSlider.value = currentCount;
    }

    public void RabbitMax()
    {
        if (currentCount == max)
        {
            if (!rabbitMaxDanger)
            {
                rabbitMaxDanger = true;
                rabbitMaxDangerStartTime = Time.time;
            }
        }
        else
        {
            rabbitMaxDanger = false;
        }
    }
}
