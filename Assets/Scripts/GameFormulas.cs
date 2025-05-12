using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public static class GameFormulas
{
    //Creo la funzione 'HasElementAdvantge' per valutare se chi subisce è debole all'elemento dell'arma di chi attacca
    public static bool HasElementAdvantge(ELEMENT attackElement, Hero defender)
    {
        if (attackElement == defender.GetWeakness())
        {
            return true;
        }
        return false;
    }


    //Creo la funzione 'HasElementDisadvantage' per valutare se chi subisce è resistente all'elemento dell'arma di chi attacca
    public static bool HasElementDisadvantage(ELEMENT attackElement, Hero defender)
    {
        if (attackElement == defender.GetResistance())
        {
            return true;
        }
        return false;
    }


    //Creo la funzione 'EvaluateElementalModifier' per aumentare o ridurre il danno in base alle debolezze o resistenze di chi subisce
    public static float EvaluateElementalModifier(ELEMENT attackElement, Hero defender)
    {
        if (HasElementAdvantge(attackElement, defender))
        {
            return 1.5f;
        }
        else if (HasElementDisadvantage(attackElement, defender))
        {
            return 0.5f;
        }
        else
        {
            return 1;
        }
    }

    //Creo la funzione 'HasHit' per valutare se il colpo è andato a segno
    public static bool HasHit(Stats attacker, Stats defender)
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

    //Creo la funzione 'IsCrit' per valutare se è un colpo critico
    public static bool IsCrit(int critValue)
    {
        int randomNumber = Random.Range(0, 100);
        if (randomNumber < critValue)
        {
            Debug.Log("CRIT");
            return true;
        }
        return false;
    }

    //Creo la funzione 'CalculateDamage' per il calcolo dei danni
    public static int CalculateDamage(Hero attacker, Hero defender)
    {
        Stats attackerStatsSum = Stats.Sum(attacker.GetStats(), attacker.GetWeapon().GetStats());
        Stats defenderStatsSum = Stats.Sum(defender.GetStats(), defender.GetWeapon().GetStats());
       
        int defType = 0;

        if (attacker.GetWeapon().GetDmgType() == Weapon.DAMAGE_TYPE.PHYSICAL)
        {
            defType = defender.GetStats().def;
        }
        else if (attacker.GetWeapon().GetDmgType() == Weapon.DAMAGE_TYPE.MAGICAL)
        {
            defType = defender.GetStats().res;
        }

        int damage = attackerStatsSum.atk - defType;
        float damageMultiplier = EvaluateElementalModifier(attacker.GetWeapon().GetElem(), defender);
        damage = Mathf.RoundToInt(damage * damageMultiplier);

        if (IsCrit(damage))
        {
            damage *= 2;
        }

        if (damage < 0)
        {
            return 0;
        }
        else
        {
            return damage;
        }
    }
}