using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    [SerializeField] bool showHelpKey = false;
    [SerializeField] TurnOff objectThatIsOn;
    [SerializeField] ParticleSystem burnerfx;
    [SerializeField] ParticleSystem smokefx;
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
            // Debug.Log("Player triggered switch!");
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
        //Debug.Log("Switch Interaction!");
        if (isInteractable && triggerEntered) 
        {
            isInteractable = false;
            itemChild.gameObject.SetActive(false);
            //Debug.Log("You flipped the switch!");
            anim.SetBool("isFlipped", true);
            audioPlayer.PlaySwitchClip();
            if (objectThatIsOn != null) {
                objectThatIsOn.gameObject.SetActive(false);
            }
            if (burnerfx != null) 
            {
                burnerfx.Stop();
            }
            if (smokefx != null) 
            {
                smokefx.Stop();
            }
        } else 
        {
            Debug.Log("Switch is not interactable.");
        }
    }

    public void ForceInteract() 
    {
        if (isInteractable) 
        {
            isInteractable = false;
            anim.SetBool("isFlipped", true);
            audioPlayer.PlaySwitchClip();
            if (objectThatIsOn != null) {
                objectThatIsOn.gameObject.SetActive(false);
            }
            if (burnerfx != null) 
            {
                burnerfx.Stop();
            }
            if (smokefx != null) 
            {
                smokefx.Stop();
            }
        } 
    }

    public bool GetIsInteractable()
    {
        return isInteractable;
    }
}
