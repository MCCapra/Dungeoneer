using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Damage + Effect", menuName = "New Effect/Damage+Effect")]
public class DamagewithEffectEffect : Effect
{
    public enum DamageType { Physical, Magical};
    [Range(0, 1.0f)]
    public float critChance;
    [Range(0, 2.0f)]
    public float damageMod;
    [Range(0, 1.0f)]
    public float additionalEffectChance;
    public bool selfApplied; //Whether additional effect is applied to enemy or self

    public Effect appliedEffect;

    [SerializeField] private DamageType damageType;
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

        if (Random.Range(0.0f, 1.0f) <= additionalEffectChance)
        {
            if (selfApplied)
            {
                appliedEffect.OnEffectApplied(user, user);
                user.ApplyEffect(appliedEffect);
            }
            else
            {
                appliedEffect.OnEffectApplied(user, receiver);
                receiver.ApplyEffect(appliedEffect);
            }
        }
    }

    public override void OnEndOfTurn(Entity user, Entity receiver)
    {
    }

    public override void OnEndOfTurn(Entity effecty)
    {
    }
}
