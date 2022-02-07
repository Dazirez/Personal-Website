using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   
    /*Data Storage */
    int key_count = 0;
    int rupee_count = 0; 
    int bomb_count = 0; 
    
    int max_count = 9999;
    // HashSet<Weapon> available_weapons = new HashSet<Weapon>(); 

    /*Link's Max Health -> can be increased later through heart containers*/
    // Rupee Functions
    public void AddRupees(int num_rupees) { 
        rupee_count += num_rupees; 
    }
    public void RemoveRupees(int num_rupees_removed) {
        if (!GameController.instance.inGodMode())
        {
            rupee_count = rupee_count - num_rupees_removed;
        }
    }
    public int GetRupees() { 
        if(GameController.instance.inGodMode()) return max_count; 
        return rupee_count; 
    }

    // Key Functions
    public void AddKeys(int num_new_keys) {
        key_count += num_new_keys;
    }
    public void RemoveKeys(int num_keys_removed) {
        if (!GameController.instance.inGodMode())
        {
            key_count = key_count - num_keys_removed;
        }
    }
    public int GetKeys() {
        if(GameController.instance.inGodMode()) return max_count; 
        return key_count;
    }

    // Bomb Functions
    public void AddBombs(int num_new_bombs)
    {
        // Check if bomb should be added to alternate weapons list
        // If before no bombs and currently not in god mode
        if (num_new_bombs > 0 && bomb_count == 0 && GetBombs() == 0)
        {
            PlayerControls playerControls = GetComponent<PlayerControls>();
            playerControls.AddAlternateWeapon("bomb", false);
        }

        // Up bomb_count
        bomb_count += num_new_bombs;
    }
    public void RemoveBombs(int num_bombs_removed)
    {
        if (!GameController.instance.inGodMode())
        {
            // Check if bomb should be removed from alternate weapons list
            //f If after remove bombs there will be 0 bombs and not in god mode
            if ((bomb_count - num_bombs_removed <= 0) && GetBombs() != max_count)
            {
                PlayerControls playerControls = GetComponent<PlayerControls>();
                playerControls.RemoveWeaponFromList("bomb");
            }

            // Decrease bomb count
            bomb_count = bomb_count - num_bombs_removed;
        }
    }
    public int GetBombs()
    {
        if (GameController.instance.inGodMode()) return max_count;
        return bomb_count;
    }


}
