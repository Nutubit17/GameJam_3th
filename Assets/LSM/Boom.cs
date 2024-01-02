using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{

    [SerializeField]
    private int boomDelayTime;

    private Animator boomAnimator;

    private void Awake()
    {
        boomAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(BoomDelay());
        }
    }

    public void BoomDestroy()
    {
        Destroy(gameObject);
    }


    public IEnumerator BoomDelay()
    {
        yield return new WaitForSeconds(boomDelayTime);
        boomAnimator.SetBool("OnBoom", true);
    }

    
}
