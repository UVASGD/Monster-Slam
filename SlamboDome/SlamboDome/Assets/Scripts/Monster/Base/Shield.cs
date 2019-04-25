using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float health = 100f, armor = 15f;
    float current_health;

    public GameObject destroy_fx, hit_fx;

    SpriteRenderer s;

    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<SpriteRenderer>();
        current_health = health;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float damage = collision.contacts[0].normalImpulse;
        Damager d = collision.collider.GetComponent<Damager>();
        if (d)
        {
            damage += d.GetDamage();
        }

        if (damage > armor)
        {
            current_health -= damage;
            s.color = new Color(s.color.r, s.color.g, s.color.b, current_health/health);
            FX_Spawner.instance.SpawnFX(hit_fx, transform.position, Vector2.up);
            if (current_health < 0)
            {
                FX_Spawner.instance.SpawnFX(destroy_fx, transform.position, Vector2.up);
                Destroy(gameObject);
            }
        }
    }
}
