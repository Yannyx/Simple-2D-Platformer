using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 6f;
    private float jumpPower = 9f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    
    [SerializeField] private Animator anim;
    private string RUN_ANIMATION = "Run";
    private string JUMP_ANIMATION = "Jump";

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();
        AnimatePlayer();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            anim.SetBool(JUMP_ANIMATION, true);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    void AnimatePlayer()
    {
        if (horizontal != 0 && IsGrounded())
        {
            //Running left or right
            anim.SetBool(RUN_ANIMATION, true);
            anim.SetBool(JUMP_ANIMATION, false);
        }
        else if (!IsGrounded())
        {
            anim.SetBool(RUN_ANIMATION, false);
            anim.SetBool(JUMP_ANIMATION, true);
        }
        else
        {
            anim.SetBool(JUMP_ANIMATION, false);
            anim.SetBool(RUN_ANIMATION, false);
        }
    }

}
