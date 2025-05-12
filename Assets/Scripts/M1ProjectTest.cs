using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{
    //Creo due 'Hero' a cui assegno dei paramteri secondo il costruttore che ho creato nella classe 'Hero'
    public Hero a = new Hero("Cloud", 150, new Stats(6, 3, 6, 7, 5, 8, 4), ELEMENT.LIGHTNING, ELEMENT.FIRE, new Weapon("Buster Sword", Weapon.DAMAGE_TYPE.PHYSICAL, ELEMENT.FIRE, new Stats(6, 4, 7, 3, 7, 8, 3)));
    public Hero b = new Hero("Sephiroth", 400, new Stats(8, 4, 4, 6, 4, 9, 3), ELEMENT.FIRE, ELEMENT.LIGHTNING, new Weapon("Masamune", Weapon.DAMAGE_TYPE.MAGICAL, ELEMENT.FIRE, new Stats(7, 6, 5, 6, 4, 4, 8)));

    //Dichiaro una variabile float 'time' per la gestione del tempo
    //float time = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Inizia il combattimento!");
    }

    // Update is called once per frame
    void Update()
    {
        //Gestisto lo scorrere del tempo tramite 'deltaTime' 
        //time += Time.deltaTime;

        //if (time > 0.75f)
        {
            //Calcolo la velocità totale di ogni 'Hero' sommando la 'spd' dello stesso a quella della sua 'Weapon' tramite i rispettivi Getter
            int totalSpeedA = a.GetStats().spd + a.GetWeapon().GetStats().spd;
            int totalSpeedB = b.GetStats().spd + b.GetWeapon().GetStats().spd;
            //Per gestire la turnazione di gioco utilizzo un IF
            if (totalSpeedA > totalSpeedB)
            {
                Debug.Log($"E' il turno di {a.GetName()}");
                MakeAnAttack(a, b);
                if (!b.IsAlive())
                {
                    Debug.Log($"L'incontro è terminato");
                    return;
                }
                else
                {
                    Debug.Log($"Adesso tocca a {b.GetName()}");
                    MakeAnAttack(b, a);
                }                    
            }
            else
            {
                Debug.Log($"E' il turno di {b.GetName()}");
                MakeAnAttack(b, a);
                if (!a.IsAlive())
                {
                    Debug.Log($"L'incontro è terminato");
                    return;
                }
                else
                {
                    Debug.Log($"Adesso tocca a {a.GetName()}");
                    MakeAnAttack(a, b);
                }
            }
            //time = 0f;
        }
    
    }

    //Creo la funzione 'MakeAnAttack' per gestire gli incontri
    public void MakeAnAttack(Hero attacker, Hero defender)
    {
        if (!a.IsAlive() || !b.IsAlive())
        {
            Debug.Log($"L'incontro è terminato");
            return;
        }
        else
        {
            Debug.Log($"{attacker.GetName()} attacca {defender.GetName()} con {attacker.GetWeapon().GetName()}");
            if (GameFormulas.HasHit(attacker.GetStats(), defender.GetStats()))
            {
                if (GameFormulas.HasElementAdvantge(attacker.GetWeapon().GetElem(), defender))
                {
                    Debug.Log($"WEAKNESS!");
                    Debug.Log($"{defender.GetName()} è debole all'elemento {attacker.GetWeapon().GetElem()} è subisce MOLTI danni!");
                }
                else if (GameFormulas.HasElementDisadvantage(attacker.GetWeapon().GetElem(), defender))
                {
                    Debug.Log($"RESIST!");
                    Debug.Log($"{defender.GetName()} è resistente all'elemento {attacker.GetWeapon().GetElem()} è subisce la META' dei danni!");
                }
                int damage = GameFormulas.CalculateDamage(attacker, defender);
                Debug.Log($"{damage} danni di tipo {attacker.GetWeapon().GetElem()} inferti a {defender.GetName()}!");
                defender.TakeDamage(damage);
            }
            if (!defender.IsAlive())
            {
                Debug.Log($"{defender.GetName()} è stato sconfitto. {attacker.GetName()} ha vinto!");
                enabled = false;
            }
        }            
    }
}
