using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private float gameTime; // 게임의 전체 시간을 초 단위로 저장
    private List<TimeEvent> timeEvents = new List<TimeEvent>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("TimeManager 인스턴스가 초기화되었습니다.");
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        float currentTime = GetGameTime();
        for (int i = timeEvents.Count - 1; i >= 0; i--)
        {
            if (timeEvents[i].triggerTime <= currentTime)
            {
                timeEvents[i].callback.Invoke();
                timeEvents.RemoveAt(i);
            }
        }
    }

    public float GetGameTime()
    {
        return Time.time;
    }

    public void SetGameTime(float time)
    {
        gameTime = time; // 게임 시간을 설정합니다.
    }


    public void RegisterTimeEvent(float delay, Action callback)
    {
        float triggerTime = GetGameTime() + delay;
        timeEvents.Add(new TimeEvent(triggerTime, callback));
    }

    private class TimeEvent
    {
        public float triggerTime;
        public Action callback;

        public TimeEvent(float triggerTime, Action callback)
        {
            this.triggerTime = triggerTime;
            this.callback = callback;
        }
    }




    //private void Update()
    //{
    //    gameTime += Time.deltaTime;

    //    // 시간 이벤트 체크
    //    for (int i = timeEvents.Count - 1; i >= 0; i--)
    //    {
    //        if (gameTime >= timeEvents[i].triggerTime)
    //        {
    //            timeEvents[i].action.Invoke();
    //            timeEvents.RemoveAt(i);
    //        }
    //    }
    //}

    //public void RegisterTimeEvent(float delay, Action action)
    //{
    //    float triggerTime = gameTime + delay;
    //    timeEvents.Add(new TimeEvent(triggerTime, action));
    //}

    //public float GetGameTime()
    //{
    //    return gameTime;
    //}

    //public void SetGameTime(float newGameTime)
    //{
    //    gameTime = newGameTime;
    //}


    //[Serializable]
    //private class TimeEvent
    //{
    //    public float triggerTime;
    //    public Action action;

    //    public TimeEvent(float triggerTime, Action action)
    //    {
    //        this.triggerTime = triggerTime;
    //        this.action = action;
    //    }
    //}


}