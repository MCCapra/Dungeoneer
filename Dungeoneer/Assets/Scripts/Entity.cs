using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    /*
     * Michael Capra
     * Entity Class: Holds stats for characters  
     * 1/22/2020
     */


    //Fields 
    public int hitpoints; //Current hitpoints
    public int maxHitpoints; //Maximum hitpoints
    public int attack;
    public int defense;
    public int magic;
    public int magDefense;
    public int speed;
    public string e_name;
    public int tier;
    public int maxMana; //Maximum mana
    public int mana; //Current mana

    //These values are calculated at the end of each turn
    public int attMod;
    public int defMod;
    public int magMod;
    public int mdefMod;
    public int spdMod;

    public int effectMaxLength; //Maximum length of ANY effect in the game.

    public Action basicAttack;
    public Entity target;

    public List<List<Effect>> StatusEffects;
    public List<Action> skills; //Skills that the player will have access to.
    //Calculation for the physical damage dealt by the entity
    public int CalculatePhysicalDamage()
    {
        int dmg = 0;

        //calculation here

        dmg = ((2 * (attack + attMod)) - (((defense + defMod) + (magDefense + mdefMod)) / 2)) * 2;

        if(dmg <= 0)
        {
            dmg = 1;
        }

        return dmg;
    }

    public int CalculateMagicDamage()
    {
        int dmg = 0;

        //calculation here
        dmg = ((2 * (magic + magMod)) - (((defense + defMod) + (magDefense + mdefMod)) / 2)) * 2;

        if(dmg <= 0)
        {
            dmg = 1;
        }

        return dmg;
    }

    public int CalculateHealingDone()
    {
        return (int)(magic + magMod) / 2;
    }

    //Damage taken from physical attacks
    public void CalculateDamageTaken(int dmg)
    {
        dmg -= (defense + defMod);

        if (dmg < 0) dmg = 0;

        hitpoints -= dmg;

        if (hitpoints < 0) hitpoints = 0;

        if (hitpoints > maxHitpoints) hitpoints = maxHitpoints;
    }

    //Damage taken from magic attacks
    public void CalculateMagicDamageTaken(int dmg)
    {
        dmg -= (magDefense - mdefMod);

        if (dmg < 0) dmg = 0;

        hitpoints -= dmg;

        if (hitpoints < 0) hitpoints = 0;
        if (hitpoints > maxHitpoints) hitpoints = maxHitpoints;
    }

    public void CalculateHealingTaken(int heal)
    {
        hitpoints += heal;

        if (hitpoints > maxHitpoints) hitpoints = maxHitpoints;
    }
    
    //These methods will be overrriden by the specific enemy/character using them.
    public virtual void OnDamageTaken(int dmg)
    {
    }
    public virtual void OnDamageDealt()
    {

    }
    public virtual void OnEndOfTurn()
    {

    } 
    public virtual void TakeTurn() { }

    private void Start()
    {
        maxHitpoints = hitpoints;
        StatusEffects = new List<List<Effect>>();

        for(int i = 0; i < effectMaxLength; i++)
        {
            StatusEffects.Add(new List<Effect>());
        }
    }

    public void ApplyEffect(Effect eff)
    {
        StatusEffects[eff.EffectLength].Add(eff);
    }
}
