using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public static class GameFormulas
{
    public static bool HasElementAdvantge (ELEMENT attackElement, Hero defender)
    {
        if (attackElement == defender.GetWeakness())
        {
            return true;
        }
        return false;
    }

    public static bool HasElementDisadvantage (ELEMENT attackElement, Hero defender)
    {
        if (attackElement == defender.GetResistance())
        {
            return true;
        }
        return false;
    }

    public static float EvaluateElementalModifier(ELEMENT attackElement, Hero defender)
    {
        if (HasElementAdvantge(attackElement, defender) == true)
        {
            return 1.5f;
        }
        else if (HasElementDisadvantage(attackElement, defender) == true)
        {
            return 0.5f;
        }
        else
        {
            return 1;
        }
    }

    public static bool HasHit (Stats attacker, Stats defender)
    {
        int hitChance = attacker.aim - defender.eva;
        int randomNumber = Random.Range(0, 100);
        if (randomNumber > hitChance)
        {
            Debug.Log("MISS");
            return false;
        }
        return true;
    }

    public static bool IsCrit (int critValue)
    {
        int randomNumber = Random.Range(0, 100);
        if (randomNumber < critValue)
        {
            Debug.Log("CRIT");
            return true;
        }
        return false;
    }

    public static int CalculateDamage(Hero attacker, Hero defender)
    {
        Stats attackerStatsSum = Stats.Sum(attacker.GetStats(), attacker.GetWeapon().GetStats());
        Stats defenderStatsSum = Stats.Sum(defender.GetStats(), defender.GetWeapon().GetStats());
        int defType;

        if (attacker.GetWeapon().GetDmgType() == Weapon.DAMAGE_TYPE.PHYSICAL)
        {
            defType = defender.GetStats().def;
        }
        else
        {
            defType = defender.GetStats().res;
        }

        int baseDamage = attackerStatsSum.atk - defType;
        float damageMultiplier = EvaluateElementalModifier(attacker.GetWeapon().GetElem(), defender);
        float finalDamage = baseDamage * damageMultiplier;

        if (IsCrit(Mathf.RoundToInt(finalDamage)) == true)
        {
            finalDamage *= 2;
        }

        if (finalDamage < 0)
        {
            return 0;
        }
        else
        {
            return Mathf.RoundToInt(finalDamage);
        }
    }
}