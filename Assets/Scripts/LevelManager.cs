using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1.5f;
    int currentSceneIndex;
    int numScenes;

    AudioPlayer audioPlayer;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        numScenes = SceneManager.sceneCountInBuildSettings;
        if (currentSceneIndex == 0) 
        {
            audioPlayer.PlayGameMusic();
        }
        if (currentSceneIndex == numScenes - 1) 
        {
            audioPlayer.PlayEndMusic();
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadWithAudio());
    }

    IEnumerator LoadWithAudio()
    {
        int nextScene = currentSceneIndex + 1;
        if (nextScene >= numScenes) 
        {
            nextScene = 0;
        }
        
        audioPlayer.PlayPortalEnterClip();
        yield return new WaitForSeconds(levelLoadDelay);
        SceneManager.LoadScene(nextScene);
    }

    public void Quit() 
    {
        // Debug.Log("Quitting the game.");
        Application.Quit();
    }
}
