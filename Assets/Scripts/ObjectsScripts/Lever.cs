using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public OpenDoor Door;    
    CapsuleCollider2D capsuleCollider;
    Animator animator;
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("Enabled", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon")) 
        {
            Door.pulsedLevers++;
            capsuleCollider.enabled = false;
            animator.SetBool("Enabled", true);
        }
    }
}
