using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bow : Enemy
{
    public GameObject arrow;
    public GameObject bow;

    private Animator bowAnimationController;

    [SerializeField]
    private float arrowSpeed;

    public override void Awake()
    {
        base.Awake();
        bowAnimationController = transform.Find("Bow").GetComponent<Animator>();
    }
    public override void Start()
    {
        base.Start();
        StartCoroutine(BowAttack());
    }

    private Vector3 distance;
    private void Update()
    {
        if (isDead) return;

        distance = player.transform.position - transform.position;
        float zPos = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        bow.transform.rotation = Quaternion.Euler(0, 0, zPos);
        if (zPos > 90)
        {
            bow.transform.localScale = new Vector3(1, -1, 1);
            sprite.flipX = true;
        }
        else
        {
            bow.transform.localScale = new Vector3(1, 1, 1);
            sprite.flipX = false;
        }
    }

    IEnumerator BowAttack()
    {
        while (true)
        {
            if(Mathf.Abs(player.transform.position.x - transform.position.x) < attackRange)
            {
                animationController.SetTrigger("arrowAttack");
                bowAnimationController.SetTrigger("arrowAttack");

                isAttack = true;

                yield return new WaitUntil(() => !isAttack);
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }


    public override void AttackOff()
    {
        isAttack = false;
    }

    public override void AttackOn()
    {
        GameObject arrowBullet = Instantiate(arrow, transform.position, transform.Find("Bow").transform.localRotation);
        
        while (5 > time)
        {
            Destroy(arrowBullet,5);
            time += Time.deltaTime;
            arrowBullet.GetComponentInChildren<Rigidbody2D>().velocity = 
                (player.transform.position - transform.position).normalized * arrowSpeed;
            Debug.Log(1);
        }
        time = 0;
    }

    public override IEnumerator JumpRoutine()
    {
        yield return null;
    }

    public override void Die()
    {
        base.Die();
        Destroy(bow);
    }

    public override IEnumerator MoveRoutine()
    {
        yield return null;
    }

    
}
