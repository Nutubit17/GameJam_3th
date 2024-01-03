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
    private Rigidbody2D rigid;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer sprite;


    public EnmeyName enemyType;
    public float normalRecognitionRange;
    public float stressRecgnitionRange;
    public float attackRange;
    public bool isOntantion;

    public int hp;
    public int damage;
    public int speed;
    public int jumpPower;

    private bool jumpCheck;
    private bool isAttack;
    private int moveDir;

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
        _player = Player.Instance;
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        polygonCollider = GetComponentInChildren<PolygonCollider2D>();


    }
    public void OnEnable()
    {
        StartCoroutine(MoveRoutine());
        StartCoroutine(SetVelocityRoutine());
        StartCoroutine(JumpRoutine());
        StartCoroutine(Attack());
    }


    IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isAttack);

            yield return new WaitForSeconds(2f);
            moveDir = Random.Range(-1, 2);


            float positionInverseLerp = Mathf.InverseLerp(startPos.x, endPos.x, transform.position.x);

            if (positionInverseLerp == 0 || positionInverseLerp == 1)
            {
                moveDir = -(int)Mathf.Sign(positionInverseLerp - 0.5f);
            }


            if (moveDir == -1)
                sprite.flipX = true;
            else if (moveDir == 1)
                sprite.flipX = false;

            poly.transform.localScale = new Vector3((sprite.flipX == true ? -1 : 1), 1, 1);

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
        while (true)
        {
            if((_player.transform.position - transform.position).sqrMagnitude <= Mathf.Pow(normalRecognitionRange, 2))
            {
                if ((_player.transform.position - transform.position).x >= 0)
                {
                    moveDir = 1;
                    transform.localScale = Vector3.one;
                }
                else
                {
                    moveDir = -1;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if((_player.transform.position - transform.position).sqrMagnitude <= attackRange)
                {
                    isAttack = true;
                    animator.SetTrigger("attack");
                    polygonCollider.enabled = true;
                    moveDir = 0;
                }

                yield return new WaitUntil(() => !isAttack);
            }

            yield return null;
        }
    }

    public void AttackOff()
    {
        polygonCollider.enabled = false;
        isAttack = false;
    }

    IEnumerator SetVelocityRoutine()
    {
        while (true)
        {
            rigid.velocity = new Vector2(moveDir * speed, rigid.velocity.y);

            yield return null;
        }
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isAttack);

            ray = Physics2D.Raycast(capsuleCollider.bounds.min + Vector3.down * 0.3f, Vector2.right, capsuleCollider.bounds.extents.x * 2,
                1 << LayerMask.NameToLayer("Ground"));
            
            ray2 = Physics2D.Raycast(capsuleCollider.bounds.center, moveDir * Vector3.right, 1,
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
            ,Mathf.InverseLerp(target0.z, target1.z, target2.z));
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Selection.activeGameObject == gameObject && gizmoOn)
        {
            Color currentColor = Gizmos.color;
            Gizmos.color = moveRangeGizmoColor2;

            Vector3 leftDown = startPos  + new Vector3(-0.1f, -0.1f);
            Vector3 leftUp = new Vector3(startPos.x, endPos.y)  + new Vector3(-0.1f, 0.1f);
            Vector3 rightUp = endPos  + new Vector3(0.1f, 0.1f);
            Vector3 rightDown = new Vector3(endPos.x, startPos.y)  + new Vector3(0.1f, -0.1f);

            Gizmos.DrawLine(leftDown, leftUp);
            Gizmos.DrawLine(leftUp, rightUp);
            Gizmos.DrawLine(rightUp, rightDown);
            Gizmos.DrawLine(rightDown, leftDown);



            Vector3 leftDown1 = startPos  + new Vector3(0.1f, 0.1f);
            Vector3 leftUp1 = new Vector3(startPos.x, endPos.y)  + new Vector3(0.1f, -0.1f);
            Vector3 rightUp1 = endPos  + new Vector3(-0.1f, -0.1f);
            Vector3 rightDown1 = new Vector3(endPos.x, startPos.y)  + new Vector3(-0.1f, 0.1f);

            Gizmos.DrawLine(leftDown1, leftUp1);
            Gizmos.DrawLine(leftUp1, rightUp1);
            Gizmos.DrawLine(rightUp1, rightDown1);
            Gizmos.DrawLine(rightDown1, leftDown1);

            Gizmos.color = moveRangeGizmoColor;
            Vector3 leftDown2 = startPos ;
            Vector3 leftUp2 = new Vector3(startPos.x, endPos.y) ;
            Vector3 rightUp2 = endPos ;
            Vector3 rightDown2 = new Vector3(endPos.x, startPos.y) ;

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
