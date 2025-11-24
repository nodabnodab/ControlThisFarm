using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = 1.2f;
    public float runSpeed = 4.0f;
    protected Vector3 vector; // z는 2d에서 필요없지만 일단 3로 하자. 
    public int walkCount;
    protected int currentWalkCount;
    public BoxCollider2D boxCollider;
    public Rigidbody2D rbody;
    public LayerMask layerMask;
    public Animator animator;


    //아래는 솔직히 필요 없음. 
    protected void Move(string _dir, int _frequency)
    {
        StartCoroutine(MoveCoroutine(_dir, _frequency));
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        vector.Set(0, 0, vector.z); 

        switch (_dir)
        {
            case "UP":
                vector.y = 1f;
                break;

            case "DOWN":
                vector.y = -1f;
                break;

            case "RIGHT":
                vector.x = 1f;
                break;

            case "LEFT":
                vector.x = -1f;
                break;
        }
        animator.SetFloat("DirX", vector.x);
        animator.SetFloat("DirY", vector.x);
        animator.SetBool("Walking", true);

        while (currentWalkCount < walkCount)
        {
            transform.Translate(vector.x * speed, vector.y * speed, 0);
            
            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }
        currentWalkCount = 0;
        if(_frequency != 5)
            animator.SetBool("Walking", false);
    }

}
