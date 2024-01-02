using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeController : Quest<TimeController, TimeControlTarget, TimeObjectInteract>
{
    public static QuestPack questPack;
    
    public override void DeadCheck()
    {
        
    }
}
public class TimeControlTarget : Target<TimeController, TimeControlTarget, TimeObjectInteract>
{
    protected override void OnQuestSpawn()
    {
        
    }
}

[System.Serializable]
public class TimeControlPrincipal : TimeControlTarget
{
    public MonoBehaviour mono;
    public float time = 2f;
    public float currentTime = 1f;
    public float nextTime = 0.0001f;

    public List<TimeControlHelpObject> conditionGettingObjects = new List<TimeControlHelpObject>();

    private IEnumerator timeRoutine;
    
    public void TryTimeControl()
    {
        bool isCanStart = true;

        foreach (var item in conditionGettingObjects)
        {
            isCanStart = isCanStart && item.condition(mono);
            if(!isCanStart) return;
        }
        
        timeRoutine = TimeControlRoutine();
        mono.StartCoroutine(timeRoutine);
    }

    public IEnumerator TimeControlRoutine()
    {
        float percent = 0;

        Time.timeScale = nextTime;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = currentTime;

    }

    public override void Add(TimeObjectInteract interact)
    {
        base.Add(interact);
        
        if (interact.target1 is TimeControlHelpObject)
        {
            conditionGettingObjects.Add(interact.target1 as TimeControlHelpObject);
        }
    }

    protected override void OnQuestSpawn()
    {
        base.OnQuestSpawn();

        if(timeRoutine is not null)
            mono.StartCoroutine(timeRoutine);
    }
    protected override void OnQuestDead()
    {
        base.OnQuestDead();
        
        if(timeRoutine is not null)
            mono.StopCoroutine(timeRoutine);
    }
}

public class TimeControlHelpObject : TimeControlTarget
{
    public Predicate<MonoBehaviour> condition;
}

public class TimeObjectInteract : Interact<TimeController, TimeControlTarget, TimeObjectInteract>
{
    public TimeObjectInteract(TimeControlTarget target0, TimeControlTarget target1) : base(target0, target1)
    {
        
    }
}
public class TimeInteractNormal : TimeObjectInteract
{
    public TimeInteractNormal(TimeControlTarget target0, TimeControlTarget target1) : base(target0, target1)
    {
    }
}