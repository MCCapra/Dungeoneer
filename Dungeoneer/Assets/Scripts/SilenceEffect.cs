using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Silence", menuName = "New Effect/Silence")]
public class SilenceEffect : Effect
{
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
        receiver.silenced = silence;

        //I know that this may seem redundant, but this ensures that the target becomes silenced.
    }

    public override void OnEndOfTurn(Entity user, Entity receiver)
    {

    }

    public override void OnEndOfTurn(Entity effecty)
    {
        effecty.silenced = silence;
    }
}
