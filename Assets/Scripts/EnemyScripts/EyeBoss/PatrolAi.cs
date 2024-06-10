using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PatrolAi : MonoBehaviour
{

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    private int currentWaypoint;
    private bool isWaiting;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = 0;  
        isWaiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != waypoints[currentWaypoint].position)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                waypoints[currentWaypoint].position, speed * Time.deltaTime);
        }
        else if(!isWaiting)
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait() 
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        currentWaypoint = Random.Range(0, waypoints.Length);
        isWaiting = false;
    }
}
