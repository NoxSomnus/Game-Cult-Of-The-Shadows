using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public int health = 100;
    public float fury = 100;
    public bool activedfury;
    public CharacterController2D controller;
    private Rigidbody2D rb;
    public GameObject BlackShockwavePrefab;
    public GameObject BlackSwordPrefab;
    public GameObject Aura;
    public float runSpeed = 40f;
    private Vector3 direction;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool isGrounded;
    Animator animator;
    private float LastShockwave = 0f;
    public RuntimeAnimatorController BlueArmor;
    public RuntimeAnimatorController RedArmor;    
    public float dashSpeed = 45f;
    public float receiveHitSpeed = 20f;
    public float receiveHitTime;
    public float dashTime;
    private bool canDash = true;
    private bool canMove = true;
    public SoundEffectsManager soundEffectsManager;
    //NUEVO
    public FuryBar furyBar;
    public HealthBar healthBar;

    void Start()
    {
        animator = GetComponent<Animator>();
        direction = Vector3.right;
        animator.runtimeAnimatorController = BlueArmor;
        rb = GetComponent<Rigidbody2D>();
        activedfury = false;
        isGrounded = false;
        //soundEffectsManager.GetComponent<SoundEffectsManager>();
        //NUEVO, ESTE PARAMETRO ES EL MAX DEL SLIDER DE LA FURIA
        furyBar.SetMaxFury(100f);
        healthBar.SetMaxHealth(health);
    }


    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (horizontalMove != 0)
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            isGrounded = false;
            animator.SetBool("Jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.O))
            animator.SetBool("Attacking", true);
        else
            animator.SetBool("Attacking", false);

        if (Input.GetKeyDown(KeyCode.P) && Time.time > LastShockwave + 0.9f && canDash)
        {
            animator.SetBool("Shockwave", true);
            if(activedfury)
                ShootShockwave();
            LastShockwave = Time.time;
        }
        else
            animator.SetBool("Shockwave", false);
        //direccion del shockwave
        if (horizontalMove > 0)
            direction = Vector3.right;
        if (horizontalMove < 0)
            direction = Vector3.left;



        if (Input.GetKeyDown(KeyCode.F) && !activedfury)
        {
            activedfury = true;
            Aura.SetActive(true);
            soundEffectsManager.AuraTransition();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) 
        {
            StartCoroutine(Dash());
            animator.SetBool("Dash", true);
        }
            
        else
            animator.SetBool("Dash", false);


        //Comprueba que la animacion de atk se este haciendo y no permite mover al jugador mientras se ejecuta
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || 
            animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAtk") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("AttackRed") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAtkRed"))
            runSpeed = 0.0f;
        else
            runSpeed = 10f; //puede volver a moverse el jugador al terminar la animacion

        /*if (Input.GetButtonDown("Down")) ESTO DE AQUI ES PARA QUE SE AGACHE
		{
			crouch = true;
		} else if (Input.GetButtonUp("Down"))
		{
			crouch = false;
		}*/


    }

    void FixedUpdate()
    {
        // Move our character
        if(canMove)
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);

        jump = false;
        if (activedfury)
        {
            SetRedArmorAnimator();
            fury -= 10f * Time.deltaTime;

            //NUEVO
            furyBar.SetFury(fury);           
        }
        else        
            SetBlueArmorAnimator();      
            
        SetLimitsInIntVariables();
    }

    public void Hit(int dmg)
    {
        health -= dmg;
        healthBar.SetHealth(health);
        animator.SetTrigger("Hit");
        soundEffectsManager.HitClip();
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator Dash() 
    {
        canDash = false;
        canMove = false;

        rb.velocity = new Vector2(dashSpeed * transform.localScale.x, 0);

        yield return new WaitForSeconds(dashTime);

        canMove = true;
        canDash = true;
    }

    private void SetRedArmorAnimator()
    {
        animator.runtimeAnimatorController = RedArmor;
    }
    private void SetBlueArmorAnimator()
    {
        animator.runtimeAnimatorController = BlueArmor;
    }

    private void ShootShockwave()
    {
        if(isGrounded) 
        {
            GameObject shockwave = Instantiate(BlackShockwavePrefab, transform.position + direction * 0.8f, Quaternion.identity);
            shockwave.GetComponent<Shockwave>().SetDirection(direction);
        }
        else 
        {
            GameObject shockwave = Instantiate(BlackSwordPrefab, transform.position + direction * 0.8f, Quaternion.identity);
            shockwave.GetComponent<Shockwave>().SetDirection(direction);
        }
        fury -= 10f;    }

    private void SetLimitsInIntVariables()
    {
        if (fury < 0)
        {
            fury = 0;
            activedfury = false;
        }

        if (fury > 100)
            fury = 100;
    }

    public void RestoreFuryEvent(float furyObtained) 
    {
        fury += furyObtained;
        //NUEVO
        furyBar.SetFury(fury);
    }

    public void RestoreHealth(int healthHealed) 
    {
        print("restauro vida");
        health += healthHealed;
        healthBar.SetHealth(health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            animator.SetBool("Jumping", false);
            isGrounded = true;
        }
    }
}
