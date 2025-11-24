using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MovingObject
{
    public float newSpeed = 3.0f;
    public string currentMapName;   //transferMap 스크립트에 있는 transferMapName 변수의 값을 저장한다. 
    public GameObject Player;

    void Start()
    {
        animator = GetComponent<Animator>(); //애니메이터를 불러오는 함수. 컴포넌트를 통제하자....
        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        bool isMoving = false;

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //프로그램상에 이미 저장된 키. 
        {

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z); //벡터 값에 저장. z 값은 필요없음...
            vector.Normalize(); //대각선 이동 속도를 동일하게 조정하자. 


            if (vector.x != 0) //x값이 0이 아니라면, 좌우 키가 눌린 상태라면, 다시말해 좌우 이동 상태라면.
            {
                transform.Translate(vector.x * newSpeed * Time.deltaTime, 0, 0); //public으로 지정된 speed의 값을 transform. 
                isMoving = true; //움직이는 중

            }   //time.deltaTime을 사용한다. 이것은 컴퓨터의 성능에 상관없이 일정한 속도로 유지시켜주는 역할을 한다. 
            if (vector.y != 0)
            {
                transform.Translate(0, vector.y * newSpeed * Time.deltaTime, 0);
                isMoving = true; //움직이는 중
            }

            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x); // 파라미터에 설정되어 있던 인수를 이 벡터 x가 받아준다. 
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true); //standing tree에서 walking tree로 상태 이전

        }

        if (!isMoving) //움직이는 상태가 아니라면
        {
            animator.SetBool("Walking", false); //반대로 walking에서 standing으로. 
        }
    }



}
