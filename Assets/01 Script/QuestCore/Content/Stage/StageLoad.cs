using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageLoad : MonoBehaviour
{
    public int loadingIdx = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Stage.currentStage = loadingIdx;
            
            foreach (var stage in Stage.stages.ToArray())
            {
                stage.CheckThisStage();
            }
        }
    }
}
