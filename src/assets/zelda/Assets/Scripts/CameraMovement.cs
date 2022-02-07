using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    // For closing door
    public GameObject lockedWestDoorPrefab;
    public Sprite specialDoorImage;

    // Access player components
    GameObject player;
    DetectDoors detectDoors;
    PlayerMovement movement;
    Rigidbody rb;
    PlayerControls playerControls;
    BoxCollider boxCollider;

    // For displaying oldManRoom Text at the correct time
    GameObject oldManText; // Diplays the text in the old man room
    RectTransform oldManRectTransform; // Controls the position of the text in the old man room
    bool oldManTextAppeared = false;

    public float durationOfPause = 0.75f;
    public float durationOfRoomSwitch = 4.0f;
    float durationOfPlayerEnter = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        // For displaying old man room text
        oldManText = GameObject.FindGameObjectWithTag("old_man_text");
        oldManRectTransform = oldManText.GetComponent<RectTransform>();

        // Need detectdoors to access door hit variable
        player = GameObject.FindWithTag("Player");
        detectDoors = player.GetComponent<DetectDoors>();
        movement = player.GetComponent<PlayerMovement>();
        rb = player.GetComponent<Rigidbody>();
        boxCollider = player.GetComponent<BoxCollider>();
        playerControls = player.GetComponent<PlayerControls>();
        StartCoroutine(WaitForPlayerInputToTransition());
    }

    IEnumerator WaitForPlayerInputToTransition()
    {
        while(true)
        {
            if(detectDoors.GetDoorHit()) // If door has been hit
            {
                // Player can no longer move 
                movement.enabled = false;
                player.GetComponent<Rigidbody>().velocity = Vector2.zero;
                // Player box collider disabled
                boxCollider.enabled = false;
                // Player can no longer attack
                playerControls.SetNoPlayerActions(true);
                // Player's box collider disabled

                //If old man text exists remove it since player must be leaving old man room
                if (oldManTextAppeared)
                {
                    oldManRectTransform.sizeDelta = new Vector2(oldManRectTransform.sizeDelta.x, -3300);
                }


                Vector3 cam_initial_position = transform.position;
                Vector3 cam_final_position;
                Vector3 player_final_position;
                float remainderX; // Rooms are 16x11
                float remainderY;
                float destCoordX; // the new x coordinate for player
                float destCoordY; // the new y coordinate for player

                // Based on which door was hit determine the final position of the camera and player
                string doorDirection = detectDoors.GetDoorDirection();
                float positionXRounded = Mathf.Round(player.transform.position.x);
                float positionYRounded = Mathf.Round(player.transform.position.y);
                if (doorDirection == "east") {
                    cam_final_position = new Vector3(transform.position.x + 16, transform.position.y, transform.position.z);

                    // Determine coordinates for player position
                    destCoordX = positionXRounded + 2f;
                    remainderY = positionYRounded % 11f;
                    destCoordY = positionYRounded - remainderY + 5f;
                }
                else if (doorDirection == "west") {
                    cam_final_position = new Vector3(transform.position.x - 16, transform.position.y, transform.position.z);

                    // Player Position
                    destCoordX = positionXRounded - 2f;
                    remainderY = positionYRounded % 11f;
                    destCoordY = positionYRounded - remainderY + 5f;
                    Debug.Log(player.transform.position);
                    Debug.Log(destCoordX);
                    Debug.Log(destCoordY);
                }
                else if (doorDirection == "north") {
                    cam_final_position = new Vector3(transform.position.x, transform.position.y + 11, transform.position.z);

                    // Player Position
                    remainderX = (Mathf.Round(player.transform.position.x * 2f) / 2f) % 16f;
                    destCoordX = (Mathf.Round(player.transform.position.x * 2f) / 2f) - remainderX + 7.5f;
                    destCoordY = positionYRounded + 2f;
                }
                else if (doorDirection == "south") { 
                    cam_final_position = new Vector3(transform.position.x, transform.position.y - 11, transform.position.z);

                    // Player Position
                    remainderX = (Mathf.Round(player.transform.position.x * 2f) / 2f) % 16f;
                    destCoordX = (Mathf.Round(player.transform.position.x * 2f) / 2f) - remainderX + 7.5f;
                    destCoordY = positionYRounded - 2f;
                }
                else if (doorDirection == "bow_room_entrance")
                {
                    cam_final_position = new Vector3(transform.position.x, transform.position.y + 11, transform.position.z);
                    // Player Position
                    remainderX = positionXRounded % 16f;
                    destCoordX = positionXRounded - remainderX + 3f;
                    remainderY = positionYRounded % 11f;
                    destCoordY = positionYRounded - remainderY + 20f;
                }
                else // doorDirection == "bow_room_exit"
                {
                    cam_final_position = new Vector3(transform.position.x, transform.position.y - 11, transform.position.z);
                    // Player Position
                    remainderX = positionXRounded % 16f;
                    destCoordX = positionXRounded - remainderX + 6f;
                    remainderY = positionYRounded % 11f;
                    destCoordY = positionYRounded - remainderY - 8f;
                }

                // Set player_final_position using coords above
                // If entering door that's about to be locked need to push player slightly more forward
                // if exiting from old man room
                if (oldManTextAppeared == true)
                {
                    player_final_position = new Vector3(destCoordX + 1, destCoordY, 0);
                    durationOfPlayerEnter = 0.5f;
                }
                else if (destCoordX == 30f && destCoordY == 27f) // Means we're entering locked keese room
                {
                    player_final_position = new Vector3(destCoordX - 1, destCoordY, 0);
                    durationOfPlayerEnter = 0.5f;
                }
                else
                {
                    player_final_position = new Vector3(destCoordX, destCoordY, 0);
                }

                /* Hang around a little bit */
                yield return new WaitForSeconds(durationOfPause);

                // normally just shift camera, but if bow room exit or entrance go to black screen then reappear
                if (doorDirection != "bow_room_exit" && doorDirection != "bow_room_entrance")
                {
                    /* Transition to new "room" */
                    yield return StartCoroutine(
                        CoroutineUtilities.MoveObjectOverTime(transform, cam_initial_position, cam_final_position, durationOfRoomSwitch)
                    );
                }
                else 
                {
                    // Go to black screen
                    transform.position = new Vector3(7.5f, 62f, -10f);
                    yield return new WaitForSeconds(durationOfRoomSwitch);
                    transform.position = cam_final_position;

                    // adjust orientaiton
                    movement.enabled = true;
                    Animator animator = player.GetComponent<Animator>();
                    animator.SetFloat("horizontal_input", 0);
                    animator.SetFloat("vertical_input", -1);
                    rb.velocity = new Vector2(0, -1) * 4f;
                    rb.velocity = new Vector2(0, 0);
                    movement.enabled = false;

                    // If bow_room_entrance need to adjust final player position
                    if (doorDirection == "bow_room_entrance")
                    {
                        player.transform.position = new Vector3(player_final_position.x, player_final_position.y + 1, 0);
                        player_final_position = new Vector3(player_final_position.x, player_final_position.y - 1, 0);
                        durationOfPlayerEnter = 0.5f;
                        rb.velocity = new Vector2(0, -1) * 4f;
                        rb.velocity = new Vector2(0, 0);
                    }
                    
                }

                // Move player to new position, let player move and attack again, and reset doorHit
                /* Transition to new "room" */
                if (doorDirection != "bow_room_exit")
                {
                    yield return StartCoroutine(
                        CoroutineUtilities.MoveObjectOverTime(player.transform, player.transform.position, player_final_position, durationOfPlayerEnter)
                    );
                    Debug.Log(player_final_position);
                }
                else
                {
                    player.transform.position = player_final_position;
                }
                movement.enabled = true;
                Debug.Log(player.transform.position);
                boxCollider.enabled = true;
                playerControls.SetNoPlayerActions(false);
                detectDoors.SetDoorHit(false);

                // if old man text appeared was in old man room, need to close the door
                if (oldManTextAppeared == true)
                {
                    GameObject NewLockedDoor = Instantiate(lockedWestDoorPrefab, new Vector3(17, 38, 0), new Quaternion(0, 0, 0, 0));
                    NewLockedDoor.tag = "special_locked_westdoor";
                    NewLockedDoor.GetComponent<SpriteRenderer>().sprite = specialDoorImage;

                    // Delete special_unlocked_west_door
                    GameObject unlockedDoor = GameObject.FindGameObjectWithTag("special_unlocked_westdoor");
                    Destroy(unlockedDoor);

                    oldManTextAppeared = false;
                }

                // If player is now in the old man room, bring the old man room text into the room
                if ((player.transform.position.x >= 13.9 && player.transform.position.x <= 14.1) &&
                    (player.transform.position.y >= 37.9 && player.transform.position.y <= 38.1))
                {
                    oldManRectTransform.sizeDelta = new Vector2(oldManRectTransform.sizeDelta.x, -1250);
                    oldManTextAppeared = true;
                }
            }

            /* We must yield here to let time pass, or we will hardlock the game (due to infinite while loop) */
            yield return null;
        }
    }
}
