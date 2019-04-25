using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostCalculator : MonoBehaviour
{
    const float GUN_COST = 40, SHIELD_COST = 25, SWORD_COST = 20;

    float cost, cost_limit = 100;
    // Start is called before the first frame update
    void Start()
    {
        MonsterBody mb = GetComponent<MonsterBody>();
        cost += mb.health;
        cost += mb.armor;
        cost += mb.move_speed / 2;

        cost += GetComponentsInChildren<Gun>().Length * GUN_COST;
        cost += GetComponentsInChildren<Shield>().Length * SHIELD_COST;
        cost += GetComponentsInChildren<Sword>().Length * SWORD_COST;

        if (cost > cost_limit) 
        {
            print("TOO COSTLY");
            Destroy(gameObject);
        }
    }
}
