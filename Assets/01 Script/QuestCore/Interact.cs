using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public abstract class Interact<Q, T, U> where Q : Quest where T : Target<Q> where U : Target<Q>
{
    public T target0;
    public U target1;
    
    public Interact(T target0, U target1)
    {
        this.target0 = target0;
        this.target1 = target1;
        Init();
    }

    public abstract void Init();

}
