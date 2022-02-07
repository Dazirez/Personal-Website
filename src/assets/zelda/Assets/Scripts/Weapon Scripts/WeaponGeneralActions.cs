using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGeneralActions : MonoBehaviour
{
    float weapon_speed = 0.2f;
    bool alteredHP = false;

    // Rigidbody of weapon
    Rigidbody rb;
    PlayerControls playerControls;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveWeaponOverTime(GameObject weapon, string orientation, Vector3 initialPos) {

        // Find orientation to determine direction object should be moving
        Vector3 travelVector;
        Vector3 finalPos; // depends on direction, position that represents wall
        float remainder; // Rooms are 16 x 11
        float destCoord;
        Vector3 longFinalPos;
        if (orientation == "down") {
            travelVector = new Vector3(0, -1f, 0);
            remainder = initialPos.y % 11f;
            finalPos = new Vector3(initialPos.x, initialPos.y - remainder + 1f, initialPos.z);
            longFinalPos = new Vector3(initialPos.x, initialPos.y - remainder, initialPos.z);
        }
        else if (orientation == "up") {
            travelVector = new Vector3(0, 1f, 0);
            remainder = initialPos.y % 11f;
            destCoord = initialPos.y - remainder + 9f; 
            finalPos = new Vector3(initialPos.x, destCoord, initialPos.z);
            longFinalPos = new Vector3(initialPos.x, destCoord + 1f, initialPos.z);
        }
        else if (orientation == "left") { 
            travelVector = new Vector3(-1f, 0, 0);
            remainder = initialPos.x % 16f;
            finalPos = new Vector3(initialPos.x - remainder + 1f, initialPos.y, initialPos.z);
            longFinalPos = new Vector3(initialPos.x - remainder, initialPos.y, initialPos.z);
        }
        else { // orientation == "right"
            travelVector = new Vector3(1f, 0, 0);
            remainder = initialPos.x % 16f;
            destCoord = initialPos.x - remainder + 14f; // 13, because when going right, sword is extra one tile length
            finalPos = new Vector3(destCoord, initialPos.y, initialPos.z);
            longFinalPos = new Vector3(destCoord + 1f, initialPos.y, initialPos.z);
        }
        
        // Calculates distance between wall and weapon
        while(weapon != null && Vector3.Distance(weapon.transform.position, finalPos) > 0.5f && Vector3.Distance(weapon.transform.position, longFinalPos) > 1.0f) {
            weapon.transform.position = weapon.transform.position + (travelVector * weapon_speed);

            // yield until the end of the frame, allowing other code / coroutines to run
            // and allowing time to pass.
            yield return null;
        }
        if (weapon != null) {
            if (weapon.tag == "sword")
            {
                Vector3 weaponPos = weapon.transform.position;
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwordExplosion>().swordExplosionAction(weaponPos);
            }
            Destroy(weapon);
        }
    }

    // Determine when weapon has hit a wall
    void OnTriggerEnter(Collider coll) {
        GameObject objectCollidedWith = coll.gameObject;

        // If object is enemy it should stop (If has HasHealth, must be an enemy)
        if (objectCollidedWith.GetComponent<HasHealth>() != null && objectCollidedWith.tag != "Player") {
            // Remove health using alterHealth from HasHealth
            HasHealth hasHealth = objectCollidedWith.GetComponent<HasHealth>();

            if (gameObject.tag == "arrow")
            {
                if (alteredHP == false)
                {
                    hasHealth.AlterHP(-2f);
                    alteredHP = true;
                }
            }

            // Destroy weapon
            if (gameObject != null) {
                if (gameObject.tag == "sword" && !playerControls.GetNoProjectile())
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<SwordExplosion>().swordExplosionAction(transform.position);
                }
                Destroy(gameObject);
            }
        }
    }
}
