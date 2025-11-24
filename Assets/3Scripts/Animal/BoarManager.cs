using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoarManager : MonoBehaviour
{
    public Transform[] points;
    public GameObject prefab;
    public float spawnTime;
    public int max;
    public Slider boarSlider; // 슬라이더 UI 참조
    //public Text boarCountText; // 텍스트 UI 참조

    public int currentCount;

    public bool boarMaxDanger = false; //패배조건
    private float boarMaxDangerStartTime; //60초 경과
    public BoarWarning boarWarning;

    void Start()
    {
        boarSlider.minValue = 0;
        boarSlider.maxValue = max;
        UpdateBoarUI();
        StartCoroutine(SpawnBeastCoroutine());
    }

    private void Update()
    {
        if (boarMaxDanger)
        {
            if (Time.time - boarMaxDangerStartTime >= 60.0f)
            {
                GameOverManager.Instance.TriggerGameOver();
                boarWarning.StartOutlineEffect();
            }
            else
            {
                // SliderOutlineController의 outline 효과 비활성화
                boarWarning.StopOutlineEffect();
            }
        }
    }

    IEnumerator SpawnBeastCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnBoar();
        }
    }

    void SpawnBoar()
    {
        currentCount = GameObject.FindObjectsOfType<BoarIdentifier>().Length;

        //if (currentCount >= 2 && currentCount < max)
        if (currentCount < max)
        {
            int i = Random.Range(0, points.Length);
            Instantiate(prefab, points[i].position, points[i].rotation);
            UpdateBoarUI();
        }
        BoarMax();
    }

    void UpdateBoarUI()
    {
        currentCount = GameObject.FindObjectsOfType<BoarIdentifier>().Length;
        boarSlider.value = currentCount;
        //boarCountText.text = $"Boars: {currentCount}/{max}";
    }

    public void BoarMax()
    {
        if (currentCount == max)
        {
            if (!boarMaxDanger)
            {
                boarMaxDanger = true;
                boarMaxDangerStartTime = Time.time;
            }
        }
        else
        {
            boarMaxDanger = false;
        }
    }
}
