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
    bool isSprinting;


    //Combat
    public bool attacking;
    public int combo;
    public bool onAir;
    bool shieldUp;
    public bool canShield;
    public float lightCutDash;

    //Dash
    bool canDash;
    bool canMove;
    public float dashSpeed;
    public float dashTime;

    //Parameters
    public Parameters playerStats;

    public Animator animator;

    Rigidbody2D rb2d;

    ChangeCharactersManager characterManager;


    // Start is called before the first frame update

    //cosas de la camar
    //public float _fallSpeedYDampingChangeThreshold;

    void Start()
    {
        characterManager = GetComponent<ChangeCharactersManager>();
        animator = GetComponent<Animator>();
        attacking = false;
        combo = 0;
        canShield = true;
        originalRunSpeed = runSpeed;
        canDash = true;
        canMove = true;
        playerStats = GetComponent<Parameters>();
        rb2d = GetComponent<Rigidbody2D>();

        //camara
       // _fallSpeedYDampingChangeThreshold = CameraManager.instance._fallSpeedYDampingChangeThreshold;

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
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        // Detectar el doble toque en las teclas A o D

        if (horizontalMove != 0)
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);


        if (Input.GetButtonDown("Jump") && !shieldUp && !attacking && canMove)
        {
            onAir = true;
            jump = true;
            animator.SetBool("Sprint", false);
            StartCoroutine(JumpLoop());
        }



        if (Input.GetKeyDown(KeyCode.S))
        {
            rb2d.gravityScale = 10f;
        }

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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !attacking && !shieldUp)
        {
            StartCoroutine(Dash());
            animator.SetTrigger("Dash");

        }

        if (Input.GetKeyDown(KeyCode.H) && !attacking && (playerStats.soul > 20)) 
        {
            animator.Play("SwordBuff");
            canMove = false;
        }

        /*// camara
        //if we are falling past a certain speed threshold
        if (rb2d.velocity.y < _fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerfalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }
        //if we are standing still or moving up
        if (rb2d.velocity.y >= 0f && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerfalling)
        {
            //reset so it can be called again
            CameraManager.instance.LerpedFromPlayerfalling = false;

            CameraManager.instance.LerpYDamping(false);

        }*/

    }

    public void SetCanMoveTrue() 
    {
        canMove = true;
    }

    private void Start_Combo_Event()
    {
        attacking = false;
        characterManager.canSwitch = true;
        if (combo < 3)
        {
            combo++;
        }

    }

    private void Finish_AtK_Event()
    {
        attacking = false;
        characterManager.canSwitch = true;
        combo = 0;
    }

    private void Finish_Air_Atk()
    {
        attacking = false;
    }

    public void Heal() 
    {
        playerStats.health = playerStats.health + 20;
        playerStats.soul = playerStats.soul - 20;
        canMove = true;
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
            else
            {
                StartCoroutine(AirAtk());
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && !attacking && canDash)
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
        }

        if (Input.GetKeyDown(KeyCode.F) && !attacking && canDash && (playerStats.soul >= 100))
        {
            playerStats.soul -= 100;
            StartCoroutine(LightCut());
        }

        if (InputManager.Instance.Shield() && canShield)
        {
            runSpeed = 0f;
            shieldUp = true;
            animator.SetBool("Shield", true);
            characterManager.canSwitch = false;
        }
        else
        {
            shieldUp = false;
            animator.SetBool("Shield", false);
            characterManager.canSwitch = true;
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        canMove = false;
        combo = 0;
        rb2d.velocity = new Vector3(dashSpeed * transform.localScale.x, 0, 0);
        gameObject.layer = 6; //Enemy
        characterManager.canSwitch = false;
        yield return new WaitForSeconds(dashTime);

        canMove = true;
        canDash = true;
        gameObject.tag = "Player";
        gameObject.layer = 7;
        characterManager.canSwitch = true;
    }

    private IEnumerator HeavyAirAtk()
    {
        attacking = true;
        animator.SetBool("HeavyAirAtk", true);
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }

    private IEnumerator HolySlash()
    {
        attacking = true;
        animator.SetTrigger("HolySlash");
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

    private IEnumerator LightCut()
    {
        attacking = true;
        animator.SetTrigger("LightCut");
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    public void LightCutMove()
    {
        attacking = false;
        rb2d.velocity = new Vector3(lightCutDash * transform.localScale.x, 0, 0);
    }

    private IEnumerator AirAtk()
    {
        animator.SetBool("AirAtk", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("AirAtk", false);
    }

    private IEnumerator JumpLoop()
    {
        animator.SetBool("Jumping", true);
        animator.SetBool("TouchGround", false);
        yield return new WaitForSeconds(0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform"))
        {
            rb2d.gravityScale = 5f;
            animator.SetBool("Jumping", false);
            animator.SetBool("HeavyAirAtk", false);
            animator.SetBool("TouchGround", true);
            onAir = false;
            animator.SetBool("Air", false);
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
        if (collision.collider.tag == "Ground" || collision.collider.CompareTag("Platform")) 
        {
            animator.SetBool("Air", true);
            animator.SetBool("TouchGround", false);
        }
    }
}
