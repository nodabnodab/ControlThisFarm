using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;


    bool isSound;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;

    //public CanvasGroup newGameNameGroup;

    public void OnBtnClick()
    {
        switch(currentType)
        {
            case BTNType.New:
                //CanvasGroupOn(newGameNameGroup);
                Debug.Log("새 게임");
                //Time.timeScale = 1f; // 시간 복원
                SceneLoader.LoadSceneHandle("Main", 0);
                break;
            case BTNType.Continue:
                Debug.Log("계속하기");
                SceneLoader.LoadSceneHandle("Main", 1);
                break;

            case BTNType.Option:
                Debug.Log("옵션");
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNType.Sound:
                if(isSound)
                {
                    Debug.Log("음소거");
                }
                else
                {
                    Debug.Log("켜기");
                }
                isSound = !isSound;
                break;

            case BTNType.Back:
                Debug.Log("뒤로가기");
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;

            //case BTNType.Done:
            //    Debug.Log("완료");
            //    SceneLoader.LoadSceneHandle("Main", 0);
            //    break;


            case BTNType.Quit:
                Application.Quit();
                Debug.Log("끝내기");
                break;
        }
    }
    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }


    //public void OnPointerEnter(PointerEventData eventData)
    //{

    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{

    //}
}
