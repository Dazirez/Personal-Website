using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject westDoorPrefab;
    public float timeToPush = 0.2f;

    public bool beforeOldBlockPushed; // if block has been pushed or not
    public bool beforeBowBlockPushed;

    GameObject roomBeforeOld; // Needed to figure out num enemies defeated
    LevelController roomBeforeOldLC;
    bool startTimer; // Keep track of how much time has passed since link started pushing block
    string orientationWhilePushing;
    PlayerMovement movement; // to get orientation
    HasHealth hasHealth;
    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        startTimer = false;
        timeLeft = timeToPush;
        movement = GetComponent<PlayerMovement>();
        hasHealth = GetComponent<HasHealth>();
        beforeOldBlockPushed = false;
        beforeBowBlockPushed = false;
        roomBeforeOld = GameObject.FindGameObjectWithTag("room_before_old");
        roomBeforeOldLC = roomBeforeOld.GetComponent<LevelController>();
    }

    private void Update()
    {
        if (startTimer)
        {
            timeLeft -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject object_collided_with = collision.gameObject;
        if (object_collided_with.tag == "pushable_block" && !hasHealth.GetIsStunned())
        {
            // Tracking which block it is based on player position
            // Room with block before bow room
            if ((transform.position.y <= 41 && transform.position.y >= 35) && (transform.position.x <= 28 && transform.position.x >= 18)) {
                // Make sure player is pushing block before bow head on (from any direction), .y check is from right/left, .x check is from up/down
                if ((transform.position.y <= 38.1 && transform.position.y >= 37.9) || (transform.position.x <= 23.1 && transform.position.x >= 22.9))
                {
                    if (beforeOldBlockPushed == false && roomBeforeOldLC.remainingEnemies == 0) // If block hasn't been pushed and enemis defeated
                    {
                        startTimer = true;
                        orientationWhilePushing = movement.GetOrientation();
                        beforeOldBlockPushed = true;
                    }
                }
            }
            else // (transform.position.y <= 63 && transform.position.y >= 57) && (transform.position.x <= 28 && transform.position.x > 18)
            {
                // Player is in room with stairs
                // Make sure player is pushing block before stairs head on, .y check is from right, .x check is from up or down
                if ((transform.position.y <= 60.1 && transform.position.y >= 59.9) || (transform.position.x <= 22.1 && transform.position.x >= 21.9))
                {
                    if (beforeBowBlockPushed == false)
                    {
                        startTimer = true;
                        orientationWhilePushing = movement.GetOrientation();
                        beforeBowBlockPushed = true;
                    }
                }
            }
        }
    }

    // Make sure player is still pushing block
    private void OnCollisionStay(Collision collision)
    {
        GameObject object_collided_with = collision.gameObject;
        if (object_collided_with.tag == "pushable_block")
        {
            // If countdown has started to make sure player is pushing block
            if (startTimer == true)
            {
                float horizontal_input = Input.GetAxisRaw("Horizontal");
                float vertical_input = Input.GetAxisRaw("Vertical");
                if (Mathf.Abs(horizontal_input) > 0.0f)
                {
                    vertical_input = 0.0f;
                }
                // If the player stopped pushing (must have stopped pushing if orientation changed)
                if (movement.GetOrientation() != orientationWhilePushing || (horizontal_input == 0 && vertical_input == 0))
                {
                    // player stopped pushing but still in contact so just reset timer
                    timeLeft = timeToPush;
                }

                // if player has been pushing block for long enough, block can now move
                if (timeLeft <= 0.0f)
                {
                    Debug.Log("we should go in here");
                    // Reset timer variables
                    startTimer = false;
                    timeLeft = timeToPush;

                    if (beforeOldBlockPushed)
                    {
                        AudioController.instance.play_secret();

                        AudioController.instance.play_door_open();
                    }
                    if(beforeBowBlockPushed) { 
                         AudioController.instance.play_secret();

                    }
                    
                    // Now move the block
                    StartCoroutine(BlockMove(object_collided_with, movement.GetOrientation()));
                }
            }
        }
    }

    // If player stops pushing before time is up, reset variables
    private void OnCollisionExit(Collision collision)
    {
        GameObject object_collided_with = collision.gameObject;
        if (object_collided_with.tag == "pushable_block")
        {
            if (startTimer == true && timeLeft >= 0)
            {
                // Reset timer and variables determining if block has been pusehd
                startTimer = false;
                timeLeft = timeToPush;
                if (transform.position.y >= 35 && transform.position.y <= 41) // room where beforebowroom block is located
                {
                    beforeOldBlockPushed = false;
                }
                else if (transform.position.y >= 57 && transform.position.y <= 63) // room where before stair block is located
                {
                    beforeBowBlockPushed = false;
                }
            }
        }
    }


    // Function to make block move by itself
    IEnumerator BlockMove(GameObject pushableBlock, string orientation)
    {
        Debug.Log("Now Moving block");
        // Figure out final position of block depending on orientation
        Vector3 finalPos;
        if (orientation == "down")
        {
            finalPos = pushableBlock.transform.position + new Vector3(0, -1, 0);
        }
        else if (orientation == "up")
        {
            finalPos = pushableBlock.transform.position + new Vector3(0, 1, 0);
        }
        else if (orientation == "left")
        {
            finalPos = pushableBlock.transform.position + new Vector3(-1, 0, 0);
        }
        else // orientation == "right"
        {
            finalPos = pushableBlock.transform.position + new Vector3(1, 0, 0);
        }

        // Spawn a tile in place of the block
        Vector3 initialPos = pushableBlock.transform.position;
        Instantiate(tilePrefab, initialPos, pushableBlock.transform.rotation);

        // now actually move block
        yield return StartCoroutine(CoroutineUtilities.MoveObjectOverTime(pushableBlock.transform, initialPos, finalPos, 1.0f));

        // Then spawn unlocked door
        if (beforeOldBlockPushed)
        {
            GameObject newUnlockedDoor = Instantiate(westDoorPrefab, new Vector3(17, 38, 0), new Quaternion(0, 0, 0, 0));
            newUnlockedDoor.tag = "special_unlocked_westdoor";

            // Delete special_locked_door
            GameObject lockedDoor = GameObject.FindGameObjectWithTag("special_locked_westdoor");
            Destroy(lockedDoor);
        }
    }

}
