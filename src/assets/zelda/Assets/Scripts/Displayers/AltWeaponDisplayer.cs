using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltWeaponDisplayer : MonoBehaviour
{
    public Sprite bowSprite;
    public Sprite bombSprite;
    public Sprite boomerangSprite;
    public Sprite blankSprite;

    PlayerControls playerControls;

    // Start is called before the first frame update
    void Start()
    {
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only display alt weapon if player does have one
        if (playerControls.allAltWeapons.Count > 0)
        {
            string currentWeaponTag = playerControls.allAltWeapons[playerControls.currentAltWeaponIndex].tag;
            if (currentWeaponTag == "arrow")
            {
                this.GetComponent<Image>().sprite = bowSprite;
            }
            else if (currentWeaponTag == "weapon_bomb")
            {
                this.GetComponent<Image>().sprite = bombSprite;
            }
            else // currentWeaponTag == "bommerang"
            {
                this.GetComponent<Image>().sprite = boomerangSprite;
            }
        }
        else // If allAltWeapons.Count == 0
        {
            this.GetComponent<Image>().sprite = blankSprite;
        } 
    }
}
