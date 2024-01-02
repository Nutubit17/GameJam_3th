using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Target<Q,T,U>
    where Q : Quest<Q,T,U>
    where T : Target<Q,T,U>
    where U : Interact<Q,T,U>
{
    [NonSerialized] public List<Interact<Q, T, U>> interacts = new List<Interact<Q, T, U>>();
    
    public Target()
    {
        Quest<Q,T,U>.OnSpawn += OnQuestSpawn;
        Quest<Q,T,U>.OnDead += OnQuestDead;
    }
    
    protected abstract void OnQuestSpawn();

    protected virtual void OnQuestDead()
    {
        interacts = null;
    }

    public virtual void Add(U interact)
    {
        if (interact.target0 == this)
        {
            interact.target1.Add(interact);
        }
        else if(interact.target1 == this)
        {
            interact.target0.Add(interact);
        }
        else
        {
            return;
        }
        
        bool isInteractfinded = false;
        int sampleInteractIdx = 0;
        
        var sampleList = interacts.Where(x =>
            {
                if (x is null) throw new NullReferenceException();
                
                bool result = x.target0 == interact.target0 && x.target1 == interact.target1;

                if (result)
                    isInteractfinded = true;
                
                if(!isInteractfinded)
                    sampleInteractIdx++;
                
                return result;
            })
            .ToArray();

        if (sampleList.Count() >= 1)
        {
            interacts[sampleInteractIdx] = interact;
        }
        else
        {
            interacts.Add(interact);
        }
        
    }

}
