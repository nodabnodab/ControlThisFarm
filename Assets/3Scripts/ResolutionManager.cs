using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    //게임 해상도를 결정하는 스크립트. 
    void Start()
    {
        // 해상도 설정
        Screen.SetResolution(1600, 900, false);
    }
}
