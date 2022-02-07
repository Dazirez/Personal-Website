using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamentusMovement : BaseMovement
{
    float[] xdirs = new float[2] { 1f, -1f };
    private float change_direction_timer;
    private int curr_direction = 0;
    string[] orientations = new string[2] { "right", "left" };
    public bool dungeon = true; 
    public float lower_bound = 72f; 
    public float upper_bound = 76f; 
    protected override void Start()
    {
        if(dungeon) { 
            transform.position = new Vector2(75, 49.5f);
        }
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        change_direction_timer -= Time.deltaTime;
        base.Update(); 
    }

    private bool out_of_range() {
        if(transform.position.x < lower_bound || transform.position.x > upper_bound)
        {
            Debug.Log("out of range"); 
            return true; 
        }
        return false; 
    }
    public override Vector2 GetInput()
    {
        if(change_direction_timer < 0 || out_of_range()) {
            change_direction_timer = Random.Range(1.0f, 4.0f);
            curr_direction = (curr_direction + 1) % 2; 
        }
        return new Vector2(xdirs[curr_direction], 0); 
    }

    public override string GetOrientation()
    {
        return orientations[curr_direction];
    }

}
