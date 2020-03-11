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
    public int maxMana;
    public int mana; //Mana cost of an ability

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

        dmg = ((2 * attack) - ((defense + magDefense) / 2)) * 2;

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
        dmg = ((2 * magic) - ((defense + magDefense) / 2)) * 2;

        if(dmg <= 0)
        {
            dmg = 1;
        }

        return dmg;
    }

    public int CalculateHealingDone()
    {
        return (int)magic / 2;
    }

    //Damage taken from physical attacks
    public void CalculateDamageTaken(int dmg)
    {
        dmg -= defense;

        if (dmg < 0) dmg = 0;

        hitpoints -= dmg;

        if (hitpoints < 0) hitpoints = 0;

        if (hitpoints > maxHitpoints) hitpoints = maxHitpoints;
    }

    //Damage taken from magic attacks
    public void CalculateMagicDamageTaken(int dmg)
    {
        dmg -= magDefense;

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
    public void OnDamageTaken(int dmg)
    {
        //loop through effects, call OnDamageTaken for each
        for (int i = 0; i < StatusEffects.Count; i++)
        {
            for (int j = 0; j < StatusEffects[i].Count; j++)
            {

            }
        }
    }
    public void OnDamageDealt()
    {

    }
    public  void OnEndOfTurn()
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
}
