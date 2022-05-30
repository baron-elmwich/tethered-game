using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour, IInteractable
{
    bool triggerEntered = false;
    bool wasCollected = false;
    Transform itemChild;
    [SerializeField] bool showHelpKey = false;

    AudioPlayer audioPlayer;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start() 
    {
        itemChild = transform.GetChild(0);
        itemChild.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            triggerEntered = true;
            if (showHelpKey) 
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
        if (!wasCollected && triggerEntered) 
        {
            wasCollected = true;
            //Debug.Log("You picked up the bag!");
            audioPlayer.PlayBagClip();
            Destroy(gameObject);
        }
    }
}
