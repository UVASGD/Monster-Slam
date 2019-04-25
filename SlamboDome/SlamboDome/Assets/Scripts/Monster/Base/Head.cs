using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HitDel(float damage, Vector2 dir);

public class Head : MonoBehaviour
{
    float crit_multiplier = 2, damage_threshold = 15f;

    public event HitDel DamageEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float damage = collision.contacts[0].normalImpulse;
        Damager d = collision.collider.GetComponent<Damager>();
        if (d)
        {
            damage += d.GetDamage();
        }

        if (damage > damage_threshold)
        {
            DamageEvent?.Invoke(damage * crit_multiplier, collision.contacts[0].normal);
        }
    }

}
