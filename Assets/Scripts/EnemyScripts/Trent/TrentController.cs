using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrentController : MonoBehaviour
{
    #region oldvariables
    private BoxCollider2D c2;
    private Rigidbody2D rbg;
    private Animator animator;
    private bool ispatrol = false;
    public Enemy healtController;
    private BoxCollider2D attackDetect;

    private BlinkEffect blink;
    private SpriteRenderer sprite;
    private bool IsPlayerInAttackRange;

    [SerializeField] private float patrolSpeed;
    [SerializeField] private float waitTime;
    public float distance;
    public GameObject target;
    [SerializeField] private bool canMove;
    #endregion

    public Enemy enemyStats;
    [SerializeField] private float lookAt;
    [SerializeField] private bool changeLook;
    [SerializeField] private float runSpeed;
    private float originalRunSpeed;
    public float cronometer;
    public bool direction;
    private bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player 1.1");
        originalRunSpeed = runSpeed;
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<Enemy>();
        canMove = true;
        changeLook = true;
        canAttack = true;
        sprite = GetComponent<SpriteRenderer>();
        blink = GetComponent<BlinkEffect>();

    }

    // Update is called once per frame

    private void Update()
    {

        if (enemyStats.Health <= 0)
        {
            animator.SetTrigger("Death");
            this.enabled = false;
        }
        distance = Vector2.Distance(target.transform.position, transform.position);

        target = GameObject.Find("Player 1.1");


        if (lookAt > 0 && changeLook)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
        if (changeLook && lookAt < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }


        if (distance <= 10)
        {
            lookAt = target.transform.position.x - transform.position.x;
            if (canMove)
            {
                Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, runSpeed * Time.deltaTime);
                animator.SetBool("Run", true);
            }
            if (distance <= 4f)
            {
                if (canMove)
                    StartCoroutine(Attack());
            }

        }
        else if (distance > 10)
        {
            Patrolling();
        }




    }

    private void Patrolling()
    {

        if (canMove)
        {
            animator.SetBool("Run", true);
        }
        cronometer -= 1 * Time.deltaTime;


        if (direction)
        {
            transform.position += Vector3.right * runSpeed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.position += Vector3.left * runSpeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //yield return new WaitForSeconds(3f);
        //animator.SetBool("Run", false);

        if (cronometer <= 0f)
        {
            cronometer = 3f;
            direction = !direction;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Weapon")
        {
            StartCoroutine(Damager());

        }
    }


    //Corrutina que interrumpe el patrullaje e inicia la animacion de atque
    //siempre que este el pLayer dentro del rango de ataque
    //private IEnumerator Attack()
    //{
    //    animator.SetBool("Attack", true);
    //    animator.SetBool("Run", false);
    //    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Espera a que termine la animación de ataque

    //    animator.SetBool("Attack", false);


    //}

    private IEnumerator Attack()
    {

        animator.SetBool("Run", false);
        animator.SetTrigger("Attack");

        canAttack = false;
        canMove = false;
        runSpeed = 0;
        changeLook = false;

        yield return new WaitForSeconds(2.5f);

        canMove = true;
        runSpeed = originalRunSpeed;
        changeLook = true;
        canAttack = true;
    }

    private IEnumerator Damager()
    {
        sprite.material = blink.blink;
        yield return new WaitForSeconds(0.5f);
        sprite.material = blink.original;
    }
}
