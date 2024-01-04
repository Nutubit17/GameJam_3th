using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageDescription : MonoBehaviour
{
    [SerializeField] private float openTime = 2f;
    [SerializeField] private float closeTime = 3f;
    
    public AnimationCurve openCurve;
    public AnimationCurve closeCurve;
    
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDesc;
    public TextMeshProUGUI hardness;

    private void Awake()
    {
        questName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        questDesc = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        hardness  = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

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
