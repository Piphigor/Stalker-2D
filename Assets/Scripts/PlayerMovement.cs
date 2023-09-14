using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float horizontal = 0;
    private float vertical = 0;
    private float prevX = 0;
    private float prevY = 0;

    [SerializeField] float runSpeed = 6.0f;
    private static readonly int animIsWalk = Animator.StringToHash("isWalk");
    private static readonly int animSpeedX = Animator.StringToHash("speedX");
    private static readonly int animSpeedY = Animator.StringToHash("speedY");

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update ()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

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

    private void FixedUpdate()
    {
        var move = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        if (move.x != 0 && move.y != 0) move *= 0.7f;
        rb.velocity = move;

    }
}
