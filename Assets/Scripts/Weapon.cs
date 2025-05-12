using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Weapon
{
    //Creo l'enumeratore 'DAMAGE_TYPE'
    public enum DAMAGE_TYPE
    {
        PHYSICAL,
        MAGICAL,
    }

    //Creo le variabili private serializzabili
    [SerializeField] private string name;
    [SerializeField] private DAMAGE_TYPE dmgType;
    [SerializeField] private ELEMENT elem;
    [SerializeField] private Stats bonusStats;

    //Creo un costruttore 'Weapon' che assegna tutti i valori tra parentesi
    public Weapon(string name, DAMAGE_TYPE dmgType, ELEMENT elem, Stats bonusStats)
    {
        this.name = name;
        this.dmgType = dmgType;
        this.elem = elem;
        this.bonusStats = bonusStats;
    }

    //Creo i vari Getter uando l'operatore freccia;
    public string GetName() => name;
    public DAMAGE_TYPE GetDmgType() => dmgType;
    public ELEMENT GetElem() => elem;
    public Stats GetStats() => bonusStats;

    //Creo i vari Setter usando l'operatore freccia;
    public void SetName(string name) => this.name = (string.IsNullOrEmpty(name) == true) ? "Inserire il nome dell'arma" : name;
    public void SetDmgType(DAMAGE_TYPE dmgType) => this.dmgType = dmgType;
    public void SetElement(ELEMENT elem) => this.elem = elem;
    public void SetStats(Stats bonusStats) => this.bonusStats = bonusStats;
}
