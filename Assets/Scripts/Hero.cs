using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable] public class Hero
{
    [SerializeField] private string name;
    [SerializeField] private int hp;
    [SerializeField] private Stats baseStats;
    [SerializeField] private ELEMENT resistance;
    [SerializeField] private ELEMENT weakness;
    [SerializeField] private Weapon weapon;

    public Hero (string name, int hp, Stats baseStats, ELEMENT resistance, ELEMENT weakness, Weapon weapon)
    {
        this.name = name;
        this.hp = hp;
        this.baseStats = baseStats;
        this.resistance = resistance;
        this.weakness = weakness;
        this.weapon = weapon;
    }

    //Creo i vari Getter usando l'operatore freccia
    public string GetName() => name;
    public int GetHp() => hp;
    public Stats GetStats() => baseStats;
    public ELEMENT GetResistance() => resistance;
    public ELEMENT GetWeakness() => weakness;
    public Weapon GetWeapon() => weapon;

    //Creo i vari Setter uando l'operatore freccia
    public void SetName (string name) => this.name = (string.IsNullOrEmpty(name) == true) ? "Inserire il nome dell'eroe" : name;
    public void SetHp (int hp) => this.hp = (hp <= 0) ? hp = 0 : hp;
    public void SetStats (Stats baseStats) => this.baseStats = baseStats;
    public void SetResistance (ELEMENT resistance) => this.resistance = resistance;
    public void SetWeakness(ELEMENT weakness) => this.weakness = weakness;
    public void SetWeapon(Weapon weapon) => this.weapon = weapon;

    //Creo la funzione 'AddHp'
    public void AddHp (int amount)
    {
        SetHp(hp + amount);
    }

    //Creo la funzione 'TakeDamage'
    public void TakeDamage (int damage)
    {
        AddHp(-damage);
    }

    //Creo la funzione 'IsAlive'
    public bool IsAlive()
    {
        if (this.hp > 0)
        {
            return true;
        }
        return false;
    }
}
