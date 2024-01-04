using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStage : Stage
{
    public override void StageAction()
    {
        StartCoroutine(StageActionRoutine());
    }

    IEnumerator StageActionRoutine()
    {
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
