using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class QuestPack : MonoBehaviour
{
    public static QuestPack Instance;
    public TimeControlPrincipal controlTest;
    
    private void Awake()
    {
        TimeController.questPack = Instance;
        
        controlTest.TryTimeControl();
        
    }
}
