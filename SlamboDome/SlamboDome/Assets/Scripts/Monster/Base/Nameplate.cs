using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nameplate : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
        transform.position = (Vector2)transform.parent.transform.position + (Vector2.up);
    }
}
