using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class StageShowUI : MonoBehaviour
{
    public Image panel;
    public TextMeshProUGUI text;
    public Image enemyImage1;    
    public Image enemyImage2;
    public StageDescription stageDesc;
    
    public float fadeTime = 2f;
    
    private void Awake()
    {
        panel       = transform.GetChild(0).GetComponent<Image>();
        text        = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        enemyImage1 = transform.GetChild(2).GetComponent<Image>();
        enemyImage2 = transform.GetChild(3).GetComponent<Image>();
    }

    private IEnumerator fadeRoutine;
    
    public void FadeOut()
    {
        fadeRoutine = FadeOutRoutine();
        StartCoroutine(fadeRoutine);
    }

    private IEnumerator FadeOutRoutine()
    {
        float percent = 0;

        while (percent <= 1)
        {
            panel.color = Color.Lerp(Color.clear, Color.black, percent);
            text.color = Color.Lerp(Color.clear, Color.black, percent);
            enemyImage1.color = Color.Lerp(Color.clear, Color.white, percent);
            enemyImage2.color = Color.Lerp(Color.clear, Color.white, percent);
            
            percent += Time.deltaTime / fadeTime;
            yield return null;
        }

        fadeRoutine = null;
    }

    public void FadeIn()
    {
        fadeRoutine = FadeInRoutine();
        StartCoroutine(fadeRoutine);
    }

    private IEnumerator FadeInRoutine()
    {
        float percent = 0;

        while (percent <= 1)
        {
            panel.color = Color.Lerp(Color.black, Color.clear, percent);
            text.color = Color.Lerp(Color.black, Color.clear, percent);
            enemyImage1.color = Color.Lerp(Color.white, Color.clear, percent);
            enemyImage2.color = Color.Lerp(Color.white, Color.clear, percent);

            percent += Time.deltaTime / fadeTime;
            yield return null;
        }
        
        fadeRoutine = null;
    }
}
