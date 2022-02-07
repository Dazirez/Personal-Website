using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangActions : MonoBehaviour
{
    public static float weapon_speed = 0.2f;
    Rigidbody rb;
    public bool playerBoomerang;
    private float timer;
    bool canAttackAgain;

    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Coroutine to have boomerang rotate at all times
        StartCoroutine(RotateBoomerang());

        initialPosition = transform.position;
        Debug.Log("INITIAL POSITION");
        Debug.Log(initialPosition);
        canAttackAgain = true;
    }

    void Update() { 
        timer += Time.deltaTime; 
        if(playerBoomerang && timer > 0.1f) { 
            timer = 0; 
            AudioController.instance.play_boomerang(); 
        }

        if (transform.position.x < (initialPosition.x - (initialPosition.x % 16f) + 1f) || transform.position.x > (initialPosition.x - (initialPosition.x % 16f) + 15f) ||
            transform.position.y < (initialPosition.y - (initialPosition.y % 11f) + 1f) || transform.position.y > (initialPosition.y - (initialPosition.y % 11f) + 10f))
        {
            Debug.Log(((initialPosition.x % 16f) + 1f));
            Debug.Log(((initialPosition.x % 16f) + 15f));
            Debug.Log(((initialPosition.y % 11f) + 1f));
            Debug.Log(((initialPosition.y % 11f) + 10f));
            Destroy(gameObject);
        }
    }


    public IEnumerator MoveBoomerangOverTime(GameObject target, string orientation, Vector3 initialPos)
    {
        // Boomerang is oriented in middle of tile so don't need to worry about +1, or -1 in distance dependign on orientation
        // Find orientation to determine direction object should be moving
        // To determine final pos, decide which is closer to player: move 4 blocks or hit wall
        Vector3 travelVector;
        Vector3 finalPos; // depends on direction, position that represents wall
        Vector3 longFinalPos;
        float remainder; // Rooms are 16 x 11
        float destCoord;
        if (orientation == "down")
        {
            travelVector = new Vector3(0, -1, 0);
            remainder = initialPos.y % 11f;
            destCoord = Mathf.Max(initialPos.y - remainder + 1f, initialPos.y - 5); // Max, since farther down means smaller number
            finalPos = new Vector3(initialPos.x, destCoord, initialPos.z);
            longFinalPos = new Vector3(initialPos.x, destCoord - 1f, initialPos.z);
        }
        else if (orientation == "up")
        {
            travelVector = new Vector3(0, 1, 0);
            remainder = initialPos.y % 11f;
            destCoord = Mathf.Min(initialPos.y - remainder + 9f, initialPos.y + 5);
            finalPos = new Vector3(initialPos.x, destCoord, initialPos.z);
            longFinalPos = new Vector3(initialPos.x, destCoord + 1f, initialPos.z);
        }
        else if (orientation == "left")
        {
            travelVector = new Vector3(-1, 0, 0);
            remainder = initialPos.x % 16f;
            destCoord = Mathf.Max(initialPos.x - remainder + 1f, initialPos.x - 5); // max since farther left means smaller number
            finalPos = new Vector3(destCoord, initialPos.y, initialPos.z);
            longFinalPos = new Vector3(destCoord - 1f, initialPos.y, initialPos.z);
        }
        else
        { // orientation == "right"
            travelVector = new Vector3(1, 0, 0);
            remainder = initialPos.x % 16f;
            destCoord = Mathf.Min(initialPos.x - remainder + 14f, initialPos.x + 5);
            finalPos = new Vector3(destCoord, initialPos.y, initialPos.z);
            longFinalPos = new Vector3(destCoord + 1f, initialPos.y, initialPos.z);
        }

        if (target.tag != "Player")
        {
            GameObject anyGoriya = GameObject.FindGameObjectWithTag("goriya");
            // Calculates distance between wall and weapon
            while (anyGoriya != null && gameObject != null && Vector3.Distance(gameObject.transform.position, finalPos) > 0.5f && Vector3.Distance(gameObject.transform.position, finalPos) > 1.5f)
            {
                gameObject.transform.position = gameObject.transform.position + (travelVector * weapon_speed);

                // yield until the end of the frame, allowing other code / coroutines to run
                // and allowing time to pass.
                yield return null;
                anyGoriya = GameObject.FindGameObjectWithTag("goriya");
            }
        }
        else
        {
            // Calculates distance between wall and weapon
            while (target != null && gameObject != null && Vector3.Distance(gameObject.transform.position, finalPos) > 0.5f && Vector3.Distance(gameObject.transform.position, finalPos) > 1.0f)
            {
                gameObject.transform.position = gameObject.transform.position + (travelVector * weapon_speed);

                // yield until the end of the frame, allowing other code / coroutines to run
                // and allowing time to pass.
                yield return null;
            }
        }

        if (target.tag != "Player")
        {
            if (GameObject.FindGameObjectWithTag("goriya") == null)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (target == null)
            {
                Destroy(gameObject);
            }
        }

        if (target.tag != "Player")
        {
            GameObject anyGoriya = GameObject.FindGameObjectWithTag("goriya");
            Debug.Log("STUPID BOOMERANG YOU SHOULD GO INHERE HOLLEEE PLEASE");
            // Now bring the boomerang back to the player
            while (anyGoriya != null && gameObject != null && Vector3.Distance(transform.position, target.transform.position) > 0.2f)
            {
                Vector3 direction = (target.transform.position - transform.position);
                direction = (new Vector3(direction.x, direction.y, 0)).normalized;
                //Debug.Log("direction");
                //Debug.Log(direction);
                rb = GetComponent<Rigidbody>();
                rb.velocity = direction * 4.5f;

                anyGoriya = GameObject.FindGameObjectWithTag("goriya");

                // yield until the end of the frame, allowing other code / coroutines to run
                // and allowing time to pass.
                yield return null;
            }
            Debug.Log(GameObject.FindGameObjectWithTag("goriya"));
        }
        else
        {
            // Now bring the boomerang back to the player
            while (target != null && gameObject != null && Vector3.Distance(transform.position, target.transform.position) > 0.2f)
            {
                Vector3 direction = (target.transform.position - transform.position);
                direction = (new Vector3(direction.x, direction.y, 0)).normalized;
                //Debug.Log("direction");
                //Debug.Log(direction);
                rb = GetComponent<Rigidbody>();
                rb.velocity = direction * 6.5f;

                // yield until the end of the frame, allowing other code / coroutines to run
                // and allowing time to pass.
                yield return null;
            }
        }


        if (target.tag != "Player")
        {

            if (GameObject.FindGameObjectWithTag("goriya") == null)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (target == null)
            {
                Destroy(gameObject);
            }
        }

        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    // Rotate Boomerang
    IEnumerator RotateBoomerang()
    {
        while (gameObject != null)
        {
            gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 30);
            yield return new WaitForSeconds(0.005f);
        }
    }

    // Determine when weapon has hit a wall
    void OnTriggerEnter(Collider coll)
    {
        GameObject objectCollidedWith = coll.gameObject;

        if (canAttackAgain)
        {
            StartCoroutine(StallOnTrigger());
            if (playerBoomerang)
            {
                // If object is enemy it should stop (If has HasHealth, must be an enemy)
                if (objectCollidedWith.GetComponent<HasHealth>() != null && (objectCollidedWith.tag == "gel" || objectCollidedWith.tag == "keese"))
                {
                    // Remove health using alterHealth from HasHealth
                    Debug.Log("ALTERING");
                    HasHealth hasHealth = objectCollidedWith.GetComponent<HasHealth>();
                    hasHealth.AlterHP(-1.0f);
                }
                else if (objectCollidedWith.tag != "Player")
                {
                    BaseMovement move = objectCollidedWith.GetComponent<BaseMovement>();
                    if (move != null)
                    {
                        move.disable();
                    }
                }
            }
            else // enemy boomerang 
            {
                // If object is enemy it should stop (If has HasHealth, must be an enemy)
                if (objectCollidedWith.tag == "Player")
                {
                    // Remove health using alterHealth from HasHealth
                    HasHealth hasHealth = objectCollidedWith.GetComponent<HasHealth>();
                    hasHealth.AlterHP(-0.5f);
                }
            }
        }
    
    }

    IEnumerator StallOnTrigger()
    {
        canAttackAgain = false;
        yield return new WaitForSeconds(0.1f);
        canAttackAgain = true;
    }
}
