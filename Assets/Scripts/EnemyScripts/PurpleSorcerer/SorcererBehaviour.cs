using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SorcererBehaviour : MonoBehaviour
{
    public float runSpeed;
    public GameObject ProjectilePrefab;
    private float lastShoot;
    public Animator ani;
    public GameObject target;
    private Vector3 direction;
    private Vector3 projectileDirection;
    public Enemy HP;
    bool follow;
    public float range;    
    private Rigidbody2D rgb;
    private CapsuleCollider2D capsuleCollider;
    private SorcererMoveWaypoint moveToWaypoint;
    private float waypointCronometer = 0;
    // Start is called before the first frame update
    void Start()
    {        
        ani = GetComponent<Animator>();
        rgb = ani.GetComponent<Rigidbody2D>();        
        lastShoot = 0f;
        moveToWaypoint = GetComponent<SorcererMoveWaypoint>();
        follow = true;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        target = GameObject.Find("Player 1.1");
    }

    // Update is called once per frame
    void Update()
    {
        direction = target.transform.position - transform.position;
        range = Vector3.Distance(target.transform.position, transform.position);
        ani.SetBool("Attacking", false);
        //ani.SetBool("Die", false);
        if(follow)
            Comportamiento();
        if (HP.Health <= 0)
        {
            moveToWaypoint.enabled = false;
            ani.SetBool("Attacking", false);
            ani.SetBool("Death", true);
            rgb.Sleep();
            rgb.MovePosition(transform.position);
            capsuleCollider.enabled = false;
            follow = false;
        }
        projectileDirection = new Vector3(transform.position.x + 1, transform.position.y - 1.5f, transform.position.z);       
    }

    public void Comportamiento()
    {
        if(range < 15 && Time.time > lastShoot + 2f) 
        {
            ani.SetBool("Attacking",true);
            lastShoot = Time.time;           
        }
        if (direction.x >= 0.0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            direction = Vector3.right;
        }
        else 
        {
            direction = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Shoot() 
    {
        GameObject shockwave = Instantiate(ProjectilePrefab, projectileDirection, Quaternion.identity);
        shockwave.GetComponent<Projectiles>().SetDirection(direction);
    }
}
