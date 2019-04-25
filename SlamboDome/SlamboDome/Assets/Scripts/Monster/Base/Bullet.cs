using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public GameObject hit_fx;

    public float bullet_speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * bullet_speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FX_Spawner.instance.SpawnFX(hit_fx, collision.contacts[0].point, collision.contacts[0].normal);
        Destroy(gameObject);
    }
}
