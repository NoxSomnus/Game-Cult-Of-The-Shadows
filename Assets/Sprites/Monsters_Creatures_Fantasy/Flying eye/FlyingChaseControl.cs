using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingChaseControl : MonoBehaviour
{
    public FlyingEnemy flyingEnemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            flyingEnemy.chase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            flyingEnemy.chase = false;
        }
    }
}
