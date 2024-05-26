using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class BossMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float walkSpeed;
    public float runSpeed;
    public float timer;
    public Animator ani;
    public GameObject target;
    public bool direction;
    public Enemy HP;
    public float range;
    public bool follow;
    public Rigidbody2D rgb;
    public float espera;
    public bool temiro;
    void Start()
    {
        ani = GetComponent<Animator>();
        rgb = ani.GetComponent<Rigidbody2D>();
        follow = false;
    }

    // Update is called once per frame
    void Update()
    {

        Search();
    }


    public void Search()
    {

        ani.SetBool("Slash", false);
        
            // range = Vector3.Distance(target.transform.position, transform.position);
            range = target.transform.position.x - transform.position.x;
            if (follow && HP.Health > 0)
            {
                HP.bossHealthBar.gameObject.SetActive(true);       

            // Mover solo en el eje X
            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, runSpeed * Time.deltaTime);
                if (range > 0 && temiro)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (range < 0 && temiro)
                {
                    transform.localScale = new Vector3(-1, 1, 1);

                }


            }
            else
            {
            HP.bossHealthBar.gameObject.SetActive(false);
            }
        if (Mathf.Abs(range) <= 1f && Time.time > espera + 4f)
        {
            espera = Time.time;
            ani.SetBool("Slash", true);
            //StartCoroutine(wait());

        }
        else
        {
            ani.SetBool("Slash", false);
        }

            if (Mathf.Abs(range) < 7f)
            {
                follow = true;
            }
            else
            {
                follow = false;
                ani.SetBool("Slash", false);
            }

    }
    public void SettemiroTrue()
    {
        temiro = true;
    }
    public void SettemiroFalse()
    {
        temiro = false;
    }

    IEnumerator wait()
    {
        
            
            yield return new WaitForSeconds(2f);
        
    }


}
