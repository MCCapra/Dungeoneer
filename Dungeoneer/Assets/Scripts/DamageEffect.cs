using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Effect 1", menuName = "New Effect/Damage")]
public class DamageEffect : Effect
{
    public enum DamageType { Physical, Magical };
    [Range(0,1.0f)]
    public float critChance;
    [Range(0, 2.0f)]
    public float damageMod;

    [SerializeField] private DamageType damageType;
    public override void OnDamageDealt(Entity user, Entity receiver)
    {
    }

    public override void OnDamageTaken(Entity user, Entity receiver)
    {
    }

    public override void OnEndOfTurn(Entity user, Entity receiver)
    {
        switch (damageType)
        {
            case DamageType.Physical:

                if(Random.Range(0.0f, 1.0f) <= critChance)
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

        Debug.Log(user.e_name + " hit " + receiver.e_name + " with " + System.Enum.GetName(typeof(DamageType), damageType) + " damage!");

        //user.OnDamageDealt();
        //receiver.OnDamageTaken();
    }

    //Not used for now, potentially used later
    public override int OnDamageTaken(int dmg)
    {
        return dmg;
    }
}
