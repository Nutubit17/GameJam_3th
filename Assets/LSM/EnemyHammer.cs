using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHammer : Enemy
{
    public override void AttackOn()
    {
        base.AttackOn();
        SoundManager.instance.MakeParticle(Vector2.one, "Hammer", true, 0.5f, 0.5f);
    }
}
