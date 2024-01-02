using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string[] targetList;
    public InteractValue[] interactList;
    
}


[System.Serializable]
public struct InteractValue
{
    public string Name;
    [Space]
    public string From;
    public string To;
}
