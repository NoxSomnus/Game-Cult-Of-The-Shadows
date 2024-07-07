using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraMovement : MonoBehaviour
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
    bool isSprinting;

    //Dash
    bool canDash;
    bool canMove;
    public float dashSpeed;
    public float dashTime;

    //Combat
    public bool attacking;
    public int combo;
    public int elementalCombo;
    public bool onAir;

    public Parameters playerStats;

    public Animator animator;

    Rigidbody2D rb2d;
    ChangeCharactersManager characterManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attacking = false;
        combo = 0;
        originalRunSpeed = runSpeed;
        canDash = true;
        canMove = true;
        playerStats = GetComponent<Parameters>();
        rb2d = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
        characterManager = GetComponent<ChangeCharactersManager>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Combos();
        Sprint();
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        // Detectar el doble toque en las teclas A o D

        if (horizontalMove != 0)
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);

        if (Input.GetButtonDown("Jump") && !attacking)
        {
            onAir = true;
            jump = true;
            animator.SetBool("Sprint", false);
            StartCoroutine(JumpLoop());
        }
    }

    public void Start_ElementalCombo_Event()
    {
        attacking = false;
        characterManager.canSwitch = true;
        if (elementalCombo < 3)
        {
            elementalCombo++;
        }

    }

    public void Finish_ElementalAtK_Event()
    {

        attacking = false;
        characterManager.canSwitch = true;
        elementalCombo = 0;
    }

    public void Start_Combo_Event2()
    {
        attacking = false;
        characterManager.canSwitch = true;
        if (combo < 3)
        {
            combo++;
        }

    }

    public void Finish_AtK_Event2()
    {

        attacking = false;
        characterManager.canSwitch = true;
        combo = 0;
    }

    private void Sprint() 
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isSprinting = true;
        }

        if (attacking)
        {
            runSpeed = 0f;
            characterManager.canSwitch = false;
        }
        else
        {
            if (isSprinting)
            {
                runSpeed = originalRunSpeed * 1.5f;
                if (!jump)
                    animator.SetBool("Sprint", true);
            }
            else
                runSpeed = originalRunSpeed;
        }

        if (isSprinting && horizontalMove == 0)
        {
            isSprinting = false;
            animator.SetBool("Sprint", false);
        }
    }

    private void Combos()
    {
        if (Input.GetKeyDown(KeyCode.O) && !attacking)
        {
            if (!onAir)
            {
                attacking = true;
                animator.SetTrigger("Atk" + combo);
            }
            /*else
            {
                StartCoroutine(AirAtk());
            }*/
        }

        if (Input.GetKeyDown(KeyCode.P) && !attacking)
        {
            if (!onAir)
            {
                if (playerStats.mana >= 20) 
                {
                    attacking = true;
                    animator.SetTrigger("ElementalStorm" + elementalCombo);
                    playerStats.mana -= 20;
                }
            }
            /*else
            {
                StartCoroutine(AirAtk());
            }*/
        }



        /*if (Input.GetKeyDown(KeyCode.P) && !attacking && canDash)
        {
            if (onAir)
                StartCoroutine(HeavyAirAtk());
            else
            {
                if (playerStats.soul >= 50)
                {
                    StartCoroutine(HolySlash());
                    playerStats.soul -= 50;
                }

            }

            rb2d.gravityScale = 10f;
        }*/

        /*if (Input.GetKeyDown(KeyCode.F) && !attacking && canDash && (playerStats.soul >= 100))
        {
            playerStats.soul -= 100;
            StartCoroutine(LightCut());
        }

        if (InputManager.Instance.Shield() && canShield)
        {
            runSpeed = 0f;
            shieldUp = true;
            animator.SetBool("Shield", true);
        }
        else
        {
            shieldUp = false;
            animator.SetBool("Shield", false);
        }*/
    }

    private IEnumerator JumpLoop()
    {
        animator.SetBool("Jumping", true);
        //animator.SetBool("TouchGround", false);
        yield return new WaitForSeconds(0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform"))
        {
            rb2d.gravityScale = 5f;
            animator.SetBool("Jumping", false);
            animator.SetBool("Air", false);
            //animator.SetBool("HeavyAirAtk", false);
            animator.SetBool("TouchGround", true);
            onAir = false;
        }

        /*if (collision.collider.CompareTag("Enemy"))
        {
            if (collision.transform.position.x < transform.position.x)
                rb2d.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
            else
                rb2d.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
        }*/

        if (collision.collider.CompareTag("WindDoor"))
        {
            if (collision.transform.position.x < transform.position.x)
                rb2d.AddForce(new Vector2(knockbackForceX, 0), ForceMode2D.Force);
            else
                rb2d.AddForce(new Vector2(-knockbackForceX, 0), ForceMode2D.Force);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
        {
            animator.SetBool("Air", true);
            animator.SetBool("TouchGround", false);
        }
    }
}
