using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public abstract class Stage : MonoBehaviour
{
    public static int currentStage = 1;
    public static HashSet<Stage> stages = new HashSet<Stage>();
    
    public int thisStageInt = 0;
    public string stageName;
    public string questName;
    public string questDesc;
    public string hardness;
    
    public GameObject gameObjectDesc;
    
    [SerializeField] private PolygonCollider2D polyRange;

    public GameObject startPos;
    
    
    private void Awake()
    {
        stages.Add(this);
    }
    
    public void CheckThisStage()
    {
        if (currentStage == thisStageInt)
        {
            CameraManager.Instance.virtualCamera.GetComponentInChildren<CinemachineConfiner2D>().m_BoundingShape2D = polyRange;
            Player.Instance.transform.position = startPos.transform.position;
            StageAction();
        }
    }

    public abstract void StageAction();

}
