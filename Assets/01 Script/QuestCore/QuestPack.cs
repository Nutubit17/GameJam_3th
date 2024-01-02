using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class QuestPack : MonoBehaviour
{
    public static QuestPack Instance;

    public void Awake()
    {
        Instance = this;
        QuestPackReset();
    }
    
    [SerializeField]
    private List<string> questNames;
    
    private List<Quest> quests = new List<Quest>();
    
    private List<Action> OnEraseAction = new List<Action>();
    
    private List<Action> OnReMakeAction = new List<Action>();

    private List<Action> OnReMakeActionStack = new List<Action>();


    public void QuestPackReset()
    {
        quests.Clear();
        OnEraseAction.Clear();
        OnReMakeAction.Clear();
        OnReMakeActionStack.Clear();

        for (int i = 0; i<questNames.Count; i++)
        {
            quests.Add(null);
            OnEraseAction.Add(null);
            OnReMakeAction.Add(null);
            OnReMakeActionStack.Add(null);
            
            MakeQuest(questNames[i]);
        }
        
    }
    
    public void MakeQuest(string name)
    {
        int targetQuestIdx = questNames.IndexOf(name);
        bool isTargetQuestExist = quests[targetQuestIdx] is not null;
        
        
        if (isTargetQuestExist)
        {
            Destroy(quests[targetQuestIdx]);
            
            Component com = quests[targetQuestIdx].gameObject.AddComponent(Type.GetType(name));
            quests[targetQuestIdx] = com as Quest;
            
            OnReMakeAction[targetQuestIdx]?.Invoke();
            UpdateAddedQuest();
        }
        else
        {
            GameObject obj = new GameObject(name);
            obj.transform.SetParent(transform);
            Component com = obj.AddComponent(Type.GetType(name));

            quests[targetQuestIdx] = com as Quest;
        }
        
        
    }

    public void EraseQuest(string name)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].GetType().ToString() == name)
            {
                OnEraseAction[i]?.Invoke();
                
                quests[i] = null;
                Destroy(quests[i]);
                break;
            }
        }
    }

    
    public void AddToQuest<T>(string questName, Target<T> target) where T : Quest
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].GetType().ToString() == questName)
            {
                 target.Instance =  quests[i] as T;
                 OnEraseAction[i] += target.Stop;
                 OnReMakeActionStack[i] += target.ResetTarget;
            }
        }
        
    }

    public void UpdateAddedQuest()
    {
        OnReMakeAction = OnReMakeActionStack;
        OnReMakeActionStack = null;
    }
    
}
