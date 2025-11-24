using UnityEngine;

public static class SaveSystem
{
    public static float LoadGameTime()
    {
        // 저장된 게임 시간을 반환합니다. 예를 들어, PlayerPrefs를 사용할 수 있습니다.
        return PlayerPrefs.GetFloat("GameTime", 0f);
    }

    public static void SaveGameTime(float time)
    {
        // 게임 시간을 저장합니다.
        PlayerPrefs.SetFloat("GameTime", time);
        PlayerPrefs.Save();
    }
}