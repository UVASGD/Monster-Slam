using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shot_fx;
    public float shot_interval = 1f;
    bool firing = false;

    GameObject barrel;

    // Start is called before the first frame update
    void Start()
    {
        barrel = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!firing)
        {
            GameObject b = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
            b.transform.up = barrel.transform.up;
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        firing = true;
        yield return new WaitForSeconds(shot_interval);
        firing = false;
    }
}
