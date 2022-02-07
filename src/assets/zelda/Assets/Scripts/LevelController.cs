using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int remainingEnemies;
    GameObject enemiesContainer;
    GameObject itemsContainer;
    Vector2 roomCoordinate;
    Scene currentScene;

    public Sprite eastDoorImage;
    public Sprite blockedEastDoorImage;
    public Sprite westDoorImage;
    public GameObject lockedWestDoorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // SetActive = false for parent object holding all the enemies, so enemies appear
        enemiesContainer = gameObject.transform.Find("Enemies").gameObject;
        enemiesContainer.SetActive(false);

        // SetActive = false for parent object holding all items, so items disapeear
        itemsContainer = gameObject.transform.Find("Items").gameObject;
        itemsContainer.SetActive(false);

        // Figure out which room we're in
        float startOfRoomX = gameObject.transform.position.x;
        float startOfRoomY = gameObject.transform.position.y;
        roomCoordinate = new Vector2(startOfRoomX / 16f, startOfRoomY / 11f);

        currentScene = SceneManager.GetActiveScene();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject objectCollidedWith = other.gameObject;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (objectCollidedWith.tag == "Player")
        {
            Debug.Log("WE ARE ENTERING INTO THIRD ROOM");
            // SetActive = true for parent object holding all the enemies, so enemies appear
            enemiesContainer.SetActive(true);

            // Set active items if remaining enemies is 0
            if (remainingEnemies == 0)
            {
                itemsContainer.SetActive(true);
            }

            // Need to close door for these two rooms if there are still enemies
            if ((roomCoordinate.x == 1 && roomCoordinate.y == 2) || (roomCoordinate.x == 4 && roomCoordinate.y == 4) && remainingEnemies > 0)
            {
                if (remainingEnemies > 0)
                {
                    GameObject doorContainer = gameObject.transform.Find("Doors").gameObject;
                    GameObject eastDoor = doorContainer.transform.Find("East Door").gameObject;
                    eastDoor.GetComponent<SpriteRenderer>().sprite = blockedEastDoorImage;

                    BoxCollider eastDoorBoxCollider = eastDoor.GetComponent<BoxCollider>();
                    eastDoorBoxCollider.isTrigger = false;
                    eastDoorBoxCollider.center = new Vector3(0.01f, -0.25f, 0);
                    eastDoorBoxCollider.size = new Vector3(1f, 0.1f, 0.2f);

                    // Move player slightly too right if roomCoordinate is 1, 2
                    if (roomCoordinate.x == 1 && roomCoordinate.y == 2)
                    {
                        player.transform.position = new Vector3(player.transform.position.x - 0.05f, player.transform.position.y, 0);

                    }
                }
            }

            // For custom stage
            if (currentScene.name == "Old_Man_Boss")
            {
                if (roomCoordinate.x == 1 && roomCoordinate.y == 0)
                {
                    GameObject doorContainer = gameObject.transform.Find("Doors").gameObject;
                    GameObject eastDoor = doorContainer.transform.Find("East Door").gameObject;
                    eastDoor.GetComponent<SpriteRenderer>().sprite = blockedEastDoorImage;

                    BoxCollider eastDoorBoxCollider = eastDoor.GetComponent<BoxCollider>();
                    eastDoorBoxCollider.isTrigger = false;
                    eastDoorBoxCollider.center = new Vector3(0.01f, -0.25f, 0);
                    eastDoorBoxCollider.size = new Vector3(1f, 0.1f, 0.2f);
                }

                if (roomCoordinate.x == 2 && roomCoordinate.y == 0)
                {
                    GameObject doorContainer = gameObject.transform.Find("Doors").gameObject;
                    GameObject eastDoor = doorContainer.transform.Find("East Door").gameObject;
                    eastDoor.GetComponent<SpriteRenderer>().sprite = blockedEastDoorImage;

                    BoxCollider eastDoorBoxCollider = eastDoor.GetComponent<BoxCollider>();
                    eastDoorBoxCollider.isTrigger = false;
                    eastDoorBoxCollider.center = new Vector3(0.01f, -0.25f, 0);
                    eastDoorBoxCollider.size = new Vector3(1f, 0.1f, 0.2f);

                    player.transform.position = new Vector3(player.transform.position.x + 1f, player.transform.position.y, 0);

                    GameObject westDoor = doorContainer.transform.Find("West Door").gameObject;
                    westDoor.GetComponent<SpriteRenderer>().sprite = blockedEastDoorImage;

                    BoxCollider westDoorBoxCollider = westDoor.GetComponent<BoxCollider>();
                    westDoorBoxCollider.isTrigger = false;
                    westDoorBoxCollider.center = new Vector3(-0.01f, -0.25f, 0);
                    westDoorBoxCollider.size = new Vector3(1f, 0.1f, 0.2f);

                    
                }
            }

            // Reset rooms with pushable blocks
            // If not in room with pushable block
            PushBlock pushBlock = player.GetComponent<PushBlock>();
            GameObject[] pushableBlocks;
            pushableBlocks = GameObject.FindGameObjectsWithTag("pushable_block");
            if (pushBlock.beforeOldBlockPushed == true || pushBlock.beforeBowBlockPushed == true)
            {
                if ((Vector2.Distance(roomCoordinate, new Vector2(1, 5)) != 0) && (Vector2.Distance(roomCoordinate, new Vector2(1, 3)) != 0))
                {
                    for (int i = 0; i < pushableBlocks.Length; i++)
                    {
                        // Before old man room block
                        if (pushableBlocks[i].transform.position.y < 40)
                        {
                            pushableBlocks[i].transform.position = new Vector3(23.0f, 38.0f, 0);
                        }
                        else // before bow room block
                        {
                            pushableBlocks[i].transform.position = new Vector3(22.0f, 60.0f, 0);
                        }
                    }

                    if (pushBlock.beforeOldBlockPushed == true && ((Vector2.Distance(roomCoordinate, new Vector2(1, 2))) == 0
                        || (Vector2.Distance(roomCoordinate, new Vector2(2, 3)) == 0)))
                    {
                        GameObject NewLockedDoor = Instantiate(lockedWestDoorPrefab, new Vector3(17, 38, 0), new Quaternion(0, 0, 0, 0));
                        NewLockedDoor.tag = "special_locked_westdoor";

                        // Delete special_unlocked_west_door
                        GameObject unlockedDoor = GameObject.FindGameObjectWithTag("special_unlocked_westdoor");
                        Destroy(unlockedDoor);
                    }

                    pushBlock.beforeBowBlockPushed = false;
                    pushBlock.beforeOldBlockPushed = false;
                }
            }

            // If entered old man room play the correct audio
            if (roomCoordinate.x == 0 && roomCoordinate.y == 3)
            {
                AudioController.instance.play_enter_old();
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        GameObject objectCollidedWith = other.gameObject;

        if (objectCollidedWith.tag == "Player")
        {
            // SetActive = false for parent object holding all the enemies, so enemies disappear
            if (roomCoordinate.x == 3 && roomCoordinate.y == 0 && currentScene.name == "Old_Man_Boss")
            {
                // Don't do anything
            }
            else
            {
                enemiesContainer.SetActive(false);
                itemsContainer.SetActive(false);
            }
        }   
    }

    public void reduceEnemies(int num_enemies_removed, Vector2 roomCoord)
    {
        remainingEnemies = remainingEnemies - num_enemies_removed;

        // If no enemies left make item appear
        if (remainingEnemies == 0)
        {
            // If item being spawned is key  play key appear sound
            if (roomCoord.x == 1 && roomCoord.y == 0 && currentScene.name != "Old_Man_Boss")
            {
                AudioController.instance.play_key_appear();
            }
            else if (roomCoord.x == 2 && roomCoord.y == 2)
            {
                AudioController.instance.play_key_appear();
            }
            else if (roomCoord.x == 2 && roomCoord.y == 5)
            {
                AudioController.instance.play_key_appear();
            }

            if (currentScene.name == "Old_Man_Boss")
            {
                if (roomCoord.x == 1 && roomCoord.y == 0)
                {
                    GameObject doorContainer = gameObject.transform.Find("Doors").gameObject;
                    GameObject eastDoor = doorContainer.transform.Find("East Door").gameObject;
                    eastDoor.GetComponent<SpriteRenderer>().sprite = eastDoorImage;

                    BoxCollider eastDoorBoxCollider = eastDoor.GetComponent<BoxCollider>();
                    eastDoorBoxCollider.isTrigger = true;
                    eastDoorBoxCollider.center = new Vector3(1.3f, -0.25f, 0);
                    eastDoorBoxCollider.size = new Vector3(0.01f, 0.1f, 1);
                }

                if (roomCoord.x == 2 && roomCoord.y == 0)
                {
                    GameObject doorContainer = gameObject.transform.Find("Doors").gameObject;
                    GameObject eastDoor = doorContainer.transform.Find("East Door").gameObject;
                    eastDoor.GetComponent<SpriteRenderer>().sprite = eastDoorImage;

                    BoxCollider eastDoorBoxCollider = eastDoor.GetComponent<BoxCollider>();
                    eastDoorBoxCollider.isTrigger = true;
                    eastDoorBoxCollider.center = new Vector3(1.3f, -0.25f, 0);
                    eastDoorBoxCollider.size = new Vector3(0.01f, 0.1f, 1);

                    GameObject westDoor = doorContainer.transform.Find("West Door").gameObject;
                    westDoor.GetComponent<SpriteRenderer>().sprite = westDoorImage;

                    BoxCollider westDoorBoxCollider = westDoor.GetComponent<BoxCollider>();
                    westDoorBoxCollider.isTrigger = true;
                    westDoorBoxCollider.center = new Vector3(-1.3f, -0.25f, 0);
                    westDoorBoxCollider.size = new Vector3(0.01f, 0.1f, 1f);
                }
            }

            itemsContainer.SetActive(true);

            // Need to open door for these two rooms
            // Figure out which room we're in
            float startOfRoomX = gameObject.transform.position.x;
            float startOfRoomY = gameObject.transform.position.y;
            Vector2 roomCoordinate = new Vector2(startOfRoomX / 16f, startOfRoomY / 11f);
            if ((roomCoordinate.x == 1 && roomCoordinate.y == 2) || (roomCoordinate.x == 4 && roomCoordinate.y == 4))
            {
                GameObject doorContainer = gameObject.transform.Find("Doors").gameObject;
                GameObject eastDoor = doorContainer.transform.Find("East Door").gameObject;
                eastDoor.GetComponent<SpriteRenderer>().sprite = eastDoorImage;

                BoxCollider eastDoorBoxCollider = eastDoor.GetComponent<BoxCollider>();
                eastDoorBoxCollider.isTrigger = true;
                eastDoorBoxCollider.center = new Vector3(1.3f, -0.25f, 0);
                eastDoorBoxCollider.size = new Vector3(0.01f, 0.1f, 1);
            }
        }
    }
}
