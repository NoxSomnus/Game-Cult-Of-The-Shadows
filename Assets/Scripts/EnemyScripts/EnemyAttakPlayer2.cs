using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyAttakPlayer2 : MonoBehaviour
{

    public int dmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detecta cualquier tipo de collider
        if (collision.gameObject.CompareTag("Player"))
        {
            Parameters player = collision.gameObject.GetComponent<Parameters>();
            if (player != null)
            {
                player.Hit(dmg);
            }
        }
    }
}
