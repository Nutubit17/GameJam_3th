using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target<T> : MonoBehaviour where T : Quest
{
    public T Instance;

    public virtual void Start()
    {
        QuestPack.Instance.AddToQuest(typeof(T).ToString(), this);        
    }

    public void ResetTarget()
    {
        QuestPack.Instance.AddToQuest(typeof(T).ToString(), this);
    }
    
    public abstract void Init();
    public abstract void Stop();
}
