using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombActions : MonoBehaviour
{
    // Player components
    GameObject player;
    PlayerControls playerControls;
    PlayerMovement movement;

    void Start()
    {
        // set Player Components
        player = GameObject.FindWithTag("Player");
        playerControls = player.GetComponent<PlayerControls>();
        movement = player.GetComponent<PlayerMovement>();
    }

    // Function doing the BombExplosion, called from PlayerControls
    public IEnumerator BombExplosion() {
        // Need to wait 0.2s before being able to move since bomb
        // immediately placed in PlayerControl
        // Allow player to move and do other actions again after 0.2s
        AudioController.instance.play_bomb_placement();
        yield return new WaitForSeconds(0.2f);

        playerControls.SetCurrentlyAttackingAlt(false);
        playerControls.SetNoPlayerActions(false);
        movement.enabled = true;
        

        // Wait another 0.8 seconds for bomb explosion
        yield return new WaitForSeconds(0.8f);
        AudioController.instance.play_bomb_explosion();

        // Find all hasHealth components in scene
        HasHealth[] allHasHealth = FindObjectsOfType<HasHealth>();

        // Find all enemy objects with HasHealth components within range of bomb
        List<GameObject> nearbyEnemies = new List<GameObject>();
        Vector3 topRightPos = gameObject.transform.position + new Vector3(-0.5f, -1.0f, 0);
        Vector3 topLeftPos = gameObject.transform.position + new Vector3(0.5f, -1.0f, 0);
        Vector3 midRightPos = gameObject.transform.position + new Vector3(1.0f, 0, 0);
        Vector3 midMidPos = gameObject.transform.position;
        Vector3 midLeftPos = gameObject.transform.position + new Vector3(-1.0f, 0, 0);
        Vector3 botRightPos = gameObject.transform.position + new Vector3(-0.5f, +1.0f, 0);
        Vector3 botLeftPos = gameObject.transform.position + new Vector3(0.5f, +1.0f, 0);
        foreach (HasHealth i in allHasHealth) {
            // If object is within 1 diagonal of bomb, and is not the player, add to nearbyEnemies 
            if ((Vector3.Distance(i.gameObject.transform.position, topRightPos) < 1.0f || Vector3.Distance(i.gameObject.transform.position, topLeftPos) < 1.0f ||
                Vector3.Distance(i.gameObject.transform.position, midRightPos) < 1.0f || Vector3.Distance(i.gameObject.transform.position, midMidPos) < 1.0f ||
                Vector3.Distance(i.gameObject.transform.position, midLeftPos) < 1.0f || Vector3.Distance(i.gameObject.transform.position, botRightPos) < 1.0f ||
                Vector3.Distance(i.gameObject.transform.position, botLeftPos) < 1.0f) && i.gameObject.tag != "player") {

                nearbyEnemies.Add(i.gameObject);
            }
        }

        // Explode the bomb, altering the health of gameObjects in nearbyEnemies
        // Alter health of enemies (reduce by -3)
        foreach (GameObject enemy in nearbyEnemies) {
            HasHealth hasHealth = enemy.GetComponent<HasHealth>();
            hasHealth.AlterHP(-3);
        }
    }
}
