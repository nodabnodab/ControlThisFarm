using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartScene : MonoBehaviour
{
    // 이 메서드는 버튼 클릭 시 호출됩니다.
    public void OnButtonClick()
    {
        // 씬 이름을 "Start"로 지정하여 로드합니다.
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}
