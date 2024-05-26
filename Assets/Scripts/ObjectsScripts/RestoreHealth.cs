using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealth : MonoBehaviour
{
    public int healthRestore;
    public float range;
    public GameObject target;
    private PlayerMovement player;

    private void Start()
    {
        player = target.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        range = Vector2.Distance(target.transform.position, transform.position);
        if (range <= 0.05f) 
        {
            player.RestoreHealth(healthRestore);
            Destroy(gameObject);
        }
    }

}
