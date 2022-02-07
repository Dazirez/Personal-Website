using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public GameObject[] prefabs;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject Spawn(int enemy_type, Vector3 pos)
    {
        //Debug.Log(prefabs[enemy_type]); 
        return Instantiate(prefabs[enemy_type], pos, Quaternion.identity);
    }
}
