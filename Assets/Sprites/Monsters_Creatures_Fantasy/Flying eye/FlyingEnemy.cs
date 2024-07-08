using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public bool chase = false;
    public Transform startingPosition;
    private Animator animator;
    private Enemy enemyStats;
    private Rigidbody2D rgd2;
    public float atkRange;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<Enemy>();
        rgd2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) 
        {
            return;
        }

        if (chase)
        {
            Chase();
            FlipToPlayer();
        }
        else 
        {
            ReturnStartPosition();
            FlipToStartingPosition();
        }

        if (enemyStats.Health <= 0) 
        {
            chase = false;
            rgd2.gravityScale = 3f;
            this.enabled = false;
        }

    }

    private void Chase() 
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.transform.position) < atkRange) 
        {
            animator.SetTrigger("Attack");
        }
    }

    private void ReturnStartPosition() 
    {
        transform.position = Vector3.MoveTowards(transform.position, startingPosition.position, speed * Time.deltaTime);
    }

    private void FlipToPlayer() 
    {
        if(transform.position.x < player.transform.position.x) 
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FlipToStartingPosition()
    {
        if (transform.position.x < startingPosition.transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
