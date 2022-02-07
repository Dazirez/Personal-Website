using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectDoors : MonoBehaviour
{
    // Prefabs for doors
    public GameObject eastDoorPrefab;
    public GameObject westDoorPrefab;
    public GameObject northDoorPrefab;
    public GameObject northDoorChildLeft;
    public GameObject northDoorChildRight;

    PlayerMovement movement; // To get orientaiton
    Inventory inventory; // Determine if player keys

    // Referenced by Camera Movement
    bool doorHit;
    string doorDirection;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        inventory = GetComponent<Inventory>();

        // Starter variables
        doorHit = false;
        doorDirection = "";
    }


    // Determine when Player has hit something that is a trigger
    void OnTriggerEnter(Collider coll) {
        GameObject object_collided_with = coll.gameObject; 

        // If object has hit an unlocked door, determine which type
        if (object_collided_with.tag == "eastdoor") {
            Debug.Log("east door hit");
            doorHit = true;
            doorDirection = "east";
        }
        else if (object_collided_with.tag == "westdoor") {
            Debug.Log("west door hit");
            doorHit = true;
            doorDirection = "west";
        }
        else if (object_collided_with.tag == "special_unlocked_westdoor")
        {
            Debug.Log("west door hit");
            doorHit = true;
            doorDirection = "west";
        }
        else if (object_collided_with.tag == "northdoor") {
            Debug.Log("north door hit");
            doorHit = true;
            doorDirection = "north";
        }
        else if (object_collided_with.tag == "southdoor") { 
            Debug.Log("south door hit");
            doorHit = true;
            doorDirection = "south";
        }
        else if (object_collided_with.tag == "bow_room_entrance")
        {
            Debug.Log("bow room entrance hit");
            doorHit = true;
            doorDirection = "bow_room_entrance";
        }
        else if (object_collided_with.tag == "bow_room_exit")
        {
            Debug.Log("bow room exit hit");
            doorHit = true;
            doorDirection = "bow_room_exit";
        }

        
    }


    // Used by collider to spawn an unlocked door
    void spawnUnlockedDoor(string direction, GameObject object_collided_with) {
        // If player has a key change it to the correct type of door
        if (inventory.GetKeys() > 0) {
            AudioController.instance.play_door_open();
            GameObject newUnlockedDoor;

            // Spawn new unlocked door
            if (direction == "east") {
                newUnlockedDoor = eastDoorPrefab;
            }
            else if (direction == "west") {
                newUnlockedDoor = westDoorPrefab;
            }
            else { // direction == "north"
                newUnlockedDoor = northDoorPrefab;

                Vector3 childLeftPosition = object_collided_with.transform.position + new Vector3(1, 0 , 0);

                // Need to spawn the 2 parts of north door
                Instantiate(northDoorChildLeft, object_collided_with.transform.position, object_collided_with.transform.rotation);
                Instantiate(northDoorChildRight, childLeftPosition, object_collided_with.transform.rotation);
            }

            // Spawn unlocked door
            Instantiate(newUnlockedDoor, object_collided_with.transform.position, object_collided_with.transform.rotation);


            // Remove one key and delete locked door
            inventory.RemoveKeys(1);
            Destroy(object_collided_with);
        }
    }

    // If player has collided with something that results in a collision
    void OnCollisionEnter(Collision coll) {
        GameObject object_collided_with = coll.gameObject; 

        // If collided with a locked door, determine which type
        if (object_collided_with.tag == "locked_eastdoor") { 
            spawnUnlockedDoor("east", object_collided_with);
        }
        else if (object_collided_with.tag == "locked_westdoor") {
            spawnUnlockedDoor("west", object_collided_with);
        }
        else if (object_collided_with.tag == "locked_northdoor") {
            spawnUnlockedDoor("north", object_collided_with);
        }
    }



    public bool GetDoorHit() {
        return doorHit;
    }

    public void SetDoorHit(bool newValue) {
        doorHit = newValue;
    }

    public string GetDoorDirection() {
        return doorDirection;
    }
}