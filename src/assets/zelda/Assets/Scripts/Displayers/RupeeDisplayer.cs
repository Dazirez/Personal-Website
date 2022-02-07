using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class RupeeDisplayer : MonoBehaviour
{
    public Inventory inventory; 
    TMP_Text text_component; 
    // Start is called before the first frame update
    void Start()
    {
        text_component = GetComponent<TMP_Text> (); 
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory != null && text_component != null) { 
            text_component.text = "Rupees: " + inventory.GetRupees().ToString(); 
        }
    }
}


/*
"- Remember AudioSource.PlayClipAtPoint(). Audio clips work well as public variables (you may then drag them into a script via the inspector).
- AudioManager gameobjects / components are great candidates for a singleton pattern.
- Use sounds-resource.com for sound effects (https://www.sounds-resource.com/nes/legendofzelda/sound/4590/)
- For music: vgmpf.com/Wiki/index.php/The_Legend_of_Zelda_(NES)"
*/
