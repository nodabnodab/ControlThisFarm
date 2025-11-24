using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatManager : MonoBehaviour
{
    public Transform[] points;
    public GameObject prefab;
    public float initialSpawnTime;
    public int max;
    public int firstThreshold = 6;
    public int secondThreshold = 14;
    public Slider catSlider; // 슬라이더 UI 참조
    //public Text catCountText; // 텍스트 UI 참조

    private float spawnTime;
    public int currentCount;

    public bool catMaxDanger = false; //패배조건
    private float catMaxDangerStartTime; //60초 경과
    public CatWarning catWarning;

    void Start()
    {
        spawnTime = initialSpawnTime;
        catSlider.minValue = 0;
        catSlider.maxValue = max;
        UpdateCatUI();
        StartCoroutine(SpawnBeastCoroutine());
    }

    private void Update()
    {
        if (catMaxDanger)
        {
            if (Time.time - catMaxDangerStartTime >= 60.0f)
            {
                GameOverManager.Instance.TriggerGameOver();
            }
            catWarning.StartOutlineEffect();
        }
        else
        {
            // SliderOutlineController의 outline 효과 비활성화
            catWarning.StopOutlineEffect();
        }
    }

    IEnumerator SpawnBeastCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnCat();
            AdjustSpawnTime();
        }
    }

    void SpawnCat()
    {
        currentCount = GameObject.FindObjectsOfType<CatIdentifier>().Length;

        if (currentCount >= 2 && currentCount < max)
        {
            int i = Random.Range(0, points.Length);
            Instantiate(prefab, points[i].position, points[i].rotation);
            UpdateCatUI();
        }

        CatMax();
    }

    void AdjustSpawnTime()
    {
        currentCount = GameObject.FindObjectsOfType<CatIdentifier>().Length;

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

    void UpdateCatUI()
    {
        currentCount = GameObject.FindObjectsOfType<CatIdentifier>().Length;
        catSlider.value = currentCount;
        //catCountText.text = $"Cats: {currentCount}/{max}";
    }

    public void CatMax()
    {
        if (currentCount == max)
        {
            if (!catMaxDanger)
            {
                catMaxDanger = true;
                catMaxDangerStartTime = Time.time;
            }
        }
        else
        {
            catMaxDanger = false;
        }
    }
}
