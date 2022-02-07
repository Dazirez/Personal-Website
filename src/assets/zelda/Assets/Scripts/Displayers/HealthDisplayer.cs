using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class HealthDisplayer : MonoBehaviour
{
    public HasHealth player_health; 
    TMP_Text text_component; 
    // Start is called before the first frame update
    void Start()
    {
        text_component = GetComponent<TMP_Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(player_health != null) { 
            text_component.text = "Health: " + player_health.GetHealth().ToString(); 
        }
    }
}
