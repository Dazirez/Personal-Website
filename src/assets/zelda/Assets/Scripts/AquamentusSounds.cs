using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamentusSounds : MonoBehaviour
{
    public float roar_timer = 10.0f; 
    private float timer; 

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) { 
            timer = roar_timer;
            AudioController.instance.play_aquamentus_roar();
        }
    }
}
