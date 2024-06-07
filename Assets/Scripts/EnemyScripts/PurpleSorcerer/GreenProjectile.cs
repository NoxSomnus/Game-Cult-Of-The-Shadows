using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenProjectile : MonoBehaviour
{
    public float Speed;
    private Vector2 Direction;
    private Rigidbody2D Rigidbody2D;
    public int dmg = 5;
    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
        if (Direction == Vector2.left)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
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
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (collision.tag == "Player")
        {
            if (player != null) player.Hit(dmg);
            Destroy(gameObject);
        } 

    }
    private IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
