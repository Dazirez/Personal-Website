using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWallMaster : MonoBehaviour
{
    public int spawn_direction = 0;
    float[] xdirs = new float[4] { -4f, -1f, 4f, 1f };
    float[] ydirs = new float[4] { -1f, 4f, 1f, -4f };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("spwanign wallmasater");
            Transform block_location = transform;
            GetComponentInParent<WallMasterController>().spawn_wallmaster(get_spawn_location(transform.position), spawn_direction);
        }
    }

    private Vector3 get_spawn_location(Vector3 block_location)
    {
        Vector3 updated_location = block_location;
        updated_location.x += xdirs[spawn_direction];
        updated_location.y += ydirs[spawn_direction];
        return updated_location;
    }
}