using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
    //directions: up, down, right, left
    float[] xdirs = new float[4] { 0f, 0f, 1f, -1f };
    float[] ydirs = new float[4] { 1f, -1f, 0f, 0f };
    int curr_direction = 1;
    public bool cantmove = false; 
    string[] orientations = new string[4] { "up", "down", "right", "left" };

    protected override void Start()
    {
        base.grid_based_movement = true;
        base.Start();
    }

    public override Vector2 GetInput()
    {
        if(cantmove) return Vector2.zero; 
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horizontal_input) > 0.0f)
        {
            vertical_input = 0.0f;
        }
        //set the current direction
        for(int i = 0; i < 4; i++) {
            if(xdirs[i] == horizontal_input && ydirs[i] == vertical_input) {
                curr_direction = i;
                break; 
            }
        }
        return new Vector2(horizontal_input, vertical_input);
    }

    public override string GetOrientation()
    {
        return orientations[curr_direction];
    }

}
