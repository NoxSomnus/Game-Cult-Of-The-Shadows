using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EsqueleticoBehaviour : MonoBehaviour
{
    public GameObject target;
    private Enemy enemyStats;
    private Animator animator;
    private Rigidbody2D rgd2;
    public Transform move;
    private bool canWalk = false;
    public float speed;
    public float atkspeed;
    private bool attacking;
    private bool direction;
    public float originalCronometer;
    private float cronometer;
    private float distance;
    public float atkRange;
    private EsqueleticoSFX sfx;
    private bool sfxActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player 1.1");
        animator = GetComponent<Animator>();
        rgd2 = GetComponent<Rigidbody2D>();
        enemyStats = GetComponent<Enemy>();
        sfx = GetComponent<EsqueleticoSFX>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            Patrolling();
            if (canWalk)
            {
                sfxActivated = false;
                transform.position = Vector3.MoveTowards(transform.position, move.position, speed * Time.deltaTime);
                sfx.Stopdibitybayadode();
            }
        }
        else 
        {
            if(!sfxActivated) 
            {
                sfxActivated = true;
                sfx.dibitybayadode();
            }
            if ((target.transform.position.x - transform.position.x) < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);

            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, atkspeed * Time.deltaTime);
        }

        distance = Vector2.Distance(target.transform.position, transform.position);

        if (distance < atkRange)
        {
            attacking = true;
            animator.SetBool("Attack", true);
        }
        else 
        {
            attacking = false;
            animator.SetBool("Attack", false);
        }

    }

    private void Patrolling()
    {
        cronometer -= 1 * Time.deltaTime;

        if (direction)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (cronometer <= 0f)
        {
            cronometer = originalCronometer;
            direction = !direction;
        }
    }

    public void SetAttackingTrue() 
    {
        attacking = true;
    }

    public void SetAttackingFalse() 
    {
        attacking = false;
    }

    public void MoveAnimation()
    {
        canWalk = true;
    }

    public void StopAnimation() 
    {
        canWalk = false;
    }
}
