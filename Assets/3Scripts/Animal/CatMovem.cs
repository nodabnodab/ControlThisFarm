using UnityEngine;

public class CatMovem : MovingObject
{
    public float minMoveTime = 1.5f;
    public float maxMoveTime = 8.0f;
    public float waitTime = 5.0f;
    public float escapeDistance = 4.0f;
    public float detectDistance = 1.5f;

    private Vector2 moveDirection;
    private float moveTime;
    private float timer;
    private bool isMoving;
    private bool isEscaping;
    private GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        player = GameObject.FindGameObjectWithTag("Player");
        SetRandomState();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= detectDistance)
        {
            isEscaping = true;
        }

        if (isEscaping)
        {
            Escape();
        }
        else
        {
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
        moveTime = Random.Range(minMoveTime, maxMoveTime);
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

    void Escape()
    {
        Vector3 dir = (transform.position - player.transform.position).normalized;
        transform.Translate(dir * runSpeed * Time.deltaTime);
        animator.SetBool("Running", true); // Run 애니메이션 시작

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            animator.SetFloat("DirX", dir.x);
            animator.SetFloat("DirY", 0);
        }
        else
        {
            animator.SetFloat("DirX", 0);
            animator.SetFloat("DirY", dir.y);
        }

        if (Vector3.Distance(transform.position, player.transform.position) > escapeDistance)
        {
            isEscaping = false;
            animator.SetBool("Running", false); // Run 애니메이션 종료
            SetRandomState();
        }
    }

    void UpdateAnimationParameters()
    {
        if (!isEscaping)
        {
            animator.SetFloat("DirX", moveDirection.x);
            animator.SetFloat("DirY", moveDirection.y);
        }
    }
}
