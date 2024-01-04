using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossStage : Stage
{
    public Transform boss;
    public Transform bossHpUI;
    
    public override void StageAction()
    {
        StartCoroutine(StageActionRoutine());
    }
    
    IEnumerator StageActionRoutine()
    {
        DOTween.Sequence()
            .AppendInterval(4f)
            .Append(bossHpUI.DOScaleX(1, 0.5f))
            .AppendInterval(0.2f)
            .AppendCallback(() => CameraManager.Instance.ChangeTarget(boss))
            .AppendInterval(0.1f)
            .AppendCallback(() => CameraManager.Instance.ChangeCameraSize(0.3f, 3f, AnimationCurve.Linear(0,0,1,1)))
            .AppendInterval(3f)
            .AppendCallback(() => CameraManager.Instance.ChangeCameraSize(1f, 1.5f, AnimationCurve.Linear(0,0,1,1)))
            .AppendInterval(1f)
            .AppendCallback(() => CameraManager.Instance.ChangeTarget(Player.Instance.transform));
        
        
        StageShowUI.Instance.text.text = stageName;
        StageShowUI.Instance.stageDesc.questName.text = questName;
        StageShowUI.Instance.stageDesc.questDesc.text = questDesc;
        StageShowUI.Instance.stageDesc.hardness.text = hardness;
        
        if(StageShowUI.Instance.stageDesc.transform.childCount > 3)
            Destroy(StageShowUI.Instance.stageDesc.transform.GetChild(3).gameObject);
        
        if(gameObjectDesc is not null)
            Instantiate(gameObjectDesc, StageShowUI.Instance.stageDesc.transform);
        
        
        StageShowUI.Instance.FadeOut();
        CameraManager.Instance.ChangeTarget(Player.Instance.transform);
        yield return new WaitForSeconds(StageShowUI.Instance.fadeTime + 1f);

        StageShowUI.Instance.FadeIn();
        yield return new WaitForSeconds(StageShowUI.Instance.fadeTime + 1f);            
    }
}
