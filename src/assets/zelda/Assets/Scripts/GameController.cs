using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public static GameController instance;

    // Prefabs of all the corresponding Level Controllers
    // 1st number is x coord, 2nd number is y coord
    public GameObject room10;
    public GameObject room20;
    public GameObject room30;
    public GameObject room21;
    public GameObject room12;
    public GameObject room22;
    public GameObject room32;
    public GameObject room03;
    public GameObject room13;
    public GameObject room23;
    public GameObject room33;
    public GameObject room43;
    public GameObject room24;
    public GameObject room44;
    public GameObject room15;
    public GameObject room54;
    public GameObject room16;
    public GameObject room25;

    // Stores coordinates of room to the correpsonding level controller
    public Dictionary<Vector2, LevelController> coordinateToLevelController = new Dictionary<Vector2, LevelController>();

    bool godMode = false;
    public string curr_scene = "dungeon"; 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            //Fix screen resolution
            Screen.SetResolution(1500 * (256 / 240), 1500, true);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Assign all coordinates to corresponding levelcontroller
    void Start()
    {
        // Room 1 0
        coordinateToLevelController.Add(new Vector2(1.0f, 0.0f), room10.GetComponent<LevelController>());
        // Room 2 0
        coordinateToLevelController.Add(new Vector2(2.0f, 0.0f), room20.GetComponent<LevelController>());
        // Room 3 0
        coordinateToLevelController.Add(new Vector2(3.0f, 0.0f), room30.GetComponent<LevelController>());
        // Room 2 1
        coordinateToLevelController.Add(new Vector2(2.0f, 1.0f), room21.GetComponent<LevelController>());
        // Room 1 2
        coordinateToLevelController.Add(new Vector2(1.0f, 2.0f), room12.GetComponent<LevelController>());
        // Room 2 2
        coordinateToLevelController.Add(new Vector2(2.0f, 2.0f), room22.GetComponent<LevelController>());
        // Room 3 2
        coordinateToLevelController.Add(new Vector2(3.0f, 2.0f), room32.GetComponent<LevelController>());
        // Room 0 3
        coordinateToLevelController.Add(new Vector2(0.0f, 3.0f), room03.GetComponent<LevelController>());
        // Room 1 3
        coordinateToLevelController.Add(new Vector2(1.0f, 3.0f), room13.GetComponent<LevelController>());
        // Room 2 3
        coordinateToLevelController.Add(new Vector2(2.0f, 3.0f), room23.GetComponent<LevelController>());
        // Room 3 3
        coordinateToLevelController.Add(new Vector2(3.0f, 3.0f), room33.GetComponent<LevelController>());
        // Room 4 3
        coordinateToLevelController.Add(new Vector2(4.0f, 3.0f), room43.GetComponent<LevelController>());
        // Room 2 4
        coordinateToLevelController.Add(new Vector2(2.0f, 4.0f), room24.GetComponent<LevelController>());
        // Room 4 4
        coordinateToLevelController.Add(new Vector2(4.0f, 4.0f), room44.GetComponent<LevelController>());
        // Room 1 5
        coordinateToLevelController.Add(new Vector2(1.0f, 5.0f), room15.GetComponent<LevelController>());
        // Room 5 4
        coordinateToLevelController.Add(new Vector2(5.0f, 4.0f), room54.GetComponent<LevelController>());
        // Room 1 6
        coordinateToLevelController.Add(new Vector2(1.0f, 6.0f), room16.GetComponent<LevelController>());
        // Room 2 5
        coordinateToLevelController.Add(new Vector2(2.0f, 5.0f), room25.GetComponent<LevelController>());
    }

    public void switchScenes() { 
        if(curr_scene == "dungeon") { 
            SceneManager.LoadScene("Old_man_boss", LoadSceneMode.Single);
            curr_scene = "boss"; 
        }
        else if(curr_scene == "boss") { 
            curr_scene = "dungeon"; 
            SceneManager.LoadScene("Dungeon-Scene", LoadSceneMode.Single);
        }

    }

    /*Getter Function*/
    public bool inGodMode()
    {
        return godMode;
    }

    /*Setter Function*/
    public void setGodMode(bool b)
    {

        godMode = b;
        Debug.Log("Godemode: " + godMode);
        if (godMode == false)
        {
            GameObject.FindGameObjectWithTag("Player").layer = 6;
            GameObject.FindGameObjectWithTag("Player").GetComponent<HasHealth>().setInvincible(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
