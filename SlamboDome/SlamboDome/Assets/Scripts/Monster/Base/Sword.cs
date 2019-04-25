using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Collider2D this_collider = GetComponent<Collider2D>();
        foreach (Collider2D coll in transform.parent.GetComponentsInChildren<Collider2D>()) 
        {
            Physics2D.IgnoreCollision(coll, this_collider, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
