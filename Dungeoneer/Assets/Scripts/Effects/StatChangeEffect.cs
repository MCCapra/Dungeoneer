using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Stat Change", menuName ="New Effect/Stat Change")]
public class StatChangeEffect : Effect
{
    [Range(-1,1)]
    public float attMod;
    [Range(-1, 1)]
    public float defMod;
    [Range(-1, 1)]
    public float mDefMod;
    [Range(-1, 1)]
    public float spdMod;
    [Range(-1, 1)]
    public float magMod;
    [Range(-1, 1)]
    public float dmgMod;
    [Range(-1, 1)]
    public float magDmgMod;
    [Range(-1, 1)]
    public float dmgTknMod;
    [Range(0, 1)]
    public float dodgeChance;

    public bool isFlat;
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
        UpdateStats(receiver);
    }

    public override void OnEndOfTurn(Entity user, Entity receiver)
    {

    }

    public override void OnEndOfTurn(Entity effecty)
    {
        UpdateStats(effecty);
    }

    private void UpdateStats(Entity receiver)
    {
        if (isFlat)
        {
            receiver.attMod += (int)(10 * attMod);
            receiver.defMod += (int)(10 * defMod);
            receiver.magMod += (int)(10 * magMod);
            receiver.mdefMod += (int)(10 * mDefMod);
            receiver.spdMod += (int)(10 * spdMod);
        }
        else
        { 
            receiver.attMod += (int)(receiver.attack * attMod);
            receiver.defMod += (int)(receiver.defense * defMod);
            receiver.magMod += (int)(receiver.magic * magMod);
            receiver.mdefMod += (int)(receiver.magDefense* mDefMod);
            receiver.spdMod += (int)(receiver.speed * spdMod);
        }

        receiver.dmgMod +=  dmgMod;
        receiver.magDmgMod += magDmgMod;
        receiver.dmgTknMod += dmgTknMod;
        receiver.dodgeChance += dodgeChance;

        receiver.stunned = stunned;
        receiver.taunted = taunted;
        receiver.bubbled = bubbled;
        receiver.protect = protect;
        receiver.silenced = silence;
    }
}
