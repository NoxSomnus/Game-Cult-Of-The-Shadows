using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkPlayer : MonoBehaviour
{
    public int mondongo=0;
    public int dmg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Parameters player = collision.GetComponent<Parameters>();
            if (player != null)             
                player.Hit(dmg);                                       
        }
    }
}
