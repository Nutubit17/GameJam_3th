using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{

    public Image panel;

    public TextMeshProUGUI panelTitle;
    public Image panelButton;
    public TextMeshProUGUI buttonText;
    

    public float moveTime;
    public AnimationCurve showMoveCurve;

    public float titleMoveTime;
    public AnimationCurve titleMoveCurve;

    public StageShowUI stageShowUI;
    public GameObject startStage;

    public bool isGameStarted;
    
    private void Awake()
    {
        panel = transform.Find("Panel").GetComponent<Image>();
        panelTitle = transform.Find("Panel/Title").GetComponent<TextMeshProUGUI>();
        panelButton = transform.Find("Panel/Button").GetComponent<Image>();
        buttonText = transform.Find("Panel/Button/Text").GetComponent<TextMeshProUGUI>();

    }

    private void Start()
    {
        StartCoroutine(StartShowRoutine());
    }

    private IEnumerator StartShowRoutine()
    {
        DOTween.Sequence()
            .Append(DOTween.To(() => CameraManager.Instance.main.backgroundColor,
                value => CameraManager.Instance.main.backgroundColor = value
                , Color.white / 6f, 0.3f))
            .Append(DOTween.To(() => CameraManager.Instance.main.backgroundColor,
                value => CameraManager.Instance.main.backgroundColor = value
                , Color.black, 0.3f));
        
        yield return new WaitForSeconds(1f);
        ParticleManager.Instance.MakeParticle( new Vector3(0, 0, -9.48f),"StartParticle", 7f);

        yield return new WaitForSeconds(3f);
        
        float percent = 0;

        Vector3 currentPanelPos = panel.transform.position;
        Vector3 endPanelPos = transform.position;
        panel.color = Color.white;
        
        
        while (percent <= 1)
        {
            panel.transform.position = Vector3.Lerp(currentPanelPos, endPanelPos, showMoveCurve.Evaluate(percent));
            
            percent += Time.deltaTime / moveTime;
            yield return null;
        }
        
        
        float percent1 = 0;

        Vector3 currentTitlePos = panelTitle.transform.position + Vector3.down * 10;
        Vector3 endTitlePos = panelTitle.transform.position;
        
        while (percent1 <= 1)
        {
            panelTitle.transform.position = Vector3.Lerp(currentTitlePos, endTitlePos, titleMoveCurve.Evaluate(percent1));
            panelTitle.color = Color.Lerp(Color.clear, Color.black, titleMoveCurve.Evaluate(percent1));
            
            percent1 += Time.deltaTime / titleMoveTime;
            yield return null;
        }
        
        float percent2 = 0;

        Vector3 currentButtonPos = panelButton.transform.position + Vector3.down * 10;
        Vector3 endButtonPos = panelButton.transform.position;
        
        while (percent2 <= 1)
        {
            panelButton.transform.position = Vector3.Lerp(currentButtonPos, endButtonPos, titleMoveCurve.Evaluate(percent2));
            buttonText.color = Color.Lerp(Color.clear, Color.black, titleMoveCurve.Evaluate(percent2));
            panelButton.color = Color.Lerp(Color.clear, Color.white, titleMoveCurve.Evaluate(percent2));
            
            percent2 += Time.deltaTime / titleMoveTime;
            yield return null;
        }
        
    }



    public void GameStart()
    {
        if(isGameStarted) return;
        isGameStarted = true;
        
        IEnumerator PanelRectRoutine()
        {
            Vector3 startPosition = panel.transform.position;
            Vector3 endPosition = startPosition + Vector3.up * 10;

            float percent = 0;

            while (percent <= 1)
            {
                panel.transform.position = Vector3.Lerp(startPosition, endPosition, showMoveCurve.Evaluate(percent));
                panel.color = Color.Lerp(Color.white, Color.clear, showMoveCurve.Evaluate(percent));
                panelTitle.color = Color.Lerp(Color.black, Color.clear, showMoveCurve.Evaluate(percent));
                buttonText.color = Color.Lerp(Color.black, Color.clear, showMoveCurve.Evaluate(percent));
                panelButton.color = Color.Lerp(Color.white, Color.clear, showMoveCurve.Evaluate(percent));
                
                percent += Time.deltaTime / moveTime;
                yield return null;
            }


        }

        IEnumerator StageShowUIRoutine()
        {
            yield return new WaitForSeconds(moveTime);
            stageShowUI.FadeOut();
            startStage.SetActive(true);
            CameraManager.Instance.ChangeTarget(Player.Instance.transform);
            yield return new WaitForSeconds(stageShowUI.fadeTime + 1f);

            stageShowUI.FadeIn();
            
        }
        
        
        StartCoroutine(PanelRectRoutine());
        StartCoroutine(StageShowUIRoutine());



    }
    
    
}
