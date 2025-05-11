using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class M1ProjectTest : MonoBehaviour
{
    public Hero a = new Hero("Cloud", 150, new Stats(6, 3, 6, 7, 5, 8, 4), ELEMENT.LIGHTNING, ELEMENT.NONE, new Weapon("Buster Sword", Weapon.DAMAGE_TYPE.PHYSICAL, ELEMENT.NONE, new Stats(6, 4, 7, 3, 7, 8, 3)));

    public Hero b = new Hero("Sephiroth", 400, new Stats(8, 4, 4, 6, 4, 6, 3), ELEMENT.FIRE, ELEMENT.NONE, new Weapon("Masamune", Weapon.DAMAGE_TYPE.MAGICAL, ELEMENT.LIGHTNING, new Stats(7, 6, 5, 6, 4, 4, 8)));

    float time = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 0.5f)
        {
            int totalSpeedA = a.GetStats().spd + a.GetWeapon().GetStats().spd;
            int totalSpeedB = b.GetStats().spd + b.GetWeapon().GetStats().spd;
            if (totalSpeedA > totalSpeedB)
            {
                MakeAnAttack(a, b);
                MakeAnAttack(b, a);
            }
            else
            {
                MakeAnAttack(b, a);
                MakeAnAttack(a, b);
            }
            time = 0f;
        }
    
    }

    //Creo la funzione 'MakeAnAttack' per gestire gli incontri
    public void MakeAnAttack(Hero attacker, Hero defender)
    {
        if (!a.IsAlive() || !b.IsAlive())
        {
            return;
        }
        else
        {
            Debug.Log($"{attacker.GetName()} attacca {defender.GetName()}");
            if (GameFormulas.HasHit(attacker.GetStats(), defender.GetStats()))
            {
                if (GameFormulas.HasElementAdvantge(attacker.GetWeapon().GetElem(), defender))
                {
                    Debug.Log($"WEAKNESS");
                }
                else if (GameFormulas.HasElementDisadvantage(attacker.GetWeapon().GetElem(), defender))
                {
                    Debug.Log($"RESIST");
                }
                int damage = GameFormulas.CalculateDamage(attacker, defender);
                Debug.Log($"{damage} inferti");
                defender.TakeDamage(damage);
            }
        }
        if (!defender.IsAlive())
        {
            Debug.Log($"{defender.GetName()} è stato sconfitto. {attacker.GetName()} ha vinto!");
            //enabled = false;
        }
            
    }
}
