using UnityEngine;

public class RabbitMovement : MovingObject
{
    public float minMoveTime = 0.3f;
    public float maxMoveTime = 3.5f;
    public float waitTime = 1.2f;

    private Vector2 moveDirection;
    private float moveTime;
    private float timer;
    private bool isMoving;

    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        SetRandomState();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isMoving)
        {
            Move();
            if (timer >= moveTime)
            {
                SetWaitState();
            }
        }
        else
        {
            if (timer >= waitTime)
            {
                SetMoveState();
            }
        }

        UpdateAnimationParameters();
    }

    void SetRandomState()
    {
        if (Random.Range(0, 2) == 0)
        {
            SetMoveState();
        }
        else
        {
            SetWaitState();
        }
    }

    void SetMoveState()
    {
        isMoving = true;
        timer = 0;
        moveTime = Random.Range(minMoveTime, maxMoveTime); // 1초에서 5초 사이의 랜덤 값 설정
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        animator.SetBool("Walking", true);
    }

    void SetWaitState()
    {
        isMoving = false;
        timer = 0;
        animator.SetBool("Walking", false);
    }

    void Move()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    void UpdateAnimationParameters()
    {
        animator.SetFloat("DirX", moveDirection.x);
        animator.SetFloat("DirY", moveDirection.y);
    }
}