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

    public float dmgMod;
    public float magDmgMod;
    public float dmgTknMod;

    public float dodgeChance;
    
    //conditions
    public bool silenced;
    public bool taunted;
    public bool stunned;
    public bool protect;
    public bool bubbled;

    public Action basicAttack;
    public Entity target;

    public List<List<Effect>> StatusEffects;
    public List<int> Test;
    public List<Action> skills; //Skills that the player will have access to.

    public Sprite icon;

    //Calculation for the physical damage dealt by the entity
    public int CalculatePhysicalDamage()
    {
        int dmg = 0;

        //calculation here

        dmg = ((2 * (attack + attMod)) - (((defense + defMod) + (magDefense + mdefMod)) / 2)) * 2;

        dmg = (int)(dmg * dmgMod);

        if (dmg <= 0) dmg = 1;

        return dmg;
    }

    public int CalculateMagicDamage()
    {
        int dmg = 0;

        //calculation here
        dmg = ((2 * (magic + magMod)) - (((defense + defMod) + (magDefense + mdefMod)) / 2)) * 2;

        dmg = (int)(dmg * magDmgMod);

        if (dmg <= 0) dmg = 1;

        return dmg;
    }

    public int CalculateHealingDone()
    {
        return (int)(magic + magMod) / 2;
    }

    public void CalculateTrueDamageTaken(int dmg)
    {
        if(protect)
        {
            protect = false;
            return;
        }

        hitpoints -= dmg;

        if (hitpoints < 0) hitpoints = 0;

        if (hitpoints > maxHitpoints) hitpoints = maxHitpoints;
    }

    //Damage taken from physical attacks
    public void CalculateDamageTaken(int dmg)
    {
        if (protect)
        {
            protect = false;
            return;
        }

        dmg -= (defense + defMod);

        dmg = (int)(dmg * dmgTknMod);

        if (dmg <= 0) dmg = 1;

        float dodge = Random.Range(0, 1);

        if(dodge <= dodgeChance)
        {
            dmg = 0;
        }

        hitpoints -= dmg;

        if (hitpoints < 0) hitpoints = 0;

        if (hitpoints > maxHitpoints) hitpoints = maxHitpoints;
    }

    //Damage taken from magic attacks
    public void CalculateMagicDamageTaken(int dmg)
    {
        if (protect)
        {
            protect = false;
            return;
        }

        dmg -= (magDefense - mdefMod);

        dmg = (int)(dmg *dmgTknMod);

        if (dmg <= 0) dmg = 1;

        float dodge = Random.Range(0, 1);

        if (dodge <= dodgeChance)
        {
            dmg = 0;
        }

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
        //reset all mods and conditions to default
        DefaultMods();
        //call end of turn for each effect
        foreach (List<Effect> list in StatusEffects)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if (target)
                {
                    list[i].OnEndOfTurn(this, target);
                }

                list[i].OnEndOfTurn(this);
            }
        }

        //tick down effects
        for(int i = 0; i < StatusEffects.Count -1; i++)
        {
            StatusEffects[i] = StatusEffects[i + 1];
        }

        StatusEffects[StatusEffects.Count - 1] = new List<Effect> { ScriptableObject.CreateInstance<BlankEffect>() };

        //clear out target
        if(!taunted) target = null;
    } 
    public virtual void TakeTurn() { }

    private void Start()
    {
        OnSpawn();
    }

    public void ApplyEffect(Effect eff)
    {
        int lengthInt = eff.EffectLength - 1;
        StatusEffects[(eff.EffectLength -1)].Add(eff);
    }

    public void OnSpawn()
    {
        maxHitpoints = hitpoints;

        StatusEffects = new List<List<Effect>>();

        for (int i = 0; i < 5; i++)
        {
            List<Effect> effList = new List<Effect>();
            effList.Add(ScriptableObject.CreateInstance<BlankEffect>());
            StatusEffects.Add(effList);
        }

        DefaultMods();
    }

    private void DefaultMods()
    {
        attMod = 0;
        defMod = 0;
        magMod = 0;
        mdefMod = 0;
        spdMod = 0;
        dodgeChance = 0;

        dmgMod = 1.0f;
        magDmgMod = 1.0f;
        dmgTknMod = 1.0f;

        silenced = false;
        taunted = false;
        stunned = false;
        bubbled = false;
    }
}
