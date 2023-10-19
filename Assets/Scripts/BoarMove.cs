using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarMove : MonoBehaviour
{
    public float speed;
    private float waitTime;           //время отдыха между передвижениями
    public float startWaitTime;
    bool wait = false;
    Vector3 moveTarget;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private static readonly int animIsWalk = Animator.StringToHash("isWalk");
    private static readonly int animSpeedX = Animator.StringToHash("speedX");
    private static readonly int animSpeedY = Animator.StringToHash("speedY");
    private float horizontal = 0;
    private float vertical = 0;
    private float prevX = 0;
    private float prevY = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        waitTime = startWaitTime;
        //transform.eulerAngles = new Vector3(0, 0, 0);
        //transform.position = new Vector3(0, 0, 0);      //стартуем в центре карты
    }

    void Update()
    {
        if (wait)
        {
            waitTime -= Time.deltaTime;
            if (waitTime < 0)
            {
                horizontal = Random.Range(-10f, 10f);
                vertical = Random.Range(-10f, 10f);
                wait = false;
                moveTarget = new Vector3(horizontal, vertical, 0);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, moveTarget) < 0.5f)
            {
                wait = true;
                waitTime = startWaitTime;
            }
        }
        if (prevX == horizontal && prevY == vertical) return;
        prevX = horizontal;
        prevY = vertical;

        animator.SetBool(animIsWalk, horizontal != 0 || vertical != 0);
        if (horizontal > 0) spriteRenderer.flipX = false;
        else if (horizontal < 0) spriteRenderer.flipX = true;
        animator.SetInteger(animSpeedX, horizontal != 0 ? 1 : 0);

        if (vertical > 0) animator.SetInteger(animSpeedY, 1);
        else if (vertical < 0) animator.SetInteger(animSpeedY, -1);
        else animator.SetInteger(animSpeedY, 0);
    }
}