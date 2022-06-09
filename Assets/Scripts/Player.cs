using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float baseMoveSpeed = 7.0f;
    [SerializeField] float baseJumpSpeed = 15.0f;
    float moveSpeed;
    float jumpSpeed;
    Vector2 moveInput;
    Rigidbody2D rb;
    BoxCollider2D footCollider;
    CapsuleCollider2D bodyCollider;
    IInteractable interactable;
    Animator anim;
    Chain greenChain;
    Chain blueChain;
    Chain redChain;
    int numChains;
    bool isDisabled = false;
    int stageNum = 1;
    float baseGravity;

    AudioPlayer audioPlayer;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        stageNum = SceneManager.GetActiveScene().buildIndex;
        baseGravity = rb.gravityScale;
        SetChains(stageNum);
        SetSpeeds();
    }

    void Update()
    {
        if (!isDisabled) {
            rb.gravityScale = baseGravity;
            SetSpeeds();
            Move();
            FlipSprite();
            Jump();
        }
        else 
        {
            Disable();
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) 
    {
        if (!footCollider.IsTouchingLayers(LayerMask.GetMask("Platforms"))) { return; }

        if (value.isPressed) 
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
            audioPlayer.PlayJumpingClip();
        }
    }

    void OnInteract()
    {
        if (interactable == null) { return; }
        interactable.Interact();
    }

    void OnPortal() 
    {
        InteractAllBags();
        InteractAllHandles();
        InteractAllSwitches();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<IInteractable>() != null) 
        {
            //Debug.Log("Setting interactable to: " + other.name);
            interactable = other.gameObject.GetComponent<IInteractable>();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.GetComponent<IInteractable>() != null) 
        {
            //Debug.Log("Unsetting interactable: " + other.name);
            interactable = null;
        }
    }

    void Move() 
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", playerHasHorizontalSpeed);

        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed) 
        {
            float currentLocalScaleX = Mathf.Abs(transform.localScale.x);
            float currentLocalScaleY = Mathf.Abs(transform.localScale.y);
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x) * currentLocalScaleX, currentLocalScaleY);
        }
    }

    void Jump()
    {
        // Play jumping animation
    }

    public void SetIsDisabled(bool b)
    {
        isDisabled = b;
        //Debug.Log("I'm disabled!");
    }

    public void SetSpeeds()
    {
        int nChains = GetNumChains();
        if (nChains == 3) 
        {
            moveSpeed = 2.5f; 
            jumpSpeed = 7.0f; 
        } else if (nChains == 2) 
        {
            moveSpeed = 3.0f; 
            jumpSpeed = 8.0f; 
        } else if (nChains == 1) 
        {
            moveSpeed = 5.0f; 
            jumpSpeed = 10.0f; 
        } else 
        {
            moveSpeed = baseMoveSpeed; 
            jumpSpeed = baseJumpSpeed; 
        }
    }

    int GetNumChains()
    {
        Chain[] chains = FindObjectsOfType<Chain>();
        int unDissolvedChains = 0;
        foreach (Chain ch in chains)
        {
            if (!ch.GetIsDissolved()) {
                unDissolvedChains += 1;
            }
        }
        return unDissolvedChains;
    }

    void SetChains(int stage)
    {
        Chain[] chains = FindObjectsOfType<Chain>();
        foreach (Chain ch in chains) 
        {
            if (ch.tag == "Green Chain") {
                greenChain = ch;
            }
            if (ch.tag == "Blue Chain") {
                blueChain = ch;
            }
            if (ch.tag == "Red Chain") {
                redChain = ch;
            }
        }

        if (stage == 1) 
        {
            greenChain.SetIsDissolvable(true);
        } 
        if (stage == 2)
        {
            Destroy(greenChain.gameObject);
            blueChain.SetIsDissolvable(true);
        }
        if (stage == 3)
        {
            Destroy(greenChain.gameObject);
            Destroy(blueChain.gameObject);
            redChain.SetIsDissolvable(true);
        } 
        if (stage > 3) 
        {
            Destroy(greenChain.gameObject);
            Destroy(blueChain.gameObject); 
            Destroy(redChain.gameObject); 
        }
    }

    void Disable() 
    {
        // Stop velocity and hold position
        rb.velocity = new Vector2(0f, 0f);
        rb.gravityScale = 0f;
    }

    void InteractAllBags()
    {
        Bag[] bags = FindObjectsOfType<Bag>();
        if (bags.Length > 0) 
        {
            foreach (Bag b in bags) 
            {
                b.ForceInteract();
            }
        }
    }

    void InteractAllHandles()
    {
        Handle[] handles = FindObjectsOfType<Handle>();
        if (handles.Length > 0) 
        {
            foreach (Handle h in handles) 
            {
                h.ForceInteract();
            }
        }
    }

    void InteractAllSwitches()
    {
        Switch[] switches = FindObjectsOfType<Switch>();
        if (switches.Length > 0) 
        {
            foreach (Switch s in switches) 
            {
                s.ForceInteract();
            }
        }
    }
    
}
