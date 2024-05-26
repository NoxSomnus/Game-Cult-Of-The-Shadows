using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shockwave : MonoBehaviour
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
        StartCoroutine(DestroyShockwave());
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision.tag == "Enemy") 
        {
            if (enemy != null) enemy.Hit(dmg);
        } //Que no choque con el jugador el collider del shockwave
            //Destroy(gameObject);
    }

    private IEnumerator DestroyShockwave()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
