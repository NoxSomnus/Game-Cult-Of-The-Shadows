using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float despawnTimer;
    private SpriteRenderer sprite;
    private BlinkEffect blink;
    public int Health;
    public BossHealthBar bossHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        despawnTimer = 5f;
        sprite = GetComponent<SpriteRenderer>();
        blink = GetComponent<BlinkEffect>();
        if (bossHealthBar != null)
        {     
            bossHealthBar.SetMaxHealth(Health);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            despawnTimer -= 1 * Time.deltaTime;

            if (despawnTimer <= 0)
                Destroy(gameObject);
        }
        

    }

    public void Hit(int dmg)
    {
        Health -= dmg;
        StartCoroutine(Damager());
        if (bossHealthBar != null)
        {
            bossHealthBar.SetHealth(Health);
        }       
    }

    private IEnumerator Damager()
    {
        sprite.material = blink.blink;
        yield return new WaitForSeconds(0.5f);
        sprite.material = blink.original;
    }
}