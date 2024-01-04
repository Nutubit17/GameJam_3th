using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Player player;
    private Animator animator;
    private Rigidbody2D rigid;
    private PolygonCollider2D polyCollider;
    public  BoxCollider2D boxCollider;
    public  GameObject polyDir;


    public float attackCoolTime;
    public float pAttackCoolTime;
    public float jAttackCoolTime;

    private bool attackCheck;
    private bool pAttackCheck;
    private bool jAttackCheck;

    private bool attack =true;



    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        polyCollider = GetComponentInChildren<PolygonCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        //boxCollider = transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(Attack());
        //StartCoroutine(PAttack());
        StartCoroutine(JAttack());
    }



    IEnumerator Attack()
    {
        yield return null;
        while (true)
        {
            polyDir.transform.localScale = new Vector3(player.sprite.flipX == true ? -1 : 1, 1, 1);
            if (Input.GetMouseButtonDown(0) && player.ray && attack == true)
            {
                attack = false;
                animator?.SetTrigger("attack");
                yield return new WaitUntil(() => !attackCheck);
                yield return new WaitForSeconds(attackCoolTime);
            }
            if (Input.GetMouseButtonDown(0) && player.ray && attack == false)
            {
                attack = true;
                animator?.SetTrigger("attack2");
                yield return new WaitUntil(() => !pAttackCheck);
                yield return new WaitForSeconds(pAttackCoolTime);
            }

            yield return null;
        }
    }

    IEnumerator JAttack()
    {
        yield return null;
        while (true)
        {
            polyDir.transform.localScale = new Vector3(player.sprite.flipX == true ? -1 : 1, 1, 1);
            if (Input.GetMouseButtonDown(0) && !player.ray)
            {
                jAttackCheck = true;
                animator?.SetTrigger("jumpAttack");
                yield return new WaitUntil(() => !jAttackCheck);
                yield return new WaitForSeconds(jAttackCoolTime);
            }
            yield return null;
        }
    }


    public void AttackOn()
    {
        polyCollider.enabled = true;
    }

    public void AttackOff()
    {
        attackCheck = false;
        polyCollider.enabled = false;
    }

    public void PAttackOn()
    {
        boxCollider.enabled = true;
    }

    public void PAttackOff()
    {
        pAttackCheck = false;
        boxCollider.enabled = false;
    }
    public void JAttackOn()
    {
        polyCollider.enabled = true;
        rigid.gravityScale = 10;
    }

    public void JAttackOff()
    {
        rigid.gravityScale = 1;
        jAttackCheck = false;
        polyCollider.enabled = false;
    }
}
