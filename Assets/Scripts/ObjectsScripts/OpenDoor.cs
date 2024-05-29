using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public int pulsedLevers = 0;
    public int leversRequired;
    CapsuleCollider2D capsuleCollider;
    Animator animator;
    // Update is called once per frame

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("Open", false);
    }
    void Update()
    {
        openDoor();
    }

    public void openDoor() 
    {
        if (pulsedLevers == leversRequired) 
        {
            capsuleCollider.isTrigger = true;
            animator.SetBool("Open", true);
        }

    }
}
