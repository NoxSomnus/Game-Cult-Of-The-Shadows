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
    private bool moveToWaypoint = false;
    public GameObject waypoint;
    private Vector3 waypointDistance;
    private CapsuleCollider2D capsuleCollider;
    private float waypointCronometer = 0;
    // Start is called before the first frame update
    void Start()
    {        
        ani = GetComponent<Animator>();
        rgb = ani.GetComponent<Rigidbody2D>();        
        lastShoot = 0f;
        follow = true;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        waypointDistance = waypoint.transform.position - transform.position;
        waypointDistance = new Vector3(waypointDistance.x, transform.position.y, waypointDistance.z);
        direction = target.transform.position - transform.position;
        range = Vector3.Distance(target.transform.position, transform.position);
        //mirarfeo();
        //ani.SetBool("Running", false);
        ani.SetBool("Attacking", false);
        //ani.SetBool("Die", false);
        if(follow)
            Comportamiento();
        if (HP.Health <= 0)
        {
            ani.SetBool("Attacking", false);
            ani.SetBool("Death", true);
            runSpeed = 0;
            rgb.Sleep();
            rgb.MovePosition(transform.position);
            capsuleCollider.enabled = false;
            follow = false;
        }
        projectileDirection = new Vector3(transform.position.x + 1, transform.position.y - 1.5f, transform.position.z);
        if (moveToWaypoint)
            MoveToWaypoint();        
    }

    public void Comportamiento()
    {
        if(range < 15 && Time.time > lastShoot + 2f) 
        {
            //Shoot();
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
            // Multiply the player's x local scale by -1.
            direction = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Shoot() 
    {
        GameObject shockwave = Instantiate(ProjectilePrefab, projectileDirection, Quaternion.identity);
        shockwave.GetComponent<Projectiles>().SetDirection(direction);
    }

    private void MoveToWaypoint() 
    {
        waypointCronometer -= 1 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, waypointDistance, runSpeed * Time.deltaTime);
        if (waypointCronometer < 0)
            moveToWaypoint = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            waypointCronometer = 2f;
            moveToWaypoint = true;
            ani.SetTrigger("Hit");
        }
    }

    /* public void mirarfeo()
     {
         // range = Vector3.Distance(target.transform.position, transform.position);
         range = target.transform.position.x - transform.position.x;
         if (follow && HP.Health > 0)
         {

             ani.SetBool("Running", true);
             //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, runSpeed * Time.deltaTime);

             // Mover solo en el eje X
             Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
             transform.position = Vector3.MoveTowards(transform.position, targetPosition, runSpeed * Time.deltaTime);
             if (range > 0)
             {
                 transform.localScale = new Vector3(1, 1, 1);
             }
             else
             {
                 transform.localScale = new Vector3(-1, 1, 1);

             }


         }
         if (Mathf.Abs(range) <= 1f)
         {
             ani.SetBool("Running", false);
             ani.SetBool("Walking", false);
             ani.SetBool("Attacking", true);
         }
         else
             ani.SetBool("Attacking", false);




         if (Mathf.Abs(range) < 3f)
         {
             follow = true;
         }
         else
         {
             follow = false;
         }


     }*/
}
