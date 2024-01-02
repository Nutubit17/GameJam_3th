using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentQuest : Quest
{
    public string title;
    public string goal;
    [Space]
    [Range(1, 5)] public int hardness;
}
