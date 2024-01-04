using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySward : Enemy
{
    public override void AttackOn()
    {
        base.AttackOn();
        SoundManager.instance.MakeParticle(new Vector2(1, 3), "LongSward", true, 0.5f, 0.5f);
    }
}
