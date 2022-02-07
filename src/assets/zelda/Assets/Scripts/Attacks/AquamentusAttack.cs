using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamentusAttack : MonoBehaviour
{
    public GameObject fireballPrefab;

    AquamentusMovement aquamentusMovement;
    GameObject fireballInstanceTop;
    GameObject fireballInstanceMiddle;
    GameObject fireballInstanceBottom;
    private float timer = 0f; 
    public float attack_timer = 3f; 

    // Start is called before the first frame update
    void Start()
    {
        aquamentusMovement = GetComponent<AquamentusMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 
        if (timer >= attack_timer)
        {
            timer = 0; 
            SpawnFireballs();
        }
    }

    void SpawnFireballs()
    {
        // Determine the starting position for the fireballs (started out group up), rotation stays the same
        Vector3 spawnPosition = new Vector3(transform.position.x - .25f, transform.position.y + .55f, 0f);
        Quaternion spawnRotation = fireballPrefab.transform.rotation;


        // Spawn the three fireballs
        fireballInstanceTop = (GameObject)Instantiate(fireballPrefab);
        fireballInstanceMiddle = (GameObject)Instantiate(fireballPrefab);
        fireballInstanceBottom = (GameObject)Instantiate(fireballPrefab);

        // Find angle between player and aquamentus
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = (player.transform.position - transform.position);
        direction = (new Vector3(direction.x, direction.y, 0)).normalized;
        Vector3 directionMiddle = (new Vector3(direction.x, direction.y + .25f)).normalized;
        Vector3 directionTop = direction;
        Vector3 directionBottom = (new Vector3(direction.x, direction.y - .25f)).normalized;

        // Move fireballs to correct position
        fireballInstanceTop.transform.position = spawnPosition;
        fireballInstanceMiddle.transform.position = spawnPosition;
        fireballInstanceBottom.transform.position = spawnPosition;

        // Run movement function for each fireball
        FireballActions fireballActionsTop = fireballInstanceTop.GetComponent<FireballActions>();
        FireballActions fireballActionsMiddle = fireballInstanceMiddle.GetComponent<FireballActions>();
        FireballActions fireballActionsBottom = fireballInstanceBottom.GetComponent<FireballActions>();

        fireballActionsTop.MoveFireball(directionTop);
        fireballActionsMiddle.MoveFireball(directionMiddle);
        fireballActionsBottom.MoveFireball(directionBottom);
    }
}
