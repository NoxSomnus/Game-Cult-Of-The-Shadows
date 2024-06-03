using System.Collections;
using UnityEngine;

public class BushController : MonoBehaviour
{
    private BoxCollider2D c2;
    private bool ispatrol = true;
    private Rigidbody2D rbg;
    private Animator animator;
    private BoxCollider2D attackDetect;
    public Enemy healtController;



    [SerializeField] private float waitTime;
    [SerializeField] private float patrolSpeed;


    void Start()
    {
        rbg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackDetect = transform.GetChild(1).GetComponent<BoxCollider2D>(); // Obtener el collider del objeto hijo "AttackDetec"
        healtController = GetComponent<Enemy>();
        c2 = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (healtController.Health <= 0)
        {

            animator.SetBool("Run", false);
            animator.SetBool("Hit", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Die", true);
            rbg.bodyType = RigidbodyType2D.Static;
            attackDetect.enabled = false;
            c2.enabled = false;
            Debug.Log("2222");
            this.enabled = false;
            


        }

        if (ispatrol )
        {
            animator.SetBool("Run", true);
            animator.SetBool("Hit", false);
            animator.SetBool("Attack", false);
            

            rbg.velocity = new Vector2(patrolSpeed, rbg.velocity.y);

            if (IsPlayerInAttackRange())
            {
                ispatrol = false;
                StartCoroutine(Attack());
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    

    // Revisa que haya colicion con Ground para seguir, si no , se devuelve
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            patrolSpeed *= -1;
            transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
        }
        if (collision.gameObject.tag == "Weapon")
        {
            animator.SetBool("Hit", true);
            StartCoroutine(TakeDamage());
        }
    }

    // revisa si el Player esta dentro del collider que funciona como rango de ataque "Attack Detect"
    private bool IsPlayerInAttackRange()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackDetect.bounds.center, attackDetect.bounds.size, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    //Corrutina que interrumpe el patrullaje e inicia la animacion de atque
    //siempre que este el pLayer dentro del rango de ataque
    private IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        animator.SetBool("Run", false);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Espera a que termine la animación de ataque
        
        ispatrol = true;
        animator.SetBool("Attack", false);


    }

    private IEnumerator TakeDamage()
    {
        Debug.Log("0000");

       
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.SetBool("Hit", false);
        animator.SetBool("Run", true);

    }
    
}


