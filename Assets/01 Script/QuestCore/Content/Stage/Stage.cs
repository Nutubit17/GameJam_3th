using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public abstract class Stage : MonoBehaviour
{
    public static int currentStage = 1;
    public int thisStageInt = 0;
    [SerializeField] private PolygonCollider2D polyRange;

    private void Start()
    {
        CheckThisStage();
    }

    public void CheckThisStage()
    {
        if (currentStage == thisStageInt)
        {
            CameraManager.Instance.virtualCamera.GetComponentInChildren<CinemachineConfiner2D>().m_BoundingShape2D = polyRange;
            StageAction();
        }
    }

    public abstract void StageAction();

}
