using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Movement
    public CharacterController2D controller;
    public float runSpeed;
    private float originalRunSpeed;
    private float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public float knockbackForceX;
    public float knockbackForceY;

    //Attack
    bool attacking;
    int combo;
    //Dash
    bool canDash;
    bool canMove;
    public float dashSpeed;
    public float dashTime;

    Animator animator;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attacking = false;
        combo = 0;
        originalRunSpeed = runSpeed;
        canDash = true;
        canMove = true;
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (canMove) 
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        
        jump = false;
    }
    // Update is called once per frame
    void Update()
    {
        Combos();
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        // Detectar el doble toque en las teclas A o D


        if (horizontalMove != 0)         
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("TouchGround", false);
            animator.SetTrigger("Jumping");
        }

        if (Input.GetKeyDown(KeyCode.S)) 
        {
            rb2d.gravityScale = 5f;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Atk" + combo))
            runSpeed = 0f;
        else
            runSpeed = 60f;
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            //animator.SetBool("Dash", true);
        }

    }

    private void Start_Combo_Event() 
    {
        attacking = false;
        if (combo < 3) 
        {
            combo++;
        }

    }

    private void Finish_AtK_Event() 
    {
        attacking = false;
        combo = 0;
    }

    private void Combos() 
    {
        if (Input.GetKeyDown(KeyCode.O) && !attacking)
        {
            attacking = true;
            animator.SetTrigger("Atk" + combo);
        }
        
    }
    private IEnumerator Dash()
    {
        canDash = false;
        canMove = false;

        rb2d.velocity = new Vector2(dashSpeed * transform.localScale.x, 0);
        gameObject.tag = "Enemy";
        gameObject.layer = 6;
        yield return new WaitForSeconds(dashTime);

        canMove = true;
        canDash = true;
        gameObject.tag = "Player";
        gameObject.layer = 7;
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground") 
        {
            rb2d.gravityScale = 1f;
            animator.SetBool("TouchGround", true);
        }
    }
}