using System.Collections;
using UnityEngine;

public class SpawnBeast : MonoBehaviour
{
    public Transform[] points;
    public GameObject prefab;
    public float spawnTime;
    public int max;
    private int currentCount;

    void Start()
    {
        // 주기적으로 SpawnRabbit 메서드를 호출합니다.
        StartCoroutine(SpawnRabbitCoroutine());
    }

    IEnumerator SpawnRabbitCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnBeasts();
        }
    }

    void SpawnBeasts()
    {
        currentCount = GameObject.FindObjectsOfType<RabbitIdentifier>().Length;

        if (currentCount >= 2 && currentCount < max)
        {
            int i = Random.Range(0, points.Length);
            Instantiate(prefab, points[i].position, points[i].rotation);
        }
    }
}
