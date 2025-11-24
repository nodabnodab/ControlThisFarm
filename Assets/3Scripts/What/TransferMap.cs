using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //라이브러리 

public class TransferMap : MonoBehaviour
{
    public string transferMapName;

    private PlayerMoving thePlayer;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMoving>(); //하이어라키에 있는 모든 객체의 <> 컴포넌트를 검색해서 리턴.
                                                      //그에 반에 Getcompo는 해당 스크립트가 적용된 객체의 <> 컴포넌트를 검색해서 리턴한다.
                                                      //검색 범위의 차이로 이해하면 된다.                                                              
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }

    }
}
