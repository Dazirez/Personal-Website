using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zelda : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag != "Player") return; 
        other.GetComponent<Rigidbody>().velocity = Vector2.zero; 
        StartCoroutine(endgame());
    }
    IEnumerator endgame() { 
        GameObject player =  GameObject.FindGameObjectWithTag("Player");
        MonoBehaviour[] comps = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = false;
        } 
        player.GetComponent<PlayerMovement>().disabled = true; 
        transform.position = transform.position + new Vector3(0, 1.0f, 0); 
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(1.0f);
        GetComponent<Animator>().SetBool("raise", true);


        AudioController.instance.play_clip(22);
        yield return new WaitForSeconds(3.0f);
        GameController.instance.switchScenes();
    }
}
