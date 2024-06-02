using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldReceiveDmg : MonoBehaviour
{
    public Parameters playerStats;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyAtkPlayer enemyDmg = collision.GetComponent<EnemyAtkPlayer>();
                playerStats.ShieldHit(enemyDmg.dmg);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            EnemyAtkPlayer enemyDmg = collision.collider.GetComponent<EnemyAtkPlayer>();
            playerStats.ShieldHit(enemyDmg.dmg);
        }
    }
}
