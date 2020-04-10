using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName= "New Heal Effect 1", menuName = "New Effect/Heal")]

public class HealEffect : Effect
{
    [Range(0, 2.0f)]
    public float healMod;
    [Range(0, 20)]
    public int flatHeal;
    public bool isFlat;
    public override void OnDamageDealt(Entity user, Entity receiver)
    {
    }

    public override void OnDamageTaken(Entity user, Entity receiver)
    {
    }

    public override void OnEffectApplied(Entity user, Entity receiver)
    {
        if (isFlat)
        {
            receiver.CalculateHealingTaken(flatHeal);
        }
        else
        {
            receiver.CalculateHealingTaken((int)(user.CalculateHealingDone() * healMod));
        }
    }

    public override void OnEndOfTurn(Entity user, Entity receiver)
    {
        
    }

    public override int OnDamageTaken(int dmg)
    {
        return dmg;
    }

    public override void OnEndOfTurn(Entity effecty)
    {
    }
}
