using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class BaseMovement : MonoBehaviour
{
    //Inspector Fields
    public float movement_speed = 4;
    protected bool grid_based_movement = false;
    public bool disabled = false; 
    Rigidbody rb;
    private float disable_timer = 0; 
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    //Grab the next input -> grid based for humanoid etc. etc. 
    protected virtual void Update()
    {   
        if(disabled) { 
            disable_timer += Time.deltaTime; 
            if(disable_timer > 2.5f) { 
                disabled = false;
                disable_timer = 0;
            }
            return; 
        }
        Vector2 current_input = GetInput();
        if (grid_based_movement)
        {
            current_input = AdjustToGridLine(current_input, GetOrientation());
        }
        rb.velocity = current_input * movement_speed;
    }

    public void disable() { 
        disabled = true; 
    }
    public virtual Vector2 GetInput()
    {
        return new Vector2(0, 0);
    }


    /*
    Returns whether or not a player is on a grid line given an orientation
    EG: horizontal movement, check if y position % 0.5 == 0
    */
    void snap_to_y() { 
        transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y * 2.0f)/ 2.0f);
    }
    void snap_to_x() { 
        transform.position = new Vector2(Mathf.Round(transform.position.x * 2.0f)/ 2.0f, transform.position.y);
    }
    bool OnGridLine(string orientation)
    {
        if (orientation == "right" || orientation == "left")
        {
            if (Abs((transform.position.y % 0.5f) - 0.5f) < 0.1 || transform.position.y % 0.5f < 0.1)
            {
                snap_to_y(); 
                return true;
            }
            return false;
        }
        else if (orientation == "up" || orientation == "down")
        {
            if (Abs((transform.position.x % 0.5f) - 0.5f) < 0.1 || transform.position.x % 0.5f < 0.1)
            {
                snap_to_x(); 
                return true;
            }
            return false;
        }
        Debug.Log("ERROR: NOT A VALID ORIENTATION IN OnGridLine(string orientation)");
        return false;
    }

    //Adjusts humanoid movements to grid lines
    //x position of 4.03 is adjusted to 4.0 when moving upwards or downwards
    Vector2 AdjustToGridLine(Vector2 input, string orientation)
    {
        //if already on grid line
        if (OnGridLine(orientation)) return input;
        if (orientation == "right" || orientation == "left")
        {
            float diff = transform.position.y % 0.5f;

            if (diff <= 0.25)
            {

                return new Vector2(0f, -1f);
            }
            else
            {
                return new Vector2(0f, 1f);
            }
        }
        else if (orientation == "up" || orientation == "down")
        {
            float diff = transform.position.x % 0.5f;
            if (diff <= 0.25)
            {
                return new Vector2(-1f, 0);
            }
            else
            {
                return new Vector2(1f, 0);
            }
        }
        return input;
    }

    public virtual string GetOrientation()
    {
        return "none";
    }
}
