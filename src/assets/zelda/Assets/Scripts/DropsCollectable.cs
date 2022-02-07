using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsCollectable : MonoBehaviour
{
    public GameObject[] drops;
    public float drop_rate;

    public void drop_collectable()
    {
        float rnd = Random.Range(0, 101);
        if (rnd < drop_rate)
        {
            int item = Random.Range(0, drops.Length);
            Instantiate(drops[item], transform.position, Quaternion.identity);
        }
    }
}
