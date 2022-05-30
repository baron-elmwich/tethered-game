using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour, IInteractable
{
    [SerializeField] bool showHelpKey = false;
    [SerializeField] ParticleSystem fx;
    bool triggerEntered = false;
    bool isInteractable = true;
    Transform itemChild;
    Animator anim;

    AudioPlayer audioPlayer;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        itemChild = transform.GetChild(0);
        itemChild.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            // Debug.Log("Player triggered handle!");
            triggerEntered = true;
            if (showHelpKey && isInteractable) 
            {
                itemChild.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        triggerEntered = false;
        itemChild.gameObject.SetActive(false);
    }

    public void Interact() 
    {
        if (isInteractable && triggerEntered) 
        {
            isInteractable = false;
            anim.SetBool("isTurning", true);
            audioPlayer.PlayHandleClip();
            // Debug.Log("You turned the handle!");
            if (fx != null) 
            {
                fx.Stop();
            }
        } else 
        {
            Debug.Log("Handle is not interactable.");
        }
    }

    public bool GetIsInteractable()
    {
        return isInteractable;
    }
}
