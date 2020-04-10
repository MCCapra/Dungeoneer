using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new KO effect", menuName ="New Effect/ KO PAUNCH")]
public class KOEffect : Effect
{
    [Range(0, 1)]
    public float koPercentage;

    public override void OnDamageDealt(Entity user, Entity receiver)
    {

    }

    public override void OnDamageTaken(Entity user, Entity receiver)
    {

    }

    public override int OnDamageTaken(int dmg)
    {
        return 0;
    }

    public override void OnEffectApplied(Entity user, Entity receiver)
    {
        float ko = Random.Range(0.0f, 1.0f);

        if(ko <= koPercentage)
        {
            receiver.hitpoints = 0;
        }
    }

    public override void OnEndOfTurn(Entity user, Entity receiver)
    {

    }

    public override void OnEndOfTurn(Entity effecty)
    {

    }
}
