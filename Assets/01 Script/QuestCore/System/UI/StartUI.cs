using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartUI : Target<UIManager>
{

    public Transform panel;

    public TextMeshProUGUI panelTitle;
    public Transform panelButton;
    public TextMeshProUGUI buttonText;
    

    public float moveTime;
    public AnimationCurve showMoveCurve;

    public float titleMoveTime;
    public AnimationCurve titleMoveCurve;

    private void Awake()
    {
        panel = transform.Find("Panel");
        panelTitle = transform.Find("Panel/Title").GetComponent<TextMeshProUGUI>();
        panelButton = transform.Find("Panel/Button");
        buttonText = transform.Find("Panel/Button/Text").GetComponent<TextMeshProUGUI>();

        StartCoroutine(StartShowRoutine());
    }

    private IEnumerator StartShowRoutine()
    {
        float percent = 0;

        Vector3 currentPanelPos = panel.transform.position;
        Vector3 endPanelPos = transform.position;
        
        while (percent <= 1)
        {
            panel.transform.position = Vector3.Lerp(currentPanelPos, endPanelPos, showMoveCurve.Evaluate(percent));
            
            percent += Time.deltaTime / moveTime;
            yield return null;
        }
        
        
        float percent1 = 0;

        Vector3 currentTitlePos = panelTitle.transform.position + Vector3.down * 50;
        Vector3 endTitlePos = panelTitle.transform.position;
        
        while (percent1 <= 1)
        {
            panelTitle.transform.position = Vector3.Lerp(currentTitlePos, endTitlePos, titleMoveCurve.Evaluate(percent1));
            panelTitle.color = Color.Lerp(Color.clear, Color.black, titleMoveCurve.Evaluate(percent1));
            
            percent1 += Time.deltaTime / titleMoveTime;
            yield return null;
        }
        
        float percent2 = 0;

        Vector3 currentButtonPos = panelButton.transform.position + Vector3.down * 50;
        Vector3 endButtonPos = panelButton.transform.position;
        
        while (percent2 <= 1)
        {
            buttonText.transform.position = Vector3.Lerp(currentButtonPos, endButtonPos, titleMoveCurve.Evaluate(percent2));
            panelTitle.color = Color.Lerp(Color.clear, Color.black, titleMoveCurve.Evaluate(percent2));
            
            percent2 += Time.deltaTime / titleMoveTime;
            yield return null;
        }
        
        
        
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Init()
    {
        
    }

    public override void Stop()
    {
        
    }
    
    
}
