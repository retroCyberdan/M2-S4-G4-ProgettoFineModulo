using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{
    public Hero a;
    public Hero b;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //else
        {
            int totalSpeedA = a.GetStats().spd + a.GetWeapon().GetStats().spd;
            int totalSpeedB = b.GetStats().spd + b.GetWeapon().GetStats().spd;
            if (totalSpeedA >= totalSpeedB)
            {
                MakeAnAttack(a, b);
                MakeAnAttack(b, a);
            }
            else
            {
                MakeAnAttack(b, a);
                MakeAnAttack(a, b);
            }
        }
    }

    //Creo la funzione 'MakeAnAttack' per gestire gli incontri
    private void MakeAnAttack(Hero attacker, Hero defender)
    {
        if (!a.IsAlive())
        {
            Debug.Log($"{b.GetName()} ha vinto");
            return;
        }
        else if (!b.IsAlive())
        {
            Debug.Log($"{a.GetName()} ha vinto");
            return;
        }
        else
        {
            Debug.Log($"{attacker.GetName()} attacca {defender.GetName()}");
            if (GameFormulas.HasHit(attacker.GetStats(), defender.GetStats()))
            {
                if (GameFormulas.EvaluateElementalModifier(attacker.GetWeapon().GetElem(), defender) == 1.5)
                {
                    Debug.Log($"WEAKNESS");
                }
                else
                {
                    Debug.Log($"RESIST");
                }
                int damage = GameFormulas.CalculateDamage(attacker, defender);
                Debug.Log($"{damage} inferti");
                defender.TakeDamage(damage);
            }
        }
            
    }
}
