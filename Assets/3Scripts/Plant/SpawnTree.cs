using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTree : MonoBehaviour
{
    public GameObject prefab; // 나무 prefab
    public float minTime = 2f; // 최소 스폰 간격
    public float maxTime = 5f; // 최대 스폰 간격
    public Transform[] spawnPoints; // 나무가 출현할 위치 배열
    public int max = 20; // 최대 나무 수
    public Slider treeSlider; // 슬라이더 UI 참조
    //public Text treeCountText; // 텍스트 UI 참조

    public HashSet<Vector3> occupiedPositions = new HashSet<Vector3>(); // 점유된 위치를 저장하는 HashSet

    void Start()
    {
        treeSlider.minValue = 0;
        treeSlider.maxValue = max;
        UpdateTreeUI();
        StartCoroutine(SpawnTrees());
    }

    IEnumerator SpawnTrees()
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
            Create();
        }
    }

    void Create()
    {
        if (GetCurrentTreeCount() >= max)
        {
            return;
        }

        List<Transform> availableSpawnPoints = new List<Transform>();

        // 사용 가능한 스폰 포인트를 찾습니다.
        foreach (var spawnPoint in spawnPoints)
        {
            if (!occupiedPositions.Contains(spawnPoint.position))
            {
                availableSpawnPoints.Add(spawnPoint);
            }
        }

        if (availableSpawnPoints.Count > 0)
        {
            // 무작위로 사용 가능한 스폰 포인트를 선택합니다.
            int i = Random.Range(0, availableSpawnPoints.Count);
            Transform selectedSpawnPoint = availableSpawnPoints[i];

            // 나무를 생성하고, 점유된 위치에 추가합니다.
            GameObject newTree = Instantiate(prefab, selectedSpawnPoint.position, Quaternion.identity);
            TreeGrowth treeGrowth = newTree.GetComponent<TreeGrowth>();
            treeGrowth.Initialize(this); // TreeGrowth 스크립트 초기화

            occupiedPositions.Add(selectedSpawnPoint.position);

            UpdateTreeUI();
        }
        else
        {
            Debug.Log("모든 위치에 이미 나무가 있습니다.");
        }
    }

    // 나무가 파괴될 때 점유된 위치를 제거하는 메서드
    public void RemoveOccupiedPosition(Vector3 position)
    {
        if (occupiedPositions.Contains(position))
        {
            occupiedPositions.Remove(position);
            UpdateTreeUI();
        }
    }

    void UpdateTreeUI()
    {
        treeSlider.value = GetCurrentTreeCount();
        //treeCountText.text = $"Trees: {GetCurrentTreeCount()}/{max}";
    }

    public int GetCurrentTreeCount()
    {
        // 현재 씬에 존재하는 나무 객체의 수를 반환
        return GameObject.FindGameObjectsWithTag("Tree").Length;
    }
}
