using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] AudioClip gameMusicClip;
    //[SerializeField] [Range(0f, 1f)] float gameMusicVolume = 0.25f;
    [SerializeField] AudioClip endMusicClip;
    //[SerializeField] [Range(0f, 1f)] float endMusicVolume = 0.25f;

    [Header("Jumping")] 
    [SerializeField] AudioClip jumpingClip;
    [SerializeField] [Range(0f, 1f)] float jumpingVolume = 0.5f;

    [Header("Heavenly Sounds")]
    [SerializeField] AudioClip ahh1Clip;
    [SerializeField] [Range(0f, 1f)] float ahh1Volume = 0.5f;

    [Header("Interactables")] 
    [SerializeField] AudioClip bagClip;
    [SerializeField] [Range(0f, 1f)] float bagVolume = 0.5f;
    [SerializeField] AudioClip handleClip;
    [SerializeField] [Range(0f, 1f)] float handleVolume = 0.5f;
    [SerializeField] AudioClip switchClip;
    [SerializeField] [Range(0f, 1f)] float switchVolume = 0.5f;
    [SerializeField] AudioClip portalEnterClip;
    [SerializeField] [Range(0f, 1f)] float portalEnterVolume = 0.5f;

    static AudioPlayer instance;
    AudioSource audioSource; // for music

    void Awake() 
    {
        ManageSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void PlayAudioClip(AudioClip aClip, float aVolume) 
    {
        if (aClip != null)
        {
            AudioSource.PlayClipAtPoint(aClip, Camera.main.transform.position, aVolume);
        }
    }

    public void PlayGameMusic()
    {
        instance.audioSource.Stop();
        instance.audioSource.clip = instance.gameMusicClip;
        instance.audioSource.Play();
    }

    public void PlayEndMusic()
    {
        instance.audioSource.Stop();
        instance.audioSource.clip = instance.endMusicClip;
        instance.audioSource.Play();
    }

    public void PlayJumpingClip()
    {
        PlayAudioClip(jumpingClip, jumpingVolume);
    }

    public void PlayBagClip()
    {
        PlayAudioClip(bagClip, bagVolume);
    }

    public void PlayHandleClip()
    {
        PlayAudioClip(handleClip, handleVolume);
    }

    public void PlaySwitchClip()
    {
        PlayAudioClip(switchClip, switchVolume);
    }

    public void PlayPortalEnterClip()
    {
        PlayAudioClip(portalEnterClip, portalEnterVolume);
    }

    public void PlayAhh1Clip()
    {
        PlayAudioClip(ahh1Clip, ahh1Volume);
    }
}
