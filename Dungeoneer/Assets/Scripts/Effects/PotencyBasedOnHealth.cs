using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New HP Based Damage 1", menuName = "New Effect/ HPPotency")]
public class PotencyBasedOnHealth : Effect
{
    public enum DamageType { Physical, Magical };
    [Range(0, 1.0f)]
    public float critChance;
    [Range(0, 2.0f)]
    public float damageMod;
    [Range(0, 0.5f)]
    public float maxHealthMod; //How many times more powerful can the modifier get?
    [SerializeField] private DamageType damageType;
    public override void OnDamageDealt(Entity user, Entity receiver)
    {

    }

    public override void OnDamageTaken(Entity user, Entity receiver)
    {

    }

    public override int OnDamageTaken(int dmg)
    {
        return 0;
    }

    public override void OnEffectApplied(Entity user, Entity receiver)
    {
        float modModifier = (Mathf.Ceil(user.hitpoints / 10)) * 0.1f;
        modModifier = maxHealthMod - modModifier;

        damageMod += modModifier;

        switch (damageType)
        {
            case DamageType.Physical:

                if (Random.Range(0.0f, 1.0f) <= critChance)
                {
                    receiver.CalculateDamageTaken((int)(user.CalculatePhysicalDamage() * 2 * damageMod));
                }
                else
                {
                    receiver.CalculateDamageTaken((int)(user.CalculatePhysicalDamage() * damageMod));
                }
                break;
            case DamageType.Magical:
                if (Random.Range(0.0f, 1.0f) <= critChance)
                {
                    receiver.CalculateMagicDamageTaken((int)(user.CalculateMagicDamage() * 2 * damageMod));
                }
                else
                {
                    int dmg = (int)(user.CalculateMagicDamage() * damageMod);
                    receiver.CalculateMagicDamageTaken(dmg);
                }
                break;
            default:
                break;
        }
    }

    public override void OnEndOfTurn(Entity user, Entity receiver)
    {

    }

    public override void OnEndOfTurn(Entity effecty)
    {

    }
}
