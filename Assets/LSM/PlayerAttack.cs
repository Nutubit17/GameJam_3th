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
    public int pAttackCoolTime;
    public int jAttackCoolTime;

    private bool attackAniCheck;
    private bool pAttackAniCheck;
    private bool jAttackAniCheck;

    private bool attackCheck = true;
    private bool pAttackCheck = true;
    private bool jAttackCheck = true;


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
        StartCoroutine(PAttack());
        StartCoroutine(JAttack());
    }



    IEnumerator Attack()
    {

        while (true)
        {
            polyDir.transform.localScale = new Vector3(player.sprite.flipX == true ? -1 : 1, 1, 1);
            if (Input.GetMouseButtonDown(0) && player.ray)
            {
                attackCheck = true;
                animator?.SetTrigger("attack");
                yield return new WaitUntil(() => !attackCheck);
                yield return new WaitForSeconds(attackCoolTime);
            }
            
            yield return null;
        }
    }

    IEnumerator JAttack()
    {
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
    IEnumerator PAttack()
    {
        while (true)
        {
            polyDir.transform.localScale = new Vector3(player.sprite.flipX == true ? -1 : 1, 1, 1);
            if (Input.GetMouseButtonDown(1) && player.ray)
            {
                pAttackCheck = true;
                animator?.SetTrigger("powerAttack");
                yield return new WaitUntil(() => !pAttackCheck);
                yield return new WaitForSeconds(pAttackCoolTime);
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
    }

    public void JAttackOff()
    {
        jAttackCheck = false;
        polyCollider.enabled = false;
    }
}
