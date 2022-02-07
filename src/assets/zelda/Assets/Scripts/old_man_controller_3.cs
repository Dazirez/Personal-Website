using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class old_man_controller_3 : MonoBehaviour
{
    public int phase = 1; 
    HasHealth health; 
    Animator animator; 
    Rigidbody rb; 
    BaseMovement movement; 
    GoriyaAttack attack1; 
    AquamentusAttack attack2;
    private int layer = 1;  
    bool controls = false; 
    
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<HasHealth>(); 
        animator = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody>(); 
        movement = GetComponent<BaseMovement>(); 
        attack1 = GetComponent<GoriyaAttack>(); 
        attack2 = GetComponent<AquamentusAttack>(); 

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("health is: " + health.GetHealth() + " phase is: " + phase); 
        if(health.GetHealth() == health.max_health && phase == 1) { 
            StartCoroutine(spanwNext(phase)); 
            phase++; 
        }
        else if(health.GetHealth() <= 16 && phase == 2) { 
            Debug.Log("starting second coroutine"); 
            StartCoroutine(spanwNext(phase)); 
            phase++; 
        }
         else if(health.GetHealth() <= 12 && phase == 3) { 
            Debug.Log("starting second coroutine"); 
            StartCoroutine(spanwNext(phase)); 
            phase++; 
        }
         else if(health.GetHealth() <= 8 && phase == 4) { 
            Debug.Log("starting second coroutine"); 
            StartCoroutine(spawnAquamentus()); 
            phase += 2; 
        }
        else if(health.GetHealth() <= 4 && phase == 6) { 
            Debug.Log("starting second coroutine"); 
            StartCoroutine(upgrade()); 
            phase += 1; 
        }
    }
    IEnumerator upgrade() { 
        ToggleControls(); 
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Pause();
        animator.SetBool("attacking", true); 
        yield return new WaitForSeconds(3.0f); 

        GetComponent<KeeseMovement>().max_speed *= 2; 
        GetComponent<SpriteRenderer>().color = new Color (1, 0, 0, 1); 
        AudioController.instance.play_clip(21); 
        yield return new WaitForSeconds(1.0f); 
        ToggleControls(); 
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
        animator.SetBool("attacking", false); 
    }
    IEnumerator spawnAquamentus() { 
        ToggleControls(); 
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Pause();
        animator.SetBool("attacking", true); 
        yield return new WaitForSeconds(3.0f); 
        transform.parent.gameObject.transform.GetChild(4).gameObject.SetActive(true);
        AudioController.instance.play_clip(21); 
        yield return new WaitForSeconds(2.0f); 
        transform.parent.gameObject.transform.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f); 
        ToggleControls(); 
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
        animator.SetBool("attacking", false); 
    }
    
    IEnumerator spanwNext(int curr) { 
        ToggleControls(); 
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Pause();
        animator.SetBool("attacking", true); 
        yield return new WaitForSeconds(3.0f); 
        transform.parent.gameObject.transform.GetChild(curr).gameObject.SetActive(true);
        AudioController.instance.play_clip(21); 
        yield return new WaitForSeconds(1.0f); 

        ToggleControls(); 
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Play();
        animator.SetBool("attacking", false); 
    }
    private void ToggleControls() { 
        Debug.Log("controls = " + controls); 
        TogglePlayerControls();
        ToggleKeeseControls();
        ToggleBlademasterControls(); 

        health.setInvincible(!controls); 
        rb.velocity = Vector2.zero; 
        movement.enabled = controls; 
        attack1.enabled = controls; 
        attack2.enabled = controls; 
        controls = !controls; 
        layer *= -1; 
    }
    private void TogglePlayerControls() { 
        GameObject player =  GameObject.FindGameObjectWithTag("Player");
        MonoBehaviour[] comps = player.GetComponents<MonoBehaviour>();
        player.GetComponent<HasHealth>().setInvincible(!controls); 
        player.layer += layer; 
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = controls;
        } 
        player.GetComponent<PlayerMovement>().cantmove = !controls;         
    }
    private void ToggleKeeseControls() { 
        GameObject[] keeses = GameObject.FindGameObjectsWithTag("keese");
         foreach (GameObject keese in keeses)
        {
            keese.GetComponent<BaseMovement>().enabled = controls; 
        }
    }
    private void ToggleBlademasterControls() { 
        GameObject[] blademasters = GameObject.FindGameObjectsWithTag("blademaster");
         foreach (GameObject blademaster in blademasters)
        {
            blademaster.GetComponent<Rigidbody>().velocity = Vector2.zero; 
            blademaster.GetComponent<BaseMovement>().enabled = controls; 
        }
    }
    void OnDestroy() { 
        GameObject[] keeses = GameObject.FindGameObjectsWithTag("keese");
        foreach (GameObject keese in keeses)
        {
            Destroy(keese);  
        }
        GameObject[] blademasters = GameObject.FindGameObjectsWithTag("blademaster");
        foreach (GameObject blademaster in blademasters)
        {
            Destroy(blademaster);  
        }
        GameObject[] aquamentuses = GameObject.FindGameObjectsWithTag("aquamentus");
        foreach (GameObject aquamentus in aquamentuses)
        {
            Destroy(aquamentus);  
        }
        transform.parent.gameObject.transform.GetChild(6).gameObject.SetActive(true);


    }
}
