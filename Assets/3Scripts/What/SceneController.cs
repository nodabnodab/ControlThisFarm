using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{

    private static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitForTimeManager());
    }

    private IEnumerator WaitForTimeManager()
    {
        // TimeManager가 초기화될 때까지 기다립니다.
        while (TimeManager.Instance == null)
        {
            yield return null; // 다음 프레임까지 기다립니다.
        }

        float savedTime = SaveSystem.LoadGameTime();
        TimeManager.Instance.SetGameTime(savedTime);
        Debug.Log("SceneController: TimeManager의 게임 시간을 설정했습니다. (" + savedTime + ")");
    }
}
