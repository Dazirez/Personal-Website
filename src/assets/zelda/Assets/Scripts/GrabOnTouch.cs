using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabOnTouch : MonoBehaviour
{
    public bool grabbed = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MonoBehaviour[] comps = other.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in comps)
            {
                c.enabled = false;
            }
            other.GetComponent<SpriteRenderer>().enabled = true;
            other.gameObject.layer = 14;
            other.transform.position = transform.position;
            other.GetComponent<SpriteRenderer>().sortingOrder = 1;
            animator.SetBool("grabbed", true);
            grabbed = true;
        }
    }
}
