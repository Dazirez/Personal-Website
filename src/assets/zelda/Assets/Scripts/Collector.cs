using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    Inventory inventory;
    HasHealth health;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        health = GetComponent<HasHealth>();
        if (inventory == null)
        {
            Debug.LogWarning("WARNING: Gameobject with a collector has no inventory to store things in!");
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        GameObject object_collided_with = coll.gameObject;

        // Determines what object was collected
        if (object_collided_with.tag == "rupee")
        {
            if (inventory != null)
            {
                inventory.AddRupees(1);
            }
            Destroy(object_collided_with);

            //play ruppe collection sound effect
            AudioController.instance.play_rupee_clip();
        }
        else if (object_collided_with.tag == "heart")
        {
            if (health != null)
            {
                health.AlterHP(1);
            }
            Destroy(object_collided_with);
            AudioController.instance.play_heart_clip();

        }
        else if (object_collided_with.tag == "key")
        {
            if (inventory != null)
            {
                inventory.AddKeys(1);
            }
            Destroy(object_collided_with);

            // Play key collection sound
            AudioController.instance.play_key_clip();
        }
        else if (object_collided_with.tag == "collectable_bomb")
        {
            AudioController.instance.play_rupee_clip();
            if (inventory != null)
            {
                inventory.AddBombs(1);
            }

            // Play bomb collection sound

            Destroy(object_collided_with);
        }
        else if (object_collided_with.tag == "bow")
        {
            // Add object to altWeapons List
            PlayerControls playerControls = GetComponent<PlayerControls>();
            playerControls.AddAlternateWeapon("bow", false);

            // Play bow collection sound
            AudioController.instance.play_obtain_clip();

            Destroy(object_collided_with);
        }
        else if (object_collided_with.tag == "collectable_boomerang")
        {
            // Add object to altWeapons List
            PlayerControls playerControls = GetComponent<PlayerControls>();
            playerControls.AddAlternateWeapon("boomerang", false);

            // Play boomerang collection sound
            AudioController.instance.play_obtain_clip();

            Destroy(object_collided_with);
        }
        else if (object_collided_with.tag == "BigHeart") { 
            AudioController.instance.play_obtain_clip();
            if (health != null)
            {
                health.AlterHP(10);
            }
            Destroy(object_collided_with);
        }

    }
}
