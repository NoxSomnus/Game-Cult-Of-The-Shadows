using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;
using UnityEngine.U2D;
using System;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;

public class Parameters : MonoBehaviour
{
    public HealthBar healthBar;
    public SoulBar soulBar;
    public int health = 100;
    public float soul = 0;
    public float Stamina = 30;
    Movement playerMovement;
    SpriteRenderer sprite;
    BlinkEffect blink;

    private void Start()
    {
        soulBar.SetMaxFury(soul);
        healthBar.SetMaxHealth(health);
        sprite = GetComponent<SpriteRenderer>();
        blink = GetComponent<BlinkEffect>();
        playerMovement = GetComponent<Movement>();
    }

    // Start is called before the first frame update
    public void RestoreFuryEvent(float furyObtained)
    {
        soul += furyObtained;
        //NUEVO
        soulBar.SetFury(soul);
    }
    public void Hit(int dmg)
    {
        health -= dmg;
        healthBar.SetHealth(health);
        StartCoroutine(Damager());
        //animator.SetTrigger("Hit");
        //soundEffectsManager.HitClip();
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ShieldHit(int dmg) 
    {
        Stamina -= dmg;
        if (Stamina <= 0)
        {
            Stamina = 0;
            playerMovement.animator.SetBool("ShieldBroken",true);
            StartCoroutine(RestoreShield());
        }
        else 
        {
            playerMovement.animator.SetTrigger("ShieldHit");
        }

    }

    private IEnumerator Damager()
    {
        sprite.material = blink.blink;
        yield return new WaitForSeconds(0.5f);
        sprite.material = blink.original;
    }

    private IEnumerator RestoreShield()
    {
        playerMovement.canShield = false;
        yield return new WaitForSeconds(10f);
        playerMovement.canShield = true;
        if (Stamina > 30f) Stamina = 30;
    }

    private void FixedUpdate()
    {
        if(!playerMovement.canShield) 
        {
            Stamina += 3f * Time.fixedDeltaTime;
            playerMovement.animator.SetBool("ShieldBroken", false);
        }

    }
}
