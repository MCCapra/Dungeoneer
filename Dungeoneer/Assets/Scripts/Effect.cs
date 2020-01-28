using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    /*
     * Michael Capra
     * Effect Class: Represents any effect in the game (debuff, buff, passive, etc...)
     * 1/23/2020
     */

    public abstract void OnDamageTaken();
    public abstract void OnDamageDealt();
    public abstract void OnEndOfTurn();
    public abstract void OnEnemyKill();
}
