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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Calculation for the physical damage dealt by the entity
    public int CalculatePhysicalDamage()
    {
        int dmg = 0;

        //calculation here

        return dmg;
    }

    public int CalculateMagicDamage()
    {
        int dmg = 0;

        //calculation here

        return dmg;
    }
}
