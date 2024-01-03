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
    private Rigidbody2D rigid;
    private Animator animator;

    public EnmeyName enemyType;
    public float normalRecognitionRange;
    public float stressRecgnitionRange;
    public bool isOntantion;

    public int hp;
    public int damage;
    public int speed;

    private int moveDir;

    public Vector3 startPos;
    public Vector3 endPos;

#if UNITY_EDITOR

    public Color gizmoColor = Color.white;

#endif
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPos += transform.position;
        endPos += transform.position;
    }
    public void OnEnable()
    {
        StartCoroutine(MoveRoutine());
        StartCoroutine(SetVelocityRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            moveDir = Random.Range(-1, 2);
            if (moveDir == -1)
                transform.localScale = new Vector3(-1, 1, 1);
            else if(moveDir == 1)
                transform.localScale = new Vector3(1, 1, 1);

            if (moveDir != 0)
            {
                animator.SetBool("run", true);
            }
            else
                animator.SetBool("run", false); ;

        }
    }

    IEnumerator SetVelocityRoutine()
    {
        while (true)
        {
            rigid.velocity = new Vector2(moveDir * speed, rigid.velocity.y);
            yield return null;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Selection.activeGameObject == gameObject)
        {
            Color currentColor = Gizmos.color;
            Gizmos.color = gizmoColor;

            Vector3 leftDown = startPos + transform.position;
            Vector3 leftUp = new Vector3(startPos.x, endPos.y) + transform.position;
            Vector3 rightUp = endPos + transform.position;
            Vector3 rightDown = new Vector3(endPos.x, startPos.y) + transform.position;

            Gizmos.DrawLine(leftDown, leftUp);
            Gizmos.DrawLine(leftUp, rightUp);
            Gizmos.DrawLine(rightUp, rightDown);
            Gizmos.DrawLine(rightDown, leftDown);

            Gizmos.DrawLine(rightDown, leftUp);
            Gizmos.DrawLine(leftDown, rightUp);


        }

    }
#endif
}
