using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigid;
    private Animator animator;
    private SpriteRenderer sprite;

    public RaycastHit2D ray;

    [System.NonSerialized]public int currentSpeed = 2;
    public int dashSpeed = 7;
    public int normalSpeed = 2;
    public int jumpPower;

    private float dir;
    public void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(MoveRoutine());
        currentSpeed = normalSpeed;
    }

    private IEnumerator MoveRoutine()
    {
        while(true)
        {
            dir = Input.GetAxisRaw("Horizontal");
            rigid.velocity = new Vector3(dir * normalSpeed, rigid.velocity.y);
            
            if (dir != 0)
            {
                animator.SetBool("isRun", true);
                if (dir == 1)
                    sprite.flipX = false;
                else
                    sprite.flipX = true;
            }
            else
                animator.SetBool("isRun", false);
            Jump();
            StartCoroutine(Dash());
            Dash();
            yield return null;
        }
    }

    public void Jump()
    {
        ray = Physics2D.Raycast(boxCollider.bounds.min + Vector3.down*0.1f, Vector2.right, boxCollider.bounds.extents.x * 2, 
            1 << LayerMask.NameToLayer("Ground"));
        if (!ray)
        {
            //animator.SetTrigger("down");
        }
        if (Input.GetKeyDown(KeyCode.Space) && ray)
        {
            animator.SetTrigger("jump");
            rigid.velocity = new Vector3(rigid.velocity.x, jumpPower);
        }
    }

    public IEnumerator Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            normalSpeed = dashSpeed;
            yield return new WaitForSeconds(0.5f);
            normalSpeed = currentSpeed;
        }
    }

    
}
