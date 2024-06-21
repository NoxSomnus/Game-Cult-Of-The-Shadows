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
    public string sceneName;
    public HealthBar healthBar;
    public SoulBar soulBar;
    public StaminaBar StaminaBar;
    public int health = 100;
    public float soul = 0;
    public float Stamina = 30;
    public Double soulFragments;
    Movement playerMovement;
    SpriteRenderer sprite;
    BlinkEffect blink;
    public PlayerData lastPlayerData;
    public FireCamp fireCamp;

    private void Start()
    {
        soulBar.SetMaxFury(soul);
        healthBar.SetMaxHealth(health);
        sprite = GetComponent<SpriteRenderer>();
        blink = GetComponent<BlinkEffect>();
        playerMovement = GetComponent<Movement>();
        lastPlayerData = SaveManager.LoadPlayerData();
        LoadDataPlayer();
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
       /* if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }*/
    }

    public void ShieldHit(int dmg)
    {
        Stamina -= dmg;
        if (Stamina <= 0)
        {
            Stamina = 0;
            playerMovement.animator.SetBool("ShieldBroken", true);
            StartCoroutine(RestoreShield());
        }
        else
        {
            playerMovement.animator.SetTrigger("ShieldHit");
            StaminaBar.SetStamina(Stamina);
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

    private void SetLimitsInVariables()
    {
        if (soul < 0)
        {
            soul = 0;
        }

        if (soul >= 100)
            soul = 100;

        if (health > 100)
            health = 100;
    }

    private void FixedUpdate()
    {
        if (!playerMovement.canShield)
        {
            Stamina += 3f * Time.fixedDeltaTime;
            playerMovement.animator.SetBool("ShieldBroken", false);
        }
        SetLimitsInVariables();
        healthBar.SetHealth(health);
        soulBar.SetFury(soul);
        StaminaBar.SetStamina(Stamina);

        if (health <= 0)
        {
            WhenPlayerDie();
        }

    }

    public void LoadDataPlayer()
    {
        fireCamp.RestAndSave();
        PlayerData playerData = SaveManager.LoadPlayerData();
        soul = playerData.soul;
        soulFragments = playerData.soulFragments;
        transform.position = new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]);

    }

    private void WhenPlayerDie()
    {
        health = 1;
        lastPlayerData = SaveManager.LoadPlayerData();
        lastPlayerData.soulFragments = soulFragments * 0.5;
        SaveManager.OnlySavePlayerData(lastPlayerData);
        TransitionManager.Instance.LoadScene(sceneName);
    }
    
}
