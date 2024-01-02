using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Player player;
    private Animator animator;

    public int attackCoolTime;
    public int pAttackCoolTime;
    public int jAttackCoolTime;

    private bool attackCheck;
    private bool pAttackCheck;
    private bool jAttackCheck;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Attak();
    }
    public void Attak()
    {
       
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
        yield return new WaitForSeconds(attackCoolTime);
        attackCheck = false;
    }

    public IEnumerator PAttackDelayTime()
    {
        pAttackCheck = true;
        animator.SetTrigger("powerAttack");
        yield return new WaitForSeconds(pAttackCoolTime);
        pAttackCheck = false;
    }

    public IEnumerator JAttackDelayTime()
    {
        jAttackCheck = true;
        animator.SetTrigger("jumpAttack");
        yield return new WaitForSeconds(jAttackCoolTime);
        jAttackCheck = false;
    }
}
