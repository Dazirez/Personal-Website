using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public class blade_master_knockback : MonoBehaviour
{
    public float health_change_amount = -0.5f;
    public float knockback_power = 20f;
    public bool destroy_self_on_touch = false;
    public void OnTriggerEnter(Collider other)
    {
        /* Player is not hurt if in God Mode */
        if (other.gameObject.tag == "Link" && GameController.instance.inGodMode()) return;

        HasHealth other_health = other.gameObject.GetComponentInParent<HasHealth>();
        Rigidbody other_rb = other.gameObject.GetComponentInParent<Rigidbody>();

        
        /* Wall Collisions */
        if (other_health == null)
        {
            return;
        }


        /* Perform Knockback */
        if (other_rb != null && !other_health.isInvincible())
        {

            /* Change health */
            other_health.AlterHP(health_change_amount);

            /*TODO: if Object is destroyed return*/

            Vector3 knockback_direction = (other.transform.position - transform.position).normalized;
            other_rb.velocity = Vector2.zero;
            /* Determine Knockback direction -> horizontal vs vertical */
            if (Math.Abs(knockback_direction.x) < Math.Abs(knockback_direction.y))
            {
                if (knockback_direction.x > 0)
                {
                    other_rb.AddForce(new Vector2(knockback_power, 0f), ForceMode.Impulse);
                    // other_rb.velocity = new Vector2(knockback_power, 0f);

                }
                else
                {
                    other_rb.AddForce(new Vector2(-knockback_power, 0f), ForceMode.Impulse);
                    // other_rb.velocity = new Vector2(-knockback_power, 0f);

                }
            }
            else
            {
                if (knockback_direction.y > 0)
                {
                    other_rb.AddForce(new Vector2(0f, knockback_power), ForceMode.Impulse);
                    // other_rb.velocity = new Vector2(0f, knockback_power);

                }
                else
                {
                    other_rb.AddForce(new Vector2(0f, -knockback_power), ForceMode.Impulse);
                    // other_rb.velocity = new Vector2(0f, -knockback_power);

                }
            }

        }


        /* Destroy self */
        if (destroy_self_on_touch)
            Destroy(gameObject);
    }
}