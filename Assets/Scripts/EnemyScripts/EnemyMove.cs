using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float cronometer;
    public Animator ani;
    public GameObject target;
    public bool direction;
    public Enemy HP;
    public float range;
    public bool follow;
    public Rigidbody2D rgb;
    
    // Start is called before the first frame update
    void Start()
    {
        cronometer = 3f;
        ani = GetComponent<Animator>();
        rgb=ani.GetComponent<Rigidbody2D>();
        follow = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        mirarfeo();
        if (follow == false  )
        {
            ani.SetBool("Running", false);
            ani.SetBool("Attacking", false);
            //ani.SetBool("Die", false);
            ani.SetBool("Walking", true);
            Comportamiento();
        }
        if(HP.Health <=0  )
        {
            ani.SetBool("Running", false);
            ani.SetBool("Attacking", false);
            ani.SetBool("Walking", false);
            ani.SetBool("Die", true);
            runSpeed = 0;
            walkSpeed = 0;
            rgb.Sleep();
            rgb.MovePosition(transform.position);
        }
        
        
    }

    public void Comportamiento()
    {
       
        cronometer -= 1 * Time.deltaTime;
        

      
        if (direction)
        {
            transform.position += Vector3.right * walkSpeed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.position += Vector3.left * walkSpeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (cronometer <= 0f)
        { 
            cronometer = 3f;
            direction = !direction;
        }

        
    }
    public void mirarfeo()
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
        if (Mathf.Abs(range) <=1f)
        {
            ani.SetBool("Running", false);
            ani.SetBool("Walking", false);
            ani.SetBool("Attacking", true);
        }
        else
            ani.SetBool("Attacking", false);




        if (Mathf.Abs(range) < 20f)
        {
            follow = true;
        }
        else
        {
            follow = false;
        }

        
    }
}
