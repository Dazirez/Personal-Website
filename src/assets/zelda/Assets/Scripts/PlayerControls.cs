using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    // Prefabs
    public GameObject primaryWeaponPrefab;
    GameObject altWeaponPrefab;
    public GameObject arrowPrefab;
    public GameObject bombPrefab;
    public GameObject boomerangPrefab;
    public GameObject dustPrefab;

    public List<GameObject> allAltWeapons = new List<GameObject>(); // List of all alternate weapons
    public int currentAltWeaponIndex = 0; // Index represents which weapon we're currently using
    GameObject primaryWeapon;
    GameObject travellingWeapon; // to represent flying sword
    GameObject altWeapon;
    PlayerMovement movement;
    Inventory inventory;
    HasHealth player_health;
    InputToAnimator inputToAnimator;

    bool currentlyAttackingPrimary;
    bool noProjectile;
    bool currentlyAttackingAlt;
    bool noPlayerActions; // Prevent attacking when camera in use
    bool obtainedBow;
    bool obtainedBoomerang;


    // Start is called before the first frame update
    void Start()
    {
        noPlayerActions = false;
        movement = GetComponent<PlayerMovement>();
        inventory = GetComponent<Inventory>();
        player_health = GetComponent<HasHealth>();
        inputToAnimator = GetComponent<InputToAnimator>();

        currentlyAttackingPrimary = false;
        currentlyAttackingAlt = false;

        obtainedBow = false;
        obtainedBoomerang = false;
        noProjectile = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!noPlayerActions)
        {
            // Weapon Selection 
            if (Input.GetKeyDown(KeyCode.X))
            {
                AudioController.instance.play_use_sword();
                if (GetComponent<HasHealth>().isFullHealth() || GameController.instance.inGodMode())
                {
                    AudioController.instance.play_shoot_sword();
                }
                StartCoroutine(SpawnAndRotatePrimary());
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                if (allAltWeapons.Count > 0)
                {

                    StartCoroutine(SpawnAndRotateAlt());
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Scrolling through Weapons");
                if (allAltWeapons.Count > 0)
                {
                    // Rotate through a list of weapons to determine which alt weapon is being used
                    if (currentAltWeaponIndex < (allAltWeapons.Count - 1))
                    {
                        currentAltWeaponIndex++;
                    }
                    else // reached last weapon in list so go back to beginning
                    {
                        currentAltWeaponIndex = 0;
                    }
                    altWeaponPrefab = allAltWeapons[currentAltWeaponIndex];
                    altWeaponPrefab.transform.rotation = allAltWeapons[currentAltWeaponIndex].transform.rotation;
                }
            }
        }

        // User can press god mode whenever they want, doesn't matter if no other controls are allowed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // If entering god mode
            if (GameController.instance.inGodMode() == false)
            {
                if (!allAltWeapons.Contains(arrowPrefab))
                {
                    AddAlternateWeapon("bow", true);
                }
                if (!allAltWeapons.Contains(boomerangPrefab))
                {
                    AddAlternateWeapon("boomerang", true);
                }
                // If allAltWeapons does not have bombPrefab, boomerangPrefab, or arrowPrefab add it
                if (!allAltWeapons.Contains(bombPrefab))
                {
                    AddAlternateWeapon("bomb", true);
                }
            }

            GameController.instance.setGodMode(!GameController.instance.inGodMode());

            // If exiting god mode
            if (GameController.instance.inGodMode() == false)
            {
                // If user has no bombs, remove bombs from allAltWeapons
                if (inventory.GetBombs() == 0)
                {
                    RemoveWeaponFromList("bomb");
                }

                // If user has not obtained boomerang remove from allAltWeapons
                if (obtainedBoomerang == false)
                {
                    RemoveWeaponFromList("boomerang");
                }

                // If use has not obtained bow remove from allAltWeapons
                if (obtainedBow == false)
                {
                    RemoveWeaponFromList("bow");
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)) { 
           GameController.instance.switchScenes(); 
        }
    }
    // Spawn the sword in the correct direction
    IEnumerator SpawnAndRotatePrimary()
    {
        // prevent more than one sword from being spawned, and can't attack while alt used
        if (primaryWeapon == null && !currentlyAttackingAlt)
        {
            currentlyAttackingPrimary = true;

            // Player should not be able to move or do any other actions;
            movement.enabled = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector2.zero;
            noPlayerActions = true;

            // Find out the orientation of the character
            string orientation = movement.GetOrientation();

            // For each direction same format:
            // Modify player position, to account for new graphic, ignore for now
            // then modify the rotation and position of the sword
            Vector3 spawnPosition;
            Quaternion spawnRotation;
            if (orientation == "down")
            {
                spawnPosition = new Vector3(transform.position.x + .03125f, transform.position.y - 0.68127f, 0f);
                spawnRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (orientation == "up")
            {
                spawnPosition = new Vector3(transform.position.x - 0.0938f, transform.position.y + 0.725f, 0f);
                spawnRotation = Quaternion.Euler(0, 0, 180);
            }
            else if (orientation == "left")
            {
                spawnPosition = new Vector3(transform.position.x - .6812f, transform.position.y - .0625f, 0f);
                spawnRotation = Quaternion.Euler(0, 0, 270);
            }
            else
            { // orientation == "right"
                spawnPosition = new Vector3(transform.position.x + .6812f, transform.position.y - .0625f, 0f);
                spawnRotation = Quaternion.Euler(0, 0, 90);
            }


            // Spawn the primary weapon and adjust it to the correct position
            primaryWeapon = (GameObject)Instantiate(primaryWeaponPrefab);
            primaryWeapon.transform.position = spawnPosition;
            primaryWeapon.transform.rotation = spawnRotation;

            // Call Animation for sword swing
            inputToAnimator.attackPrimary();

            // The sword that is swung (not the one flying) is deleted and variables changed after 0.2s
            yield return new WaitForSeconds(0.2f);
            GameObject tempWeapon = null;
            if (travellingWeapon == null)
            {
                tempWeapon = primaryWeapon;
            }
            else
            {
                Destroy(primaryWeapon);
            }
            primaryWeapon = null;
            currentlyAttackingPrimary = false;
            movement.enabled = true;
            noPlayerActions = false;

            // If max health then sword will travel if no other traveling sword, start a new Coroutine
            if (player_health.GetHealth() == player_health.max_health)
            {
                Debug.Log(noProjectile);
                if (travellingWeapon == null && tempWeapon != null && noProjectile == true)
                {
                    Debug.Log("GO IN HERE");
                    // Spawn a weapon and start coroutine with this weapon
                    travellingWeapon = tempWeapon;
                    travellingWeapon.transform.position = spawnPosition;
                    travellingWeapon.transform.rotation = spawnRotation;
                    noProjectile = false;
                    StartCoroutine(travellingWeapon.GetComponent<WeaponGeneralActions>().MoveWeaponOverTime(travellingWeapon, orientation, spawnPosition));
                }
                else
                {
                    Destroy(tempWeapon);
                }
            }
            else
            {
                Destroy(tempWeapon);
            }
        }
    }

    IEnumerator SpawnAndRotateAlt()
    {
        // prevent more than one alt weapon from being spawned and check sword not being used
        if (altWeapon == null && !currentlyAttackingPrimary)
        {

            // Use a rupee when using arrow
            if (altWeaponPrefab.tag == "arrow")
            {
                // If not in god mode

                AudioController.instance.play_arrow();

                if (!GameController.instance.inGodMode())
                {
                    // Check there are rupees, if there isn't exit
                    if (inventory.GetRupees() <= 0)
                    {
                        yield break;
                    }
                    inventory.RemoveRupees(1);
                }
            }

            // Use a bomb when using bomb
            if (altWeaponPrefab.tag == "weapon_bomb")
            {
                // If not in god mode
                if (!GameController.instance.inGodMode())
                {
                    // Check there are boms available in inventory
                    if (inventory.GetBombs() <= 0)
                    {
                        yield break;
                    }
                    inventory.RemoveBombs(1);
                }
            }

            currentlyAttackingAlt = true;

            // Player should not be able to move;
            movement.enabled = false;

            // Find out the orientation of the character
            string orientation = movement.GetOrientation();

            // For each direction same format:
            // modify the rotation and position of the alternate weapon
            Vector3 arrowSpawnPosition;
            Quaternion arrowSpawnRotation; // goes +90 degrees each time
            Vector3 bombBoomerSpawnPosition; // both always spawned one tile away
            Quaternion boomerangSpawnRotation; // goes -90 degrees each time
            if (orientation == "down")
            {
                arrowSpawnPosition = new Vector3(transform.position.x, transform.position.y - 1.1f, 0f);
                arrowSpawnRotation = Quaternion.Euler(0, 0, 0);

                bombBoomerSpawnPosition = new Vector3(transform.position.x, transform.position.y - 1.0f, 0f);
                boomerangSpawnRotation = altWeaponPrefab.transform.rotation;
            }
            else if (orientation == "up")
            {
                arrowSpawnPosition = new Vector3(transform.position.x, transform.position.y + 1.1f, 0f);
                arrowSpawnRotation = Quaternion.Euler(0, 0, 180);

                bombBoomerSpawnPosition = new Vector3(transform.position.x, transform.position.y + 1.0f, 0f);
                boomerangSpawnRotation = altWeaponPrefab.transform.rotation * Quaternion.Euler(0, 0, -180);
            }
            else if (orientation == "left")
            {
                arrowSpawnPosition = new Vector3(transform.position.x - 1.1f, transform.position.y - .065f, 0f);
                arrowSpawnRotation = Quaternion.Euler(0, 0, 270);

                bombBoomerSpawnPosition = new Vector3(transform.position.x - 1.0f, transform.position.y, 0f);
                boomerangSpawnRotation = altWeaponPrefab.transform.rotation * Quaternion.Euler(0, 0, -90);
            }
            else
            { // orientation == "right"
                Debug.Log(orientation);
                arrowSpawnPosition = new Vector3(transform.position.x + 1.1f, transform.position.y - .065f, 0f);
                arrowSpawnRotation = Quaternion.Euler(0, 0, 90);

                bombBoomerSpawnPosition = new Vector3(transform.position.x + 1.0f, transform.position.y, 0f);
                boomerangSpawnRotation = altWeaponPrefab.transform.rotation * Quaternion.Euler(0, 0, 90);
            }


            // Spawn the alternate weapon
            altWeapon = (GameObject)Instantiate(altWeaponPrefab);

            // Scenario depending on weapon
            // Every scenario playerMovement will be reenabled in those functions
            // if arrow
            // Use arrowspawnPosition and rotate and send fly 
            if (altWeaponPrefab.tag == "arrow")
            {
                altWeapon.transform.position = arrowSpawnPosition;
                altWeapon.transform.rotation = arrowSpawnRotation;

                // Call Animation for bow
                inputToAnimator.attackBow();

                // Start coroutine for having weapon fly
                StartCoroutine(altWeapon.GetComponent<WeaponGeneralActions>().MoveWeaponOverTime(altWeapon, orientation, arrowSpawnPosition));

                // Wait 0.2 seconds before renenabling player controls
                // Allow player to move and do other actions again after 0.2s
                yield return new WaitForSeconds(0.2f);
                SetCurrentlyAttackingAlt(false);
                SetNoPlayerActions(false);
                movement.enabled = true;
            }
            else if (altWeaponPrefab.tag == "weapon_bomb")
            {
                // Don't rotate use bombBoomerSpawnPosition
                altWeapon.transform.position = bombBoomerSpawnPosition;

                // Call Animation for bomb placement
                inputToAnimator.attackBomb();

                // Allow BombActions.cs to make the bomb explode
                BombActions bombActions = altWeapon.GetComponent<BombActions>();
                yield return StartCoroutine(bombActions.BombExplosion());

                Vector3 bombPosition = altWeapon.transform.position;

                // Destroy the bomb
                Destroy(altWeapon);

                // Spawn dust sprites indicating blast radius
                // Will appear for 0.5 seconds then destroy them
                Vector3 topRightPos = bombPosition + new Vector3(-0.5f, 1.0f, 0);
                Vector3 topLeftPos = bombPosition + new Vector3(0.5f, 1.0f, 0);
                Vector3 midRightPos = bombPosition + new Vector3(1.0f, 0, 0);
                Vector3 midMidPos = bombPosition;
                Vector3 midLeftPos = bombPosition + new Vector3(-1.0f, 0, 0);
                Vector3 botRightPos = bombPosition + new Vector3(-0.5f, -1.0f, 0);
                Vector3 botLeftPos = bombPosition + new Vector3(0.5f, -1.0f, 0);

                GameObject dustTopRight = (GameObject)Instantiate(dustPrefab);
                dustTopRight.transform.position = topRightPos;
                GameObject dustTopLeft = (GameObject)Instantiate(dustPrefab);
                dustTopLeft.transform.position = topLeftPos;
                GameObject dustMidRight = (GameObject)Instantiate(dustPrefab);
                dustMidRight.transform.position = midRightPos;
                GameObject dustMidMid = (GameObject)Instantiate(dustPrefab);
                dustMidMid.transform.position = midMidPos;
                GameObject dustMidLeft = (GameObject)Instantiate(dustPrefab);
                dustMidLeft.transform.position = midLeftPos;
                GameObject dustBotRight = (GameObject)Instantiate(dustPrefab);
                dustBotRight.transform.position = botRightPos;
                GameObject dustBotLeft = (GameObject)Instantiate(dustPrefab);
                dustBotLeft.transform.position = botLeftPos;

                // Wait 0.5 seconds then delete the dust
                yield return new WaitForSeconds(0.5f);
                Destroy(dustTopRight);
                Destroy(dustTopLeft);
                Destroy(dustMidRight);
                Destroy(dustMidMid);
                Destroy(dustMidLeft);
                Destroy(dustBotLeft);
                Destroy(dustBotRight);
            }
            else // altWeaponPrefab.tag == "boomerang"
            {
                // Rotate and spawn boomerang in right position
                altWeapon.transform.position = bombBoomerSpawnPosition;
                altWeapon.transform.rotation = boomerangSpawnRotation;

                // Call Animation for boomerang
                inputToAnimator.attackBoomerang();

                // Start coroutine for having boomerang fly out and come back
                BoomerangActions boomerangActions = altWeapon.GetComponent<BoomerangActions>();
                boomerangActions.playerBoomerang = true;
                StartCoroutine(boomerangActions.MoveBoomerangOverTime(gameObject, orientation, bombBoomerSpawnPosition));

                // Wait 0.2 seconds before renenabling player controls
                // Allow player to move and do other actions again after 0.2s
                yield return new WaitForSeconds(0.2f);
                SetCurrentlyAttackingAlt(false);
                SetNoPlayerActions(false);
                movement.enabled = true;

            }
        }
    }

    // Get the variable currentlyAttackingPrimary, used by Animator to determine 
    // when attacking
    public bool GetCurrentlyAttackingPrimary()
    {
        return currentlyAttackingPrimary;
    }

    public void SetNoProjectile(bool newValue)
    {
        noProjectile = newValue;
    }

    public bool GetNoProjectile()
    {
        return noProjectile;
    }

    public void SetCurrentlyAttackingAlt(bool newValue)
    {
        currentlyAttackingAlt = newValue;
    }

    public bool GetNoPlayerActions()
    {
        return noPlayerActions;
    }

    public void SetNoPlayerActions(bool newValue)
    {
        noPlayerActions = newValue;
    }

    public void AddAlternateWeapon(string weaponName, bool addedInGM)
    {
        Debug.Log("ADD ALT WEAPON");
        // Depending on what weapon picked up add to allAltWeapons
        if (weaponName == "bow")
        {
            if (!allAltWeapons.Contains(arrowPrefab))
            {
                allAltWeapons.Add(arrowPrefab);
            }
            if (!addedInGM)
            {
                obtainedBow = true;
            }
        }
        else if (weaponName == "bomb")
        {
            allAltWeapons.Add(bombPrefab);
        }
        else if (weaponName == "boomerang")
        {
            if (!allAltWeapons.Contains(boomerangPrefab))
            {
                allAltWeapons.Add(boomerangPrefab);
            }
            if (!addedInGM)
            {
                obtainedBoomerang = true;
            }
        }

        // Equip alt weapon if this is the first one
        if (allAltWeapons.Count == 1)
        {
            currentAltWeaponIndex = 0;
            altWeaponPrefab = allAltWeapons[currentAltWeaponIndex];
        }
    }
    public void RemoveWeaponFromList(string weapon)
    {
        GameObject weaponRemovedPrefab;
        if (weapon == "bomb")
        {
            weaponRemovedPrefab = bombPrefab;
        }
        else if (weapon == "boomerang")
        {
            weaponRemovedPrefab = boomerangPrefab;
        }
        else // weapon == "bow" 
        {
            weaponRemovedPrefab = arrowPrefab;
        }

        allAltWeapons.Remove(weaponRemovedPrefab);

        // If currently using bomb
        if (altWeaponPrefab == weaponRemovedPrefab)
        {
            // Rotate through a list of weapons to determine which alt weapon is being used
            if (currentAltWeaponIndex < (allAltWeapons.Count - 1))
            {
                currentAltWeaponIndex++;
            }
            else // reached last weapon in list so go back to beginning
            {
                currentAltWeaponIndex = 0;
            }
            if (allAltWeapons.Count > 0)
            {
                altWeaponPrefab = allAltWeapons[currentAltWeaponIndex];
                altWeaponPrefab.transform.rotation = allAltWeapons[currentAltWeaponIndex].transform.rotation;
            }
        }
    }
}
