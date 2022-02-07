using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballActions : MonoBehaviour
{
    public float fireball_speed = 6;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveFireball(Vector3 travelDirection)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = travelDirection * fireball_speed; 
    }

    void OnTriggerEnter(Collider coll)
    {
        GameObject objectCollidedWith = coll.gameObject;

        if (objectCollidedWith.tag == "Player")
        {
            // Remove health using alterHealth from HasHealth
            HasHealth hasHealth = objectCollidedWith.GetComponent<HasHealth>();
            hasHealth.AlterHP(-1);
            Destroy(gameObject);
        }
        else if (objectCollidedWith.tag == "wall")
        {
            Destroy(gameObject);
        }
    }
}
