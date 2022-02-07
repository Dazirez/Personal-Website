using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordExplosion : MonoBehaviour
{
    // For sword explosion
    public GameObject explosionTopRightPrefab;
    public GameObject explosionTopLeftPrefab;
    public GameObject explosionBottomRightPrefab;
    public GameObject explosionBottomLeftPrefab;

    GameObject explosionTopRight;
    GameObject explosionTopLeft;
    GameObject explosionBottomRight;
    GameObject explosionBottomLeft;

    PlayerControls playerControls;

    float timeLeft;
    bool startTimer;

    // Start is called before the first frame update
    void Start()
    {
        startTimer = false;
        timeLeft = 0.4f;
        playerControls = GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timeLeft -= Time.deltaTime;
        }

        if (timeLeft <= 0.0f)
        {
            Destroy(explosionTopRight);
            Destroy(explosionTopLeft);
            Destroy(explosionBottomRight);
            Destroy(explosionBottomLeft);
            GameObject[] destroyThese = GameObject.FindGameObjectsWithTag("sword_dust");
            for (int i = 0; i < destroyThese.Length; i++)
            {
                Destroy(destroyThese[i]);
            }
            startTimer = false;
            timeLeft = 0.4f;
            playerControls.SetNoProjectile(true);
        }
    }

    // Does the sword explosion
    public void swordExplosionAction(Vector3 swordPos)
    {
        
        // Spawn all the explosions
        Quaternion generalRotation = Quaternion.Euler(0, 0, 0);

        explosionTopRight = (GameObject)Instantiate(explosionTopRightPrefab);
        explosionTopRight.transform.position = new Vector3(swordPos.x + .1f, swordPos.y, 0f);
        explosionTopRight.transform.rotation = generalRotation;
        Vector3 explTRFinalPos = new Vector3(swordPos.x + 1.6f, swordPos.y + 1.6f, 0f);

        explosionTopLeft = (GameObject)Instantiate(explosionTopLeftPrefab);
        explosionTopLeft.transform.position = new Vector3(swordPos.x - .1f, swordPos.y, 0f);
        explosionTopLeft.transform.rotation = generalRotation;
        Vector3 explTLFinalPos = new Vector3(swordPos.x - 1.6f, swordPos.y + 1.6f, 0f);

        explosionBottomRight = (GameObject)Instantiate(explosionBottomRightPrefab);
        explosionBottomRight.transform.position = new Vector3(swordPos.x + .1f, swordPos.y - .2f, 0f);
        explosionBottomRight.transform.rotation = generalRotation;
        Vector3 explBRFinalPos = new Vector3(swordPos.x + 1.6f, swordPos.y - 1.6f, 0f);

        explosionBottomLeft = (GameObject)Instantiate(explosionBottomLeftPrefab);
        explosionBottomLeft.transform.position = new Vector3(swordPos.x - .1f, swordPos.y - .2f, 0f);
        explosionBottomLeft.transform.rotation = generalRotation;
        Vector3 explBLFinalPos = new Vector3(swordPos.x - 1.6f, swordPos.y - 1.6f, 0f);

        startTimer = true;
        StartCoroutine(CoroutineUtilities.MoveObjectOverTime(explosionTopRight.transform, explosionTopRight.transform.position, explTRFinalPos, 0.4f));
        StartCoroutine(CoroutineUtilities.MoveObjectOverTime(explosionTopLeft.transform, explosionTopLeft.transform.position, explTLFinalPos, 0.4f));
        StartCoroutine(CoroutineUtilities.MoveObjectOverTime(explosionBottomRight.transform, explosionBottomRight.transform.position, explBRFinalPos, 0.4f));
        StartCoroutine(CoroutineUtilities.MoveObjectOverTime(explosionBottomLeft.transform, explosionBottomLeft.transform.position, explBLFinalPos, 0.4f));
    }
}
