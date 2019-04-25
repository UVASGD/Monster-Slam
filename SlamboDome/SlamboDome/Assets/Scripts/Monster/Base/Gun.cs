using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shot_fx;
    float shot_range_min = 2f, shot_range_max = 5f;
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
            b.transform.localScale = transform.localScale;
            b.transform.up = barrel.transform.up;
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        firing = true;
        yield return new WaitForSeconds(Random.Range(shot_range_min, shot_range_max));
        firing = false;
    }
}
