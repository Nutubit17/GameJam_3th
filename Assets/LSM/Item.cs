using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField]
    private AnimationCurve curve;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
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
