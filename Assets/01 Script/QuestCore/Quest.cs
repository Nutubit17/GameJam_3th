using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest<Q,T,U> 
    where Q : Quest<Q,T,U>
    where T : Target<Q,T,U>
    where U : Interact<Q,T,U>
{

    private bool IsAlive;
    public bool isAlive
    {
        get => isAlive;
        set
        {
            IsAlive = value;
            if (value)
            {
                Spawn();
            }
            else
            {
                Die();
            }
        }

    }
    
    public static System.Action OnSpawn = null;
    public static System.Action OnDead = null;
    
    public abstract void DeadCheck();

    protected void Spawn()
    {
        if(isAlive) return;
        OnSpawn?.Invoke();
        isAlive = true;
    }
    
    protected void Die()
    {
        if(!isAlive) return;
        OnDead?.Invoke();
        isAlive = false;
    }
    
    
}

