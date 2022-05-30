using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    [SerializeField] TurnOff objectThatIsOn;
    [SerializeField] ParticleSystem burnerfx;
    [SerializeField] ParticleSystem smokefx;
    bool triggerEntered = false;
    bool isInteractable = true;
    Animator anim;

    AudioPlayer audioPlayer;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start() 
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            // Debug.Log("Player triggered switch!");
            triggerEntered = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        triggerEntered = false;
    }

    public void Interact() 
    {
        //Debug.Log("Switch Interaction!");
        if (isInteractable && triggerEntered) 
        {
            isInteractable = false;
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

    public bool GetIsInteractable()
    {
        return isInteractable;
    }
}
