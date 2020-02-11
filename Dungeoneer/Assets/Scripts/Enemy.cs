using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    /*
     * Michael Capra
     * Enemy Class: Inherits from entity, represents the enemies to be seen in the dungeon
     * 2/11/2020
     */

    public int gold_reward; //Gold rewarded on death
    public GameObject itemDrop; //Each enemy has a chance to drop an item on death, this will be randomized.
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
