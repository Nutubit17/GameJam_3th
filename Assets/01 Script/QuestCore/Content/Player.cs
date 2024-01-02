using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float speed = 2f;
    public void Awake()
    {
        StartCoroutine(MoveRoutine());
        rigid = GetComponent<Rigidbody2D>();

        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while(true)
        {
            float dir = Input.GetAxisRaw("Horizontal");
            rigid.velocity = dir * speed * Vector3.right;
            
            yield return null;
        }
    }
}
