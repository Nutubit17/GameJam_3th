using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public abstract class Interact<Q,T,U>
    where Q : Quest<Q,T,U>
    where T : Target<Q,T,U>
    where U : Interact<Q,T,U>
{
    public T target0;
    public T target1;
    
    public Interact(T target0, T target1)
    {
        this.target0 = target0;
        this.target1 = target1;
    }
    
}
