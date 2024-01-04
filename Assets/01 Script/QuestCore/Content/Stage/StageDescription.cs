using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDescription : MonoBehaviour
{
    [SerializeField] private float openTime = 2f;
    [SerializeField] private float closeTime = 3f;
    
    public AnimationCurve openCurve;
    public AnimationCurve closeCurve;
    
    public void Open()
    {
        StartCoroutine(OpenRoutine());
    }

    IEnumerator OpenRoutine()
    {
        float percent = 0;

        while (percent <= 1)
        {
            transform.localScale = new Vector3(openCurve.Evaluate(percent),1, 1);
            
            percent += Time.deltaTime / openTime;
            yield return null;
        }
    }

    public void Close()
    {
        StartCoroutine(CloseRoutine());
    }

    IEnumerator CloseRoutine()
    {
        float percent = 0;

        while (percent <= 1)
        {
            
            transform.localScale = new Vector3(closeCurve.Evaluate(Mathf.Lerp(1,0, percent)), 1,  1);
            
            percent += Time.deltaTime / closeTime;
            yield return null;
            
        }

        transform.localScale = new Vector3(0, 1, 1);
    }

}
