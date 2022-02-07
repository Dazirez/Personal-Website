using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Stunned(GameObject collidedObject)
    {
        //turn on invincibility for 2 seconds and stun for 0.5 seconds; 
        collidedObject.GetComponent<BaseMovement>().enabled = false;
        yield return new WaitForSeconds(2.5f);
        collidedObject.GetComponent<BaseMovement>().enabled = true;
    }
}
