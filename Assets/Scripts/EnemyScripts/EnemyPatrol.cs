using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BEnemyPatrol : MonoBehaviour
{

    private bool ispatrol = true;
    private Rigidbody2D rbg;
    private Animator animator;
    private BoxCollider2D attackDetect;
    public float currentHp;
    [SerializeField] private Enemy hp;
    [SerializeField] private float waitTime;
    [SerializeField] private float patrolSpeed;

    void Start()
    {
        currentHp = hp.Health;
        rbg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackDetect = transform.GetChild(1).GetComponent<BoxCollider2D>(); // Obtener el collider del objeto hijo "AttackDetec"
    }

    private void Update()
    {
        if (currentHp >= hp.Health)
        {
            animator.SetBool("Hit", true);
            currentHp= hp.Health;
        }
        if(currentHp < 0)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Hit", false);
            animator.SetBool("Die", true);
            patrolSpeed = 0;
            rbg.Sleep();
            rbg.MovePosition(transform.position);
        }
        if (ispatrol)
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

        if (!IsPlayerInAttackRange())// se supone que espere pero no funca
        {
            Debug.Log("hacercate viejo");
            yield return new WaitForSeconds(5f);
        }

    }



}


