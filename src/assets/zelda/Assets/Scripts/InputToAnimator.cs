using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToAnimator : MonoBehaviour
{
    Animator animator; 
    PlayerControls playerControls;
    BaseMovement movement;

    public bool is_player; 
    // Start is called before the first frame update
    void Start()
    {
        if(is_player) playerControls = GetComponent<PlayerControls>();
        animator = GetComponent<Animator> ();
        movement = GetComponent<BaseMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = movement.GetInput().x;
        float y = movement.GetInput().y;

        if (!movement.enabled || (x == 0 && y == 0))
        {
            animator.SetFloat("horizontal_input", 0);
            animator.SetFloat("vertical_input", 0);
            //if(!is_player) Debug.Log("disabling movement animation"); 
            //animator.speed = 0.0f;
        }
        else
        {
            // Animation for movement)
            animator.SetFloat("horizontal_input", x);
            animator.SetFloat("vertical_input", y);

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.0f)
            {
                animator.SetFloat("vertical_input", 0);
            }
            animator.speed = 1.0f;
        }
    }

    public void attackPrimary()
    {
        //Animation for primary weapon
        if (is_player)
        {
            // Find orientation and set booleans based on orientation
            string orientation = movement.GetOrientation();

            // If currently attacking don't want movement animation to occur
            animator.SetFloat("vertical_input", 0);
            animator.SetFloat("horizontal_input", 0);
            animator.speed = 1.0f; // make sure even if not moving still animate

            if (orientation == "down")
            {
                animator.SetTrigger("attack_down_primary");
            }
            else if (orientation == "left")
            {
                animator.SetTrigger("attack_left_primary");
            }
            else if (orientation == "up")
            {
                animator.SetTrigger("attack_up_primary");
            }
            else
            { // orientation == "right"
                animator.SetTrigger("attack_right_primary");
            }
        }
    }

    public void attackBow()
    {
        //Animation for primary weapon
        if (is_player)
        {
            // Find orientation and set booleans based on orientation
            string orientation = movement.GetOrientation();

            // If currently attacking don't want movement animation to occur
            animator.SetFloat("vertical_input", 0);
            animator.SetFloat("horizontal_input", 0);
            animator.speed = 1.0f; // make sure even if not moving still animate

            if (orientation == "down")
            {
                animator.SetTrigger("arrow_down");
            }
            else if (orientation == "left")
            {
                animator.SetTrigger("arrow_left");
            }
            else if (orientation == "up")
            {
                animator.SetTrigger("arrow_up");
            }
            else
            { // orientation == "right"
                animator.SetTrigger("arrow_right");
            }
        }
    }

    public void attackBomb()
    {
        //Animation for primary weapon
        if (is_player)
        {
            // Find orientation and set booleans based on orientation
            string orientation = movement.GetOrientation();

            // If currently attacking don't want movement animation to occur
            animator.SetFloat("vertical_input", 0);
            animator.SetFloat("horizontal_input", 0);
            animator.speed = 1.0f; // make sure even if not moving still animate

            if (orientation == "down")
            {
                animator.SetTrigger("bomb_down");
            }
            else if (orientation == "left")
            {
                animator.SetTrigger("bomb_left");
            }
            else if (orientation == "up")
            {
                animator.SetTrigger("bomb_up");
            }
            else
            { // orientation == "right"
                animator.SetTrigger("bomb_right");
            }
        }
    }

    public void attackBoomerang()
    {
        //Animation for primary weapon
        if (is_player)
        {
            // Find orientation and set booleans based on orientation
            string orientation = movement.GetOrientation();

            // If currently attacking don't want movement animation to occur
            animator.SetFloat("vertical_input", 0);
            animator.SetFloat("horizontal_input", 0);
            animator.speed = 1.0f; // make sure even if not moving still animate

            if (orientation == "down")
            {
                animator.SetTrigger("boomerang_down");
            }
            else if (orientation == "left")
            {
                animator.SetTrigger("boomerang_left");
            }
            else if (orientation == "up")
            {
                animator.SetTrigger("boomerang_up");
            }
            else
            { // orientation == "right"
                animator.SetTrigger("boomerang_right");
            }
        }
    }
}
