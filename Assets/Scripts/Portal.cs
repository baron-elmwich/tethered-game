using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField] float levelLoadDelay = 1.5f;
    [SerializeField] bool showHelpKey = false;
    bool triggerEntered = false;
    int currentSceneIndex;
    Transform itemChild;

    AudioPlayer audioPlayer;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        itemChild = transform.GetChild(0);
        itemChild.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            // Debug.Log("Player entered portal");
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
        if (triggerEntered)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        // Debug.Log("Portal calling LoadNextScene");
        int nextScene = currentSceneIndex + 1;
        audioPlayer.PlayPortalEnterClip();
        yield return new WaitForSeconds(levelLoadDelay);
        SceneManager.LoadScene(nextScene);
    }

    public int GetSceneIndex() 
    {
        return currentSceneIndex;
    }
}
