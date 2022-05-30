using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] float dissolveDelay = 1.5f;
    PortalManager pm;
    int numItemsRemaining;
    Animator anim;
    Player player;
    bool isDissolved = false;
    bool isDissolvable = false;

    AudioPlayer audioPlayer;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        pm = FindObjectOfType<PortalManager>();
        numItemsRemaining = pm.GetNumRemaining();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        numItemsRemaining = pm.GetNumRemaining();
        if (numItemsRemaining < 1 & !isDissolved & isDissolvable) 
        {
            StartCoroutine(Dissolve());
        }
    }

    IEnumerator Dissolve()
    {
        player.SetIsDisabled(true);
        isDissolved = true;
        yield return new WaitForSeconds(dissolveDelay);
        anim.SetBool("isDissolving", true);
        audioPlayer.PlayAhh1Clip();
        yield return new WaitForSeconds(dissolveDelay);
        player.SetIsDisabled(false);
    }

    public void SetIsDissolvable(bool b) 
    {
        isDissolvable = b;
    }

    public bool GetIsDissolved() 
    {
        return isDissolved;
    }
}
