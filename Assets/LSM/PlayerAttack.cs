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
        attackCheck = true;
        animator.SetTrigger("attack");
        polyCollider.enabled = true;
        yield return new WaitForSeconds(attackCoolTime);
        attackCheck = false;
    }

    public IEnumerator PAttackDelayTime()
    {
        pAttackCheck = true;
        animator.SetTrigger("powerAttack");
        boxCollider.enabled = true;
        yield return new WaitForSeconds(pAttackCoolTime);
        pAttackCheck = false;
    }

    public IEnumerator JAttackDelayTime()
    {
        jAttackCheck = true;
        animator.SetTrigger("jumpAttack");
        polyCollider.enabled = true;
        yield return new WaitForSeconds(jAttackCoolTime);
        jAttackCheck = false;
    }

    public void PolyColliderOff()
    {
        polyCollider.enabled = false;
    }

    public void BoxColliderOff()
    {
        boxCollider.enabled = false;
    }
}
