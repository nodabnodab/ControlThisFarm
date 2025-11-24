using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forever_ChaseCamera : MonoBehaviour
{
    private void LateUpdate() //플레이어 이동이 선행되고 그다음에 따라가야만 한다. 
    {
        Vector3 pos = transform.position; //변수를 만들고
        pos.z = -10; //멀리서 이곳을 바라볼 수 있게. 
        Camera.main.gameObject.transform.position = pos; //플레이어 위치를 카메라 위치에 넣는다. 
    }
}
