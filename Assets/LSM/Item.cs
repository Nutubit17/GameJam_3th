using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField]
    private AnimationCurve curve;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOutRoutine());
    }

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
        while (percent <= 1)
        {
            keyboard_F.transform.position = Vector3.Lerp(currentPosition, nextPosition, percent);
            keyboard_F.GetComponent<SpriteRenderer>().color = Color.Lerp(currentColor, nextColor, percent);
            percent += Time.deltaTime / delay;
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
                fadeRoutine = MoveRoutine();
                StartCoroutine(fadeRoutine);

                isOnKeyboardCheck = true;
            }
        }
        else if (isOnKeyboardCheck)
        {
            fadeRoutine = FadeInRoutine();
            StartCoroutine(fadeRoutine);

            isOnKeyboardCheck = false;
        }

    }


    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            float percent = 0;
            Vector2 currentPosition = transform.position;
            Vector2 nextPosition = currentPosition + Vector2.up;
            float delay = 1f;
            while (percent <= 1)
            {
                transform.position = Vector3.Lerp(currentPosition, nextPosition, curve.Evaluate(percent));

                percent += Time.deltaTime / delay;
                yield return null;
            }
        }

    }
}
