using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankEffect : Effect
{
    //Meaybe thisworks
    public override void OnDamageDealt(Entity user, Entity receiver)
    {

    }

    public override void OnDamageTaken(Entity user, Entity receiver)
    {

    }

    public override int OnDamageTaken(int dmg)
    {
        return dmg;
    }

    public override void OnEffectApplied(Entity user, Entity receiver)
    {

    }

    public override void OnEndOfTurn(Entity user, Entity receiver)
    {

    }

    public override void OnEndOfTurn(Entity effecty)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
