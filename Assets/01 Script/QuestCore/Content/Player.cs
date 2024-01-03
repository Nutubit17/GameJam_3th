using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public SpriteRenderer sprite;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigid;
    private Animator animator;
    private TrailRenderer trail;

    public RaycastHit2D ray;

    [System.NonSerialized]
    public int currentSpeed = 2;
    public int dashSpeed = 7;
    public int normalSpeed = 2;
    public int damage = 2;
    public int force = 5;

    [System.NonSerialized]
    public int currentDir = 1;
    [System.NonSerialized]
    public int dashDir = 1;
    public float dashTime;
    public float dashCoolTime;

    public int jumpPower;

    [System.NonSerialized]
    public bool jumpCheck;
    public bool onDash;
    public bool isDash = false;

    private float dir;
    public void Awake()
    {
        Instance = this;

        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        trail = GetComponent<TrailRenderer>();
        StartCoroutine(MoveRoutine());
        StartCoroutine(DashRoutine());
        currentSpeed = normalSpeed;
    }

    private IEnumerator MoveRoutine()
    {
        while(true)
        {
            dir = Input.GetAxisRaw("Horizontal");
            currentDir = sprite.flipX == true ? -1: 1;

            if (!isDash)
            {
                rigid.velocity = new Vector3(dir * currentSpeed, rigid.velocity.y);

                Run();
                Jump();
            }
            else
            {
                rigid.velocity = new Vector3(dashDir * currentSpeed, rigid.velocity.y);
            }



            yield return null;
        }
    }

    public void Run()
    {
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
    }

    public void Jump()
    {
        ray = Physics2D.Raycast(boxCollider.bounds.min + Vector3.down*0.3f, Vector2.right, boxCollider.bounds.extents.x * 2, 
            1 << LayerMask.NameToLayer("Ground"));
        if (!ray && jumpCheck ==false)
        {
            animator.SetTrigger("down");
        }
        if (Input.GetKeyDown(KeyCode.Space) && ray)
        {
            jumpCheck = true;
            animator.SetTrigger("jump");
            rigid.velocity = new Vector3(rigid.velocity.x, jumpPower);
        }
    }

    public void JumpCheck()
    {
        jumpCheck = false;
    }

    public IEnumerator DashRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (onDash)
                    yield return null;
                currentSpeed = dashSpeed;
                dashDir = currentDir;
                isDash = true;

                yield return new WaitForSeconds(dashTime);
                currentSpeed = normalSpeed;
                isDash = false;
                yield return new WaitForSeconds(dashCoolTime- dashTime);
                onDash = false;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponentInParent<Enemy>();

            Vector2 dirVector = Mathf.Sign(enemy.transform.position.x - transform.position.x) * Vector2.right;

            enemy.HpDown(damage);
            enemy.KnockBack(dirVector.normalized, force, 0.3f);
            ParticleManager.Instance.MakeParticle(enemy.transform.position, "Slash", 5f);

        }
    }

}
