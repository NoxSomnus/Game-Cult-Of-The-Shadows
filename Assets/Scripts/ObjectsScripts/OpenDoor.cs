using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public int pulsedLevers = 0;
    public int leversRequired;
    CapsuleCollider2D capsuleCollider;
    Animator animator;
    public bool enable = false;
    public enabled enabledScript;
    // Update is called once per frame

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("Open", false);
        enabledScript = GetComponent<enabled>();
    }
    void Update()
    {
        openDoor();
    }

    public void openDoor() 
    {
        if (pulsedLevers == leversRequired && enabledScript.activao == false) 
        {
            enable= true;
            enabledScript.SetActivao();

        }
        else if(enabledScript.activao)
        {
            enable = true;

        }
        if (enable)
        {
            capsuleCollider.isTrigger = true;
            animator.SetBool("Open", true);
        }

    }
}
