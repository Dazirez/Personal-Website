using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaMovement : BaseMovement
{
    //directions: up, down, right, left
    float[] xdirs = new float[4] { 0f, 0f, 1f, -1f };
    float[] ydirs = new float[4] { 1f, -1f, 0f, 0f };
    int curr_direction = 0;
    string[] orientations = new string[4] { "up", "down", "right", "left" };

    /*timer range (1, 4) for how long Goriya moves in direction*/
    private float change_direction_timer;
    private Raycastdetector rc;

    protected override void Start()
    {
        rc = GetComponent<Raycastdetector>();
        base.grid_based_movement = true;
        base.Start();
    }

    protected override void Update()
    {
        change_direction_timer -= Time.deltaTime;
        base.Update();
    }

    public override Vector2 GetInput()
    {
        if (rc.wall_in_front() || change_direction_timer < 0)
        {
            change_direction_timer = Random.Range(1.0f, 2.5f);
            curr_direction = Random.Range(0, 4);
        }
        return new Vector2(xdirs[curr_direction], ydirs[curr_direction]);
    }

    public override string GetOrientation()
    {
        return orientations[curr_direction];
    }
}
