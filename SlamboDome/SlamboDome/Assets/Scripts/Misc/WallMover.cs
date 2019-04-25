using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMover : MonoBehaviour
{
    public Vector2 target;
    Vector2 velocity;
    float smoothtime = 1, max_speed = 0.1f, sound_threshold = 10f;
    // Start is called before the first frame update
    void Start()
    {
        target *= transform.parent.localScale.x;
        target = (Vector2)transform.position + target;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, target, ref velocity, smoothtime, max_speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impact = collision.contacts[0].normalImpulse;

        if (impact > sound_threshold)
        {
            FX_Spawner.instance.SpawnFX(FXType.Wall, collision.contacts[0].point, Vector2.up);
        }
    }
}
