using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallmasterMovement : BaseMovement
{
    //directions: up, down, right, left
    float[] xdirs = new float[4] { 0f, 1f, 0f, -1f };
    float[] ydirs = new float[4] { 1f, 0f, -1f, 0f };

    /*
     l u r
     u r d
     r d l
     */
    int curr_direction = -1;
    string[] orientations = new string[4] { "up", "right", "down", "left" };

    private Vector2 target_position;
    private Vector2 start_position;

    private float current, target;
    private float speed = 0.5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        StartCoroutine(transition());
    }

    protected override void Update()
    {
        if (curr_direction != -1)
        {
            StartCoroutine(transition());

        }
    }
    public void set_direction(int i)
    {
        curr_direction = i;
    }

    IEnumerator transition()
    {
        GrabOnTouch gb = GetComponent<GrabOnTouch>();
        Vector2 initial_position = (Vector2)transform.position;
        Vector2 final_position = initial_position + new Vector2(xdirs[curr_direction], ydirs[curr_direction]);
        yield return StartCoroutine(CoroutineUtilities.MoveObjectOverTime(transform, initial_position, final_position, 1.0f));
        initial_position = (Vector2)transform.position;
        final_position = initial_position + new Vector2(4 * xdirs[(curr_direction + 1) % 4], 4 * ydirs[(curr_direction + 1) % 4]);
        yield return StartCoroutine(CoroutineUtilities.MoveObjectOverTime(transform, initial_position, final_position, 2.0f));
        initial_position = (Vector2)transform.position;
        final_position = initial_position + new Vector2(xdirs[(curr_direction + 2) % 4], ydirs[(curr_direction + 2) % 4]);
        yield return StartCoroutine(CoroutineUtilities.MoveObjectOverTime(transform, initial_position, final_position, 1.0f));
        if (gb.grabbed)
        {
            gb.enabled = false;
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = new Vector3(39.5f, 2, 0);
            MonoBehaviour[] comps = player.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in comps)
            {
                c.enabled = true;
            }
            player.gameObject.layer = 6;

            player.GetComponent<SpriteRenderer>().sortingOrder = 4;

            GameObject camera = GameObject.FindWithTag("MainCamera");
            camera.transform.position = new Vector3(39.5f, 7f, -10);

            gb.enabled = true;
        }

    }

}
