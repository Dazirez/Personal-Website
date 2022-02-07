using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class GelMovement : BaseMovement
{
    //directions: up, down, right, left
    float[] xdirs = new float[4] { 0f, 0f, 1f, -1f };
    float[] ydirs = new float[4] { 1f, -1f, 0f, 0f };
    int curr_direction = 0;
    string[] orientations = new string[4] { "up", "down", "right", "left" };

    private Vector3 desired_position;
    private Raycastdetector rc; 
    public float rest_timer; 
    protected override void Start()
    {
        rc = GetComponent<Raycastdetector>(); 
        desired_position = transform.position; 
        base.grid_based_movement = true;
        rest_timer = 1f; 
        base.Start();
    }

    protected override void Update()
    {
        rest_timer -= Time.deltaTime; 
        base.Update();
    }

    public override Vector2 GetInput()
    {
        if (rest_timer > 0f)
        {
            return Vector2.zero;
        }
        Vector3 curr = transform.position;

        if (rc.wall_in_front() || Abs(curr.x - desired_position.x) < 0.05 && Abs(curr.y - desired_position.y) < 0.05)
        {
            rest_timer = Random.Range(0.1f, 1.0f); 
            curr_direction = Random.Range(0, 4);
            desired_position = new Vector3(transform.position.x + xdirs[curr_direction], transform.position.y + ydirs[curr_direction], 0); 
        }
        return new Vector2(xdirs[curr_direction], ydirs[curr_direction]);
    }

    public override string GetOrientation()
    {
        return orientations[curr_direction];
    }
}
