using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatrolIA : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;

    private Animator animator;

    private int currentWaypoint;
    private bool isWaiting;
    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        
       // animator.SetBool("Run", true);
        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) > 0.1f)
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Run", true);
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);
        }
        else if(!isWaiting)
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Run", false);
            StartCoroutine(Wait());
            
        }
    }

     IEnumerator Wait()
    {
        isWaiting=true;
        
        yield return new WaitForSeconds(waitTime);
        currentWaypoint++;

        if(currentWaypoint == waypoints.Length)
        {
            currentWaypoint = 0;
        }

        isWaiting = false;
        Rotate();
    }
    private void Rotate()
    {
        Debug.Log(transform.position.x - waypoints[currentWaypoint].position.x);
         if (transform.position.x - waypoints[currentWaypoint].position.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }


}
