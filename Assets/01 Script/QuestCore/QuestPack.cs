using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPack : MonoBehaviour
{
    public static QuestPack Instance;

    public void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private SystemQuest[] _systemQuests;

    
    [SerializeField]
    private ContentQuest[] _contentQuests;


}
