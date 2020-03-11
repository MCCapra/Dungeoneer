using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName= "New Heal Effect 1", menuName = "New Effect/Heal")]

public class HealEffect : Effect
{
    [Range(0, 2.0f)]
    public float healMod;
    public override void OnDamageDealt(Entity user, Entity receiver)
    {
    }

    public override void OnDamageTaken(Entity user, Entity receiver)
    {
    }

    public override void OnEndOfTurn(Entity user, Entity receiver)
    {
        receiver.CalculateHealingTaken((int)(user.CalculateHealingDone() * healMod));
    }

    public override int OnDamageTaken(int dmg)
    {
        return dmg;
    }
}
