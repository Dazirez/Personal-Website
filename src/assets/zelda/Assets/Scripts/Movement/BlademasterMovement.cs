using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class BlademasterMovement : BaseMovement
{
    private Raycastdetector rc;
    //if blademaster is in starting location it is ready to launch towards a player
    private bool returning = true;
    private bool attacking = false; 
    Vector2 starting_pos;
    Vector2 curr; 
    float return_speed;
    float attack_speed;
    // Start is called before the first frame update
    protected override void Start()
    {
        attack_speed = movement_speed;      
        return_speed = movement_speed / 4.0f; 
        starting_pos = transform.position;
        grid_based_movement = false; 
        rc = GetComponent<Raycastdetector>();
        base.Start(); 
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); 
    }
    public override string GetOrientation() { 
        if(Abs(curr.y - 1.0f) < 0.1) { 
            return "up";
        }
        else if(Abs(curr.y - (-1.0f)) < 0.1) { 
            return "down"; 
        }
        else if(Abs(curr.x - 1.0f) < 0.1) { 
            return "right"; 
        }
        else if(Abs(curr.x - (-1.0f)) < 0.1) {
            return "left"; 
        }
        return "none"; 
    }
    
    public override Vector2 GetInput()
    {
        // Debug.Log(rc.player_in_sight() + " " + returning + " " + attacking + " " + curr); 
        Debug.Log("walll detected: " + rc.wall_in_front()); 

        if(rc.blade_master_in_front() || rc.wall_in_front()) { 
            Debug.Log("blademaster or wall detected"); 
            returning = true; 
            attacking = false; 
        }
        else if(rc.player_in_sight() && !returning && curr == Vector2.zero) { 
            GameObject player = GameObject.Find("Player");
            curr = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
            if (Abs(curr.x) > Abs(curr.y))
            {
                curr.y = 0;
            }
            else
            {
                curr.x = 0;
            }
            base.movement_speed = attack_speed; 
            attacking = true; 
        }
        if (attacking)
        {
            return curr;
        }
        else if (returning)
        {

            base.movement_speed = return_speed;

            returning = true;
            float dist = Vector2.Distance(starting_pos, (Vector2)transform.position);
            if (dist < 0.1)
            {
                returning = false;
                curr = Vector2.zero; 
            }
            return -curr;
        }
        return curr;
    }
    void onTriggerEnter(Collider other) { 
        if(other.gameObject.tag == "wall") { 
            returning = true; 
            attacking = false; 
        }
    }
}
