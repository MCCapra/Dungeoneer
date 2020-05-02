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
    public Action enemySkill;
    void Start()
    {

    }

    public int GiveGold()
    {
        return gold_reward;
    }

    public Action chooseAction()
    {
        if(Random.Range(0,1) > .5)
        {
            return basicAttack;
        }
        else
        {
            return enemySkill;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
