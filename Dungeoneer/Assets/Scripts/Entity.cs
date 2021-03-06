﻿using System.Collections;
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

    public Action basicAttack;
    public Entity target;

    public List<Action> skills; //Skills that the player will have access to.
    //Calculation for the physical damage dealt by the entity
    public int CalculatePhysicalDamage()
    {
        int dmg = 0;

        //calculation here

        dmg = ((2 * attack) - ((defense + magDefense) / 2)) * 2;

        return dmg;
    }

    public int CalculateMagicDamage()
    {
        int dmg = 0;

        //calculation here
        dmg = ((2 * magic) - ((defense + magDefense) / 2)) * 2;
        return dmg;
    }


    //Damage taken from physical attacks
    public void CalculateDamageTaken(int dmg)
    {
        hitpoints -= (dmg - defense);

        if (hitpoints < 0) hitpoints = 0;
    }

    //Damage taken from magic attacks
    public void CalculateMagicDamageTaken(int dmg)
    {
        hitpoints -= (dmg - magDefense);
        if (hitpoints < 0) hitpoints = 0;
    }

    public void Heal(int heal)
    {
        hitpoints += heal;

        if (hitpoints > maxHitpoints) hitpoints = maxHitpoints;
    }

    //These methods will be overrriden by the specific enemy/character using them.
    public virtual void OnDamageTaken() { }
    public virtual void OnDamageDealt() { }
    public virtual void OnEndOfTurn() { } 

    public virtual void TakeTurn() { }

    private void Start()
    {
        maxHitpoints = hitpoints;
    }
}
