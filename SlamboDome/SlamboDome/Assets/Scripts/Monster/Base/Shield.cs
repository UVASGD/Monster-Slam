﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float health = 100f, armor = 15f;
    float current_health;

    public GameObject destroy_fx, hit_fx;

    SpriteRenderer s;

    Vector2 offset;
    private void Awake()
    {
        offset = transform.position - transform.parent.position;
        Collider2D this_collider = GetComponent<Collider2D>();
        foreach (Collider2D coll in transform.parent.GetComponentsInChildren<Collider2D>())
        {
            Physics2D.IgnoreCollision(coll, this_collider, true);
        }
    }

    private void LateUpdate()
    {
        transform.position = (Vector2)transform.parent.transform.position + offset;
    }

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
