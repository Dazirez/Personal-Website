using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeseMovement : BaseMovement
{
    //directions: up, down, right, left, northeast, southwest, southeast, northwest
    float[] xdirs = new float[8] { 0f, 0f, 1f, -1f, 1f, -1f, 1f, -1f};
    float[] ydirs = new float[8] { 1f, -1f, 0f, 0f, 1f, -1f, -1f, 1f};
    int curr_direction = 0;

    //when num_changes 2 times alter speed
    int num_changes = 0;
    float change_value = 2.0f;
    float progress = 2;
    float initial_time;
    Animator animator;
        
    string[] orientations = new string[8] { "up", "down", "right", "left", "northeast", "southwest", "southeast", "northwest"};

    /*timer range (1, 4) for how long stalfos moves in direction*/
    private float change_direction_timer;
    public float max_speed; 
    private bool slowing_down = false;
    private bool speeding_up = false; 
    private bool resting = false; 
    protected override void Start()
    {
        animator = GetComponent<Animator>(); 
        max_speed = base.movement_speed;  
        base.grid_based_movement = false;
        base.Start();
    }

    protected override void Update()
    {

        // animator.speed = base.movement_speed / 4.0f; 
        progress = (Time.time - initial_time) / change_direction_timer;
        base.Update();
    }

    public override Vector2 GetInput()
    {
        if(slowing_down) {  
            base.movement_speed *= Mathf.Max(1 - progress, 0); 
            animator.speed  = Mathf.Max(1 - progress, 0); 
        }
        else if(resting) { 
            base.movement_speed = 0; 
            animator.speed = 0; 
        }
        else if(speeding_up) { 
            base.movement_speed = max_speed * progress; 
            animator.speed = progress; 
        }
        else { 
            base.movement_speed = max_speed; 
            animator.speed = 1; 
        }
        if (progress >= 1)
        {
            if(slowing_down) { 
                slowing_down = false; 
                resting = true; 
                change_direction_timer = 2.0f;
            }
            else if(resting) { 
                resting = false; 
                speeding_up = true; 
                change_direction_timer = 8.0f;
            }
            else if(speeding_up) { 
                speeding_up = false; 
                change_direction_timer = 2.0f;

            }
            else { 
                num_changes++;
                change_direction_timer = Random.Range(0.1f, 1.0f);

            }
            initial_time = Time.time;
            if (num_changes > 2)
            {
                max_speed += change_value;
                change_value *= -1f;
                num_changes = 0;
            }
            int new_direction = Random.Range(0, 7);
            int slow = Random.Range(0, 50); 
            if (slow == 49)
            {
                slowing_down = true;
            }
            else
            {
                curr_direction = new_direction;
            }
        }

        return new Vector2(xdirs[curr_direction], ydirs[curr_direction]);
    }

    public override string GetOrientation()
    {
        return orientations[curr_direction];
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "pushable_block")
        {
            change_direction_timer = Random.Range(0.1f, 1.0f);
            curr_direction = Random.Range(0, 7);
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "wall" || coll.gameObject.tag == "pushable_block")
        {
            change_direction_timer = Random.Range(0.1f, 1.0f);
            curr_direction = Random.Range(0, 7);
        }
    }
}
