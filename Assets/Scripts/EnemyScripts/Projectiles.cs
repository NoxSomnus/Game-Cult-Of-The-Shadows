using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float Speed;
    private Vector2 Direction;
    private Rigidbody2D Rigidbody2D;
    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
        if (direction.x >= 0.0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Direction = Vector2.right;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Direction = Vector2.left;
        }
    }

    public void SetDirectionArrow(Vector2 direction)
    {
        Direction = direction;
        if (direction.x >= 0.0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void SetDirectionShockwave() 
    {
        Direction = Vector2.down;
    }


    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyProjectile());
    }
    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Movement player = collision.GetComponent<Movement>();
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }

    }
    private IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
