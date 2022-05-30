using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    Animator anim;

    void Start() 
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") {
            anim.SetBool("playerEnteredRegion", true);
            Debug.Log("Player entered region");
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        anim.SetBool("playerEnteredRegion", false);
        Debug.Log("Player exited region");
    }
}
