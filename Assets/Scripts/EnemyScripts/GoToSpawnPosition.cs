using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSpawnPosition : MonoBehaviour
{
    [SerializeField] private GameObject spawn;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, spawn.transform.position);

        if (distance > 30) 
        {
            gameObject.transform.position = spawn.transform.position;
        }
    }
}
