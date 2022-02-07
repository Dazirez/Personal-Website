using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaAttack : MonoBehaviour
{
    public GameObject boomerangPrefab;

    BaseMovement goriyaMovement;
    GameObject boomerangInstance;
    float timeLeft;
    float randomTime;
    bool startBoomerangTimer;

    // Start is called before the first frame update
    void Start()
    {
        goriyaMovement = GetComponent<BaseMovement>();
        startBoomerangTimer = true;

        // Figure out time it'll take to spawn first boomerang
        randomTime = Random.Range(2.0f, 8.0f);
        timeLeft = randomTime;
    }

    void Update()
    {
        // Countdown time, once time reached spawn boomerang
        if (startBoomerangTimer)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                startBoomerangTimer = false;
                StartCoroutine(SpawnAndRotateBoomerang());
            }
        }
    }

    IEnumerator SpawnAndRotateBoomerang()
    {
        // prevent more than one boomerang from being spawned
        if (boomerangInstance == null)
        {

            // Goriya should not be able to move;
            goriyaMovement.enabled = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector2.zero;

            // Find out the orientation of goriya
            string orientation = goriyaMovement.GetOrientation();

            // For each direction same format:
            // modify the rotation and position of the boomerang to fit the direction
            Vector3 spawnPosition;
            Quaternion spawnRotation;
            if (orientation == "down")
            {
                spawnPosition = new Vector3(transform.position.x, transform.position.y - 1.0f, 0f);
                spawnRotation = boomerangPrefab.transform.rotation;
            }
            else if (orientation == "up")
            {
                spawnPosition = new Vector3(transform.position.x, transform.position.y + 1.0f, 0f);
                spawnRotation = boomerangPrefab.transform.rotation * Quaternion.Euler(0, 0, -180);
            }
            else if (orientation == "left")
            {
                spawnPosition = new Vector3(transform.position.x - 1.0f, transform.position.y, 0f);
                spawnRotation = boomerangPrefab.transform.rotation * Quaternion.Euler(0, 0, -90);
            }
            else // orientation == "right"
            {

                spawnPosition = new Vector3(transform.position.x + 1.0f, transform.position.y, 0f);
                spawnRotation = boomerangPrefab.transform.rotation * Quaternion.Euler(0, 0, 90);
            }


            // Spawn the alternate weapon
            boomerangInstance = (GameObject)Instantiate(boomerangPrefab);

            // Rotate and spawn boomerang in right position
            boomerangInstance.transform.position = spawnPosition;
            boomerangInstance.transform.rotation = spawnRotation;

            // Start coroutine for having boomerang fly out and come back
            BoomerangActions boomerangActions = boomerangInstance.GetComponent<BoomerangActions>();
            boomerangActions.playerBoomerang = false;
            StartCoroutine(boomerangActions.MoveBoomerangOverTime(gameObject, orientation, spawnPosition));

            // Wait for this boomerang to be destroyed before reenabling goriya movement
            while (boomerangInstance != null)
            {
                // waiting
                yield return null;
            }
            goriyaMovement.enabled = true;

            // Figure out time it'll take to spawn next boomerang
            randomTime = Random.Range(2.0f, 8.0f);
            timeLeft = randomTime;
            startBoomerangTimer = true;
        }
    }
}

