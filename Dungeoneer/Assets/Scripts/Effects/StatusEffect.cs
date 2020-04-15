using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Status Effect 1", menuName = "New Effect/Status Effect")]
public class StatusEffect : Effect
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
        receiver.taunted = taunted;
        receiver.stunned = stunned;
        receiver.protect = protect;
        receiver.bubbled = bubbled;
        //I know that this may seem redundant, but this ensures that the target becomes silenced.
    }

    public override void OnEndOfTurn(Entity user, Entity receiver)
    {

    }

    public override void OnEndOfTurn(Entity effecty)
    {
        effecty.silenced = silence;
        effecty.taunted = taunted;
        effecty.stunned = stunned;
        effecty.bubbled = bubbled;
    }
}
