using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycastdetector : MonoBehaviour
{
    LineRenderer debug_line_renderer;
    BaseMovement movement;
    public float raycast_strength = 0.5f; 

    private bool debug = true;
    // Update is called once per frame

    private void Start()
    {
        movement = GetComponent<BaseMovement>();
    }
    void Update()
    {
        DebugVisuals();
    }

    void DebugVisuals()
    {
        if (debug_line_renderer == null)
            debug_line_renderer = gameObject.AddComponent<LineRenderer>();
        if (debug)
        {
            debug_line_renderer.startWidth = 0.1f;
            debug_line_renderer.positionCount = 2;
            debug_line_renderer.startColor = Color.red;
            debug_line_renderer.SetPosition(0, transform.position);
            debug_line_renderer.SetPosition(1, transform.position + transform.up * raycast_strength);

            if (movement.GetOrientation() == "up")
            {
                debug_line_renderer.SetPosition(1, transform.position + transform.up * raycast_strength);
            }
            else if(movement.GetOrientation() == "down") {
                debug_line_renderer.SetPosition(1, transform.position + transform.up * -raycast_strength);

            }
            else if (movement.GetOrientation() == "right")
            {
                debug_line_renderer.SetPosition(1, transform.position + transform.right * raycast_strength);

            }
            else if (movement.GetOrientation() == "left")
            {
                debug_line_renderer.SetPosition(1, transform.position + transform.right * -raycast_strength);

            }


        }
        else
        {
            if (debug_line_renderer != null)
                Destroy(debug_line_renderer);
        }

        // Draw line in scene view.
        Debug.DrawLine(transform.position, transform.position + transform.up * 5.0f, Color.red);
    }
 

    private bool is_wall(RaycastHit hit) { 
        if (hit.collider.gameObject.tag == "wall" || hit.collider.gameObject.tag == "pushable_block" || hit.collider.gameObject.tag == "westdoor" || hit.collider.gameObject.tag == "eastdoor" || hit.collider.gameObject.tag == "southdoor" || hit.collider.gameObject.tag == "northdoor") { 
            return true; 
        }
        return false; 
    }
    public bool wall_in_front() {
        RaycastHit hit;

        // Perform the raycast.
        if (movement.GetOrientation() == "up" && Physics.Raycast(transform.position, transform.up, out hit, raycast_strength))
        {
            // The raycast hit something!
            // Check if it hit a wall.
            if (is_wall(hit))
            {
                // It hit a wall
                return true;
            }
        }
        else if (movement.GetOrientation() == "down" && Physics.Raycast(transform.position, -transform.up, out hit, raycast_strength))
        {
            // The raycast hit something!
            // Check if it hit a player.
            if (is_wall(hit))
            {
                // It hit a wall
                return true;
            }
        }
        else if (movement.GetOrientation() == "right" && Physics.Raycast(transform.position, transform.right, out hit, raycast_strength))
        {
            // The raycast hit something!
            // Check if it hit a wall.
            if (is_wall(hit))
            {
                // It hit a wall
                return true;
            }
        }
        else if (movement.GetOrientation() == "left" && Physics.Raycast(transform.position, -transform.right, out hit, raycast_strength))
        {
            // The raycast hit something!
            // Check if it hit a wall.
            if (is_wall(hit))
            {
                // It hit a wall
                return true; 
            }
        }
        return false; 
    }
    public bool player_in_sight()
    {
        RaycastHit hit;
        //Debug.DrawRay(transform.position, transform.up, Color.red, 5f);
        //Debug.DrawRay(transform.position, transform.right, Color.red, 5f);

        //Physics.Raycast(transform.position, transform.up, out hit);
        Physics.Raycast(transform.position, transform.right, out hit);
        if(hit.collider.gameObject.tag == "Link") {
            return true; 
        }
        Physics.Raycast(transform.position, -transform.right, out hit);
        if (hit.collider.gameObject.tag == "Link")
        {
            return true;
        }
        Physics.Raycast(transform.position, transform.up, out hit);
        if (hit.collider.gameObject.tag == "Link")
        {
            return true;
        }
        Physics.Raycast(transform.position, -transform.up, out hit);
        if (hit.collider.gameObject.tag == "Link")
        {
            return true;
        }
        return false; 
    }
    public bool blade_master_in_front()
    {
        RaycastHit hit;
        //Debug.DrawRay(transform.position, transform.up, Color.red, 5f);
        //Debug.DrawRay(transform.position, transform.right, Color.red, 5f);
        //Physics.Raycast(transform.position, transform.up, out hit);
        if(Physics.Raycast(transform.position, transform.right, out hit, 0.5f)) { 
            if(hit.collider.gameObject.tag == "blademaster") {
                return true; 
            }
        }
        
        if(Physics.Raycast(transform.position, -transform.right, out hit, 0.5f)) {
             if (hit.collider.gameObject.tag == "blademaster")
            {
                return true;
            }
        }
       
        if(Physics.Raycast(transform.position, transform.up, out hit, 0.5f)) { 
            if (hit.collider.gameObject.tag == "blademaster")
            {
                return true;
            }
        }
        
        if(Physics.Raycast(transform.position, -transform.up, out hit, 0.5f)) { 
            if (hit.collider.gameObject.tag == "blademaster")
            {
                return true;
            }
        }
        
        return false; 
    }
}
