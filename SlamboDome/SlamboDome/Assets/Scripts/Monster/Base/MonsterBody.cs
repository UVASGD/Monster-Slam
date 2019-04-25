using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public delegate void DeathDel(MonsterBody body);

[RequireComponent(typeof(CostCalculator))]
[RequireComponent(typeof(Rigidbody2D))]
public class MonsterBody : MonoBehaviour
{
    public float health, armor, move_speed;
    float dash_speed = 20f, 
        turn_speed = 20f, 
        dash_time = 3f, 
        stun_time = 3f, 
        stun_threshold = 30f, 
        knockback = 25f;
    bool dashing = false, stunned = false;

    [HideInInspector] public string monster_name = "Dummy";

    Rigidbody2D rb;

    GameObject target;
    List<GameObject> detected_monsters = new List<GameObject>();

    public event DeathDel DeathEvent;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GetComponentInChildren<Head>())
            GetComponentInChildren<Head>().DamageEvent += Damage;
        if (GetComponentInChildren<TextMeshPro>())
            monster_name = GetComponentInChildren<TextMeshPro>().text;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        float damage = collision.contacts[0].normalImpulse;
        Damager d = collision.collider.GetComponent<Damager>();
        if (d)
        {
            damage += d.GetDamage();
        }

        if (damage > armor)
        {
            Damage(damage, collision.contacts[0].normal);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<MonsterBody>())
            detected_monsters.Add(other.gameObject);
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<MonsterBody>())
            detected_monsters.Remove(other.gameObject);
    }

    protected void Damage(float damage, Vector2 dir)
    {
        if (damage > stun_threshold)
            Stun();

        rb.AddForce(-dir * knockback, ForceMode2D.Impulse);
        FX_Spawner.instance.SpawnFX(FXType.Hit, transform.position, dir);

        health -= damage;
        if (health < 0)
            Die();
    }

    protected void Stun()
    {
        if (!stunned)
            StartCoroutine(StunCo());
    }
    IEnumerator StunCo()
    {
        stunned = false;
        yield return new WaitForSeconds(stun_time);
        stunned = true;
    }

    protected void Die()
    {
        DeathEvent?.Invoke(this);
        FX_Spawner.instance.SpawnFX(FXType.Death, transform.position, Vector2.up);
        Destroy(gameObject, 0.5f);
    }

    protected void Move(Vector2 dir)
    {
        rb.AddForce(dir * move_speed);
    }

    protected void MoveToTarget()
    {
        if (target)
        {
            Move((target.transform.position - transform.position).normalized);
            TurnToTarget();
        }
    }

    protected void Turn(float dir)
    {
        rb.AddTorque(dir * turn_speed);
    }

    protected void TurnToTarget()
    {
        if (target)
            Turn(Mathf.Sign(Vector2.SignedAngle(transform.up, (target.transform.position - transform.position).normalized)));
    }

    protected void Dash(Vector2 dir)
    {
        if (!dashing)
        {
            rb.AddForce(dir * dash_speed, ForceMode2D.Impulse);
            StartCoroutine(DashCo());
        }
    }
    IEnumerator DashCo()
    {
        dashing = true;
        yield return new WaitForSeconds(dash_time);
        dashing = false;
    }
}
