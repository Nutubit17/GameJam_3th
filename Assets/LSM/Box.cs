using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Player player;
    public GameObject keyboard_F;

    public int distance;

    private Animator animator;

    public bool isBoxOpened = false;

    IEnumerator fadeRoutine;
    bool isOnKeyboardCheck = false;
    public IEnumerator FadeInRoutine()
    {
        float percent = 0;
        Vector2 currentPosition = keyboard_F.transform.position;
        Vector2 nextPosition = currentPosition + Vector2.down;
        Color currentColor = Color.white;
        Color nextColor = Color.clear;
        float delay = 0.3f;
        while(percent <= 1)
        {
            keyboard_F.transform.position = Vector3.Lerp(currentPosition, nextPosition, percent);
            keyboard_F.GetComponent<SpriteRenderer>().color = Color.Lerp(currentColor, nextColor, percent);
            percent += Time.deltaTime/delay;
            yield return null;
        }
        keyboard_F.SetActive(false);
        fadeRoutine = null;
    }
    private IEnumerator FadeOutRoutine()
    {
        keyboard_F.SetActive(true);
        float percent = 0;
        Vector2 currentPosition = keyboard_F.transform.position;
        Vector2 nextPosition = currentPosition + Vector2.up;
        Color currentColor = Color.clear;
        Color nextColor = Color.white;
        float delay = 0.3f;
        while (percent <= 1)
        {
            keyboard_F.transform.position = Vector3.Lerp(currentPosition, nextPosition, percent);
            keyboard_F.GetComponent<SpriteRenderer>().color = Color.Lerp(currentColor, nextColor, percent);
            percent += Time.deltaTime / delay;
            yield return null;
        }

        fadeRoutine = null;
    }

    public void AnimatorDestroy()
    {
        Destroy(animator);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if ((transform.position - player.transform.position).magnitude < distance && !isBoxOpened) 
        {
            if (Input.GetKeyDown(KeyCode.F) && fadeRoutine is null && isOnKeyboardCheck)
            {
                animator.SetTrigger("open");

                fadeRoutine = FadeInRoutine();
                StartCoroutine(fadeRoutine);

                isOnKeyboardCheck = false;
                isBoxOpened = true;
            }
            if (fadeRoutine is null && !isOnKeyboardCheck)
            {
                fadeRoutine = FadeOutRoutine();
                StartCoroutine(fadeRoutine);

                isOnKeyboardCheck = true;
            }
        }
        else if(isOnKeyboardCheck)
        {
            fadeRoutine = FadeInRoutine();
            StartCoroutine(fadeRoutine);

            isOnKeyboardCheck = false;
        }

    }

}
