using DG.Tweening;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public enum EnmeyName
{
    NONE,
    SWARD,
    BOW,
    AXE
}

public enum EnmeyCondetiion
{
    NORMAL,
    CHASE,
    ATTACK
}

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{

#if UNITY_EDITOR
    [SerializeField] bool gizmoOn = true;
    [Space]

#endif

    public GameObject poly;
    private Player _player;
    private PolygonCollider2D polygonCollider;
    public Rigidbody2D rigid;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer sprite;
    private HpGauge hpGauge;

    public Material enemyMat;
    public GameObject stateObject;

    public EnmeyName enemyType;
    public float normalRecognitionRange;
    public float stressRecgnitionRange;
    public float stressDurationTime;
    public float attackRange;
    public float attackCoolTime;
    public bool isOntantion;

    public int maxHp;
    public int currentHp;
    public int damage;
    public int speed;
    public int jumpPower;

    public float moveDir;


    private bool jumpCheck;
    private bool isAttack;
    private bool isOnKnockBack;
    private bool isDead;
    public bool typeStress;
    public bool typeNormal;
    public bool enemyStateParticle;

    private float time = 0;

    private readonly int _RateHash = Shader.PropertyToID("_Rate");

    private RaycastHit2D ray;
    private RaycastHit2D ray2;


    public Vector3 startPos;
    public Vector3 endPos;

#if UNITY_EDITOR

    public Color moveRangeGizmoColor = Color.white;
    public Color moveRangeGizmoColor2 = Color.white;

    public Color normalRecogGizmoColor = Color.blue;
    public Color stressRecogGizmoColor = Color.red;
    public Color attackRangeGizmoColor = Color.white;


#endif
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        polygonCollider = GetComponentInChildren<PolygonCollider2D>();
        hpGauge = GetComponentInChildren<HpGauge>();

        currentHp = maxHp;
    }
    
    private void Start()
    {
        _player = Player.Instance;    
    }

    public void OnEnable()
    {
        enemyMat = GetComponent<SpriteRenderer>().material;
        StartCoroutine(MoveRoutine());
        StartCoroutine(SetVelocityRoutine());
        StartCoroutine(JumpRoutine());
        StartCoroutine(Attack());
        StartCoroutine(EnmeyState());
    }


    IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() =>
            {
                bool result = !(isAttack || isOnKnockBack);

                if (!result) moveDir = 0;
                return result;
            });

            yield return new WaitForSeconds(2f);
            moveDir = Random.Range(-1, 2);


            float positionInverseLerp = Mathf.InverseLerp(startPos.x, endPos.x, transform.position.x);

            if (positionInverseLerp == 0 || positionInverseLerp == 1)
            {
                moveDir = -(int)Mathf.Sign(positionInverseLerp - 0.5f);
            }


            yield return new WaitUntil(() =>
            {
                bool result = !(isAttack || isOnKnockBack);

                if (!result) moveDir = 0;
                return result;
            });


            if (moveDir == -1)
                sprite.flipX = true;
            else if (moveDir == 1)
                sprite.flipX = false;


            if (moveDir != 0)
            {
                animator.SetBool("run", true);
            }
            else
                animator.SetBool("run", false);


        }
    }


    public IEnumerator Attack()
    {
        yield return null;

        while (true)
        {
            if ((_player.transform.position - transform.position).sqrMagnitude <= Mathf.Pow(normalRecognitionRange, 2))
            {
                poly.transform.localScale = new Vector3((sprite.flipX == true ? -1 : 1), 1, 1);
                if ((_player.transform.position - transform.position).sqrMagnitude <= attackRange)
                {
                    isAttack = true;

                    animator.SetTrigger("attack");
                    moveDir = 0.001f;
                }


                yield return new WaitUntil(() => !isAttack);
                yield return new WaitForSeconds(attackCoolTime);
            }

            yield return null;
        }
    }
    public void AttackOn()
    {
        polygonCollider.enabled = true;

    }

    public void AttackOff()
    {
        polygonCollider.enabled = false;
        isAttack = false;
    }

    IEnumerator EnmeyState()
    {
        yield return null;

        while (true)
        {
            if (Mathf.Abs(_player.transform.position.x - transform.position.x) <= normalRecognitionRange)
            {
                if(!typeStress)
                    enemyStateParticle = false;

                typeNormal = false;
                typeStress = true;

            }

            if (Mathf.Abs(_player.transform.position.x - transform.position.x) <= stressRecgnitionRange)
                time = 0;

            if (Mathf.Abs(_player.transform.position.x - transform.position.x) >= stressRecgnitionRange && typeStress == true)
            {
                time += Time.deltaTime;
                if (stressDurationTime < time)
                {
                    typeStress = false;
                    typeNormal = true;
                    time = 0;
                }
            }
            if (typeStress == true)
            {
                stateObject.GetComponent<SpriteRenderer>().color = Color.red;
                EnemyStateParticle();
                if ((_player.transform.position - transform.position).x >= 0f
                    && Vector3.Distance(_player.transform.position, transform.position) >= 1f)
                {
                    moveDir = 1;
                    sprite.flipX = false;
                }
                else if(Vector3.Distance(_player.transform.position, transform.position) >= 1f)
                {
                    moveDir = -1;
                    sprite.flipX = true;
                }
            }
            else if (typeNormal == true)
                stateObject.GetComponent<SpriteRenderer>().color = Color.green;
            yield return null;
        }
    }
    public void EnemyStateParticle()
    {
        if (enemyStateParticle == false)
        ParticleManager.Instance.MakeParticle(transform.position + Vector3.up * 2.2f, "Recognition", 2f, transform);

        enemyStateParticle = true;
    }

    IEnumerator SetVelocityRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isOnKnockBack);



            
            Vector2 dir = new Vector2(moveDir * speed, rigid.velocity.y);

            if (Mathf.Abs(_player.transform.position.x - transform.position.x) <= 1)
            {
                dir *= Vector2.up;
                moveDir = 0;
            }

            rigid.velocity = dir;

            yield return null;
        }
    }


    IEnumerator JumpRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => !(isAttack || isOnKnockBack));

            ray = Physics2D.Raycast(capsuleCollider.bounds.min + Vector3.down * 0.3f, Vector2.right, capsuleCollider.bounds.extents.x * 2,
                1 << LayerMask.NameToLayer("Ground"));

            ray2 = Physics2D.Raycast(capsuleCollider.bounds.center, moveDir * Vector3.right, 1f,
                1 << LayerMask.NameToLayer("Ground"));



            if (ray2 && ray)
            {
                rigid.velocity = new Vector3(rigid.velocity.x, jumpPower);
            }

            yield return null;
        }
    }
    public Vector3 InverseLerp(Vector3 target0, Vector3 target1, Vector3 target2)
    {
        return new Vector3(Mathf.InverseLerp(target0.x, target1.x, target2.x), Mathf.InverseLerp(target0.y, target1.y, target2.y)
            , Mathf.InverseLerp(target0.z, target1.z, target2.z));
    }

    public void HpDown(int damage)
    {
        hpGauge.Rate -= (float)damage / maxHp;
        currentHp -= damage;
    }

    public void KnockBack(Vector2 dir, float forceScale, float knockBackTime = 1f)
    {
        StartCoroutine(KnockBackRoutine(dir, forceScale, knockBackTime));
    }

    private IEnumerator KnockBackRoutine(Vector2 dir, float forceScale, float knockBackTime = 1f)
    {
        isOnKnockBack = true;

        yield return null;
        rigid.velocity += dir * forceScale;
        yield return new WaitForSeconds(knockBackTime);

        isOnKnockBack = false;
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        StopAllCoroutines();
        rigid.velocity = Vector2.zero;
        animator.SetTrigger("die");
        
        TimeControlPrincipal timeController = new TimeControlPrincipal();

        timeController.mono = this;
        timeController.currentTime = 1f;
        timeController.nextTime = 0.01f;
        timeController.time = 0.3f;
        timeController.TryTimeControl();

    }

    public void DieAniEnd()
    {
        StartCoroutine(DieRoutine());
    }

    public IEnumerator DieRoutine()
    {
        hpGauge.transform.DOScaleX(0, 1f);
        yield return new WaitForSeconds(1f);
        DOTween.To(() => enemyMat.GetFloat(_RateHash), x => enemyMat.SetFloat(_RateHash, x), 1f, 5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 dirToOther = (collision.transform.position - transform.position).normalized;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.KnockBack(-dirToOther, 20f, 0.5f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Poly") && !isDead)
        {
            ParticleManager.Instance.MakeParticle(transform.position, "Blood");
            CameraManager.Instance.CameraShake(0.6f, 10, 50, 0.2f);
            

        }else if(isDead && collision.CompareTag("Poly"))
        {
            ParticleManager.Instance.MakeParticle(transform.position, "Blood");
            CameraManager.Instance.CameraShake(1f, 10, 150, 0.2f);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Selection.activeGameObject == gameObject && gizmoOn)
        {
            Color currentColor = Gizmos.color;
            Gizmos.color = moveRangeGizmoColor2;

            Vector3 leftDown = startPos + new Vector3(-0.1f, -0.1f);
            Vector3 leftUp = new Vector3(startPos.x, endPos.y) + new Vector3(-0.1f, 0.1f);
            Vector3 rightUp = endPos + new Vector3(0.1f, 0.1f);
            Vector3 rightDown = new Vector3(endPos.x, startPos.y) + new Vector3(0.1f, -0.1f);

            Gizmos.DrawLine(leftDown, leftUp);
            Gizmos.DrawLine(leftUp, rightUp);
            Gizmos.DrawLine(rightUp, rightDown);
            Gizmos.DrawLine(rightDown, leftDown);



            Vector3 leftDown1 = startPos + new Vector3(0.1f, 0.1f);
            Vector3 leftUp1 = new Vector3(startPos.x, endPos.y) + new Vector3(0.1f, -0.1f);
            Vector3 rightUp1 = endPos + new Vector3(-0.1f, -0.1f);
            Vector3 rightDown1 = new Vector3(endPos.x, startPos.y) + new Vector3(-0.1f, 0.1f);

            Gizmos.DrawLine(leftDown1, leftUp1);
            Gizmos.DrawLine(leftUp1, rightUp1);
            Gizmos.DrawLine(rightUp1, rightDown1);
            Gizmos.DrawLine(rightDown1, leftDown1);

            Gizmos.color = moveRangeGizmoColor;
            Vector3 leftDown2 = startPos;
            Vector3 leftUp2 = new Vector3(startPos.x, endPos.y);
            Vector3 rightUp2 = endPos;
            Vector3 rightDown2 = new Vector3(endPos.x, startPos.y);

            Gizmos.DrawLine(leftDown2, leftUp2);
            Gizmos.DrawLine(leftUp2, rightUp2);
            Gizmos.DrawLine(rightUp2, rightDown2);
            Gizmos.DrawLine(rightDown2, leftDown2);


            Gizmos.color = normalRecogGizmoColor;
            Gizmos.DrawWireSphere(transform.position, normalRecognitionRange);

            Gizmos.color = stressRecogGizmoColor;
            Gizmos.DrawWireSphere(transform.position, stressRecgnitionRange);

            Gizmos.color = attackRangeGizmoColor;
            Gizmos.DrawWireSphere(transform.position, attackRange);


            Gizmos.color = currentColor;

        }

    }
#endif
}
