using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn_after_time : MonoBehaviour
{
    public float despawn_timer = 10f;
    public bool despawns = true;
    // Update is called once per frame
    void Update()
    {
        if (despawns)
        {
            despawn_timer -= Time.deltaTime;
            if (despawn_timer <= 0) Destroy(gameObject);
        }
    }
}
