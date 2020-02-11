using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    /*
     * Michael Capra
     * Item Class: Represents any item in the game
     * 1/23/2020
     */


    public int hitpoints;
    public int attack;
    public int defense;
    public int magic;
    public int magDefense;
    public int speed;
    public string e_name;
    public int tier;

    public Effect ability; //This is the ability that the item may or may not have
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
