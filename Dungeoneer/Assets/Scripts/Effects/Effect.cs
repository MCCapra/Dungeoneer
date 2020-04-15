using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    /*
     * Michael Capra
     * Effect Class: Represents any effect in the game (debuff, buff, passive, etc...)
     * 1/23/2020
     */
    [Range(0,5.0f, order = 1)]
    public int EffectLength; //Used for determining length of a lasting effect (damage doesn't care about this)
    public bool silence;
    public bool taunted;
    public bool stunned;
    public bool protect;
    public bool bubbled;

    public bool isNegative; //Bad effect
    public abstract void OnDamageTaken(Entity user, Entity receiver);

    public abstract int OnDamageTaken(int dmg);
    public abstract void OnDamageDealt(Entity user, Entity receiver);
    public abstract void OnEffectApplied(Entity user, Entity receiver);

    public abstract void OnEndOfTurn(Entity user, Entity receiver);
    public abstract void OnEndOfTurn(Entity effecty);
}
