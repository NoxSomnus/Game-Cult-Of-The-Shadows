using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SorcererMoveWaypoint : MonoBehaviour
{

    bool moveToWaypoint;
    [SerializeField] private Transform waypoint;
    [SerializeField] private float speed;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        moveToWaypoint = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (moveToWaypoint) 
        {
            StartCoroutine(MoveToWaipoint());
        }
    }

    private IEnumerator MoveToWaipoint() 
    {
        transform.position = Vector2.MoveTowards(transform.position,
                waypoint.position, speed * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        moveToWaypoint = false;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon")) 
        {
            animator.SetTrigger("Hit");
            moveToWaypoint = true;
        }
    }
}
