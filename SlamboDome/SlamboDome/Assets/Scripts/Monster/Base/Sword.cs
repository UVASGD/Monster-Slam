using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    Vector2 offset;
    // Start is called before the first frame update
    void Awake()
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
        transform.position = (Vector2)transform.parent.position + offset;
    }
}
