using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Player player;
    private Animator animator;
    private PolygonCollider2D polyCollider;
    public  BoxCollider2D boxCollider;
    public  GameObject polyDir;

    public float attackCoolTime;
    public int pAttackCoolTime;
    public int jAttackCoolTime;

    private bool attackCheck;
    private bool pAttackCheck;
    private bool jAttackCheck;
    private bool isAttack;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        polyCollider = GetComponentInChildren<PolygonCollider2D>();
    }
    private void Update()
    {
        Attak();
    }
    public void Attak()
    {
        polyDir.transform.localScale =  new Vector3(player.sprite.flipX == true ? -1:1,1,1);
        if (Input.GetMouseButtonDown(0) && player.ray)
        {
            if (attackCheck)
                return;
            StartCoroutine(AttackDelayTime());

        }

        if(Input.GetMouseButtonDown(0) && !player.ray)
        {
            if (jAttackCheck)
                return;
            player.jumpCheck = true;
            StartCoroutine(JAttackDelayTime());
        }

        if (Input.GetMouseButtonDown(1) && player.ray)
        {
            if (pAttackCheck)
                return;
            StartCoroutine(PAttackDelayTime());
        }
    }

    public IEnumerator AttackDelayTime()
    {
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(attackCoolTime);
    }

    public IEnumerator PAttackDelayTime()
    {
        animator.SetTrigger("powerAttack");
        yield return new WaitForSeconds(pAttackCoolTime);
    }

    public IEnumerator JAttackDelayTime()
    {
        animator.SetTrigger("jumpAttack");
        yield return new WaitForSeconds(jAttackCoolTime);
    }
    public void AttackOn()
    {
        jAttackCheck = true;
        polyCollider.enabled = true;
    }

    public void AttackOff()
    {
        jAttackCheck = false;
        polyCollider.enabled = false;
    }
    public void PAttackOn()
    {
        jAttackCheck = true;
        boxCollider.enabled = true;
    }

    public void PAttackOff()
    {
        jAttackCheck = false;
        boxCollider.enabled = false;
    }
}
