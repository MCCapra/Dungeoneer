using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Poison Effect 1", menuName = "New Effect/Poison")]

public class PoisonEffect : Effect
{
    [Range(0, 1)]
    public float poisonMod;
    public override void OnDamageDealt(Entity user, Entity receiver)
    {
    }

    public override void OnDamageTaken(Entity user, Entity receiver)
    {
    }

    public override int OnDamageTaken(int dmg)
    {
        return dmg;
    }

    public override void OnEffectApplied(Entity user, Entity receiver)
    {
        Debug.Log("applied");
    }

    public override void OnEndOfTurn(Entity user, Entity receiver)
    {
    }

    public override void OnEndOfTurn(Entity effecty)
    {
        int dmg = (int)(effecty.maxHitpoints * poisonMod);

        if(dmg <= 0)
        {
            dmg = 1;
        }

        effecty.CalculateTrueDamageTaken(dmg);

        Debug.Log(effecty.name + " has taken " + dmg + " poison damage!");
    }
}
