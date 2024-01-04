using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDescription : MonoBehaviour
{
    private float openTime = 2f;
    private float closeTime = 3f;
    
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
            transform.localScale = new Vector3(1, 1, openCurve.Evaluate(percent));
            
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
            
            transform.localScale = new Vector3(1, 1, closeCurve.Evaluate(percent));
            
            percent += Time.deltaTime / closeTime;
            yield return null;
            
            percent += Time.deltaTime;
            yield return null;
        }
    }

}
