using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkEnemy : MonoBehaviour
{

    public Parameters Player;
    public int dmg;
    public float furyObtained;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Player.RestoreFuryEvent(furyObtained);
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Hit(dmg);
            }
        }
    }

}
