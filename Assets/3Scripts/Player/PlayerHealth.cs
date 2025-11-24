using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 클래스를 사용하기 위해 필요

public class PlayerHealth : MonoBehaviour
{
    public int playerHp = 100;
    public Slider healthSlider; // 체력바로 사용할 Slider 참조
    public RawImage imgDamage;
    public RawImage imgCure;
    bool bDamage;
    bool bCure;
    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
    }


    public void SavePlayer() //저장용
    {
        SaveData save = new SaveData();
        save.HP = playerHp;
        save.x = transform.position.x;
        save.y = transform.position.y;
        save.z = transform.position.z;
        SaveManager.Save(save);
    }

    public void LoadPlayer() //백업 로드용
    {
        SaveData save = SaveManager.Load();
        playerHp = save.HP;
        transform.position = new Vector3(save.x, save.y, save.z);
    }




    void Start()
    {
        // 시작 시 Slider의 최대값과 현재값을 초기화
        healthSlider.maxValue = playerHp;
        healthSlider.value = playerHp;
    }

    public void SetHP(int value) //매개변수 하나 넣어주고.. 외부에서 100이 들어오면 hp를 100으로 만들어버릴 것이다. 
                                 //bar 설정도 잊지 않는다. 
    {
        if (value < 0 || value > 100)
        {
            return;
        }
        playerHp += value;
        healthSlider.value = playerHp;
    }


    private void Update()
    {
        if (bDamage) //위에서 선언한 if bDamage가 true라면, 0이 아닌 값이라면
        {
            imgDamage.color = new Color(1, 0, 0, 0.5f); //rgba 내가 컬러 설정가능. a를 원래 1로 해야하는데
                                                        //나는 0.5로 하려고.
        }
        else
        {
            imgDamage.color = Color.Lerp(imgDamage.color, Color.clear, 5 * Time.deltaTime); //fade out 하는 느낌으로 만들어보자.
                                                                                            //a 색상에서 b 색상으로 넘어가는 시간.
        }
        bDamage = false;


        if (bCure)
        {
            imgCure.color = new Color(0, 1, 0, 0.3f); // 녹색 (RGBA: 0, 1, 0)이며 투명도는 0.7
        }
        else
        {
            imgCure.color = Color.Lerp(imgCure.color, Color.clear, 0.5f * Time.deltaTime);
        }
        bCure = false;

    }

    public void CureHP(int amount)
    {
        if (playerHp <= 0)
        {
            return;
        }
        playerHp += amount;

        if (playerHp > 100)
        {
            playerHp = 100;
        }
        bCure = true;
        healthSlider.value = playerHp;

    }


    public void GotDamage(int amount)
    {
        if (playerHp <= 0)
        {
            return;
        }

        playerHp -= amount;
        bDamage = true; //공격을 받으면 dDamage를 true로 바꿔준다. 
        healthSlider.value = playerHp; // 체력바 업데이트

        if (playerHp <= 0)
        {
            
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<PlayerMoving>().enabled = false;
            GetComponent<TestDestroy>().enabled = false;
            isDead = true;
        }
    }

}
