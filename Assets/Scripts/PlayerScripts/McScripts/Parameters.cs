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
    public HealthBar healthBarJ;
    public HealthBar healthBarA;
    public SoulBar soulBar;
    public ManaBar manaBar;
    //public StaminaBar StaminaBar;
    public int health = 100;
    public float soul = 0;
    public float maxSoul = 100;
    public float Stamina = 30;
    public float mana = 100;
    public Double soulFragments;
    Movement playerMovement;
    SpriteRenderer sprite;
    BlinkEffect blink;
    public GameSaveData gameSaveData;
    public FireCamp fireCamp;
    public bool inicio;

    public int MaximumHealth = 100;
    public float MaximumMana = 100;
    public float MaximumSoul = 100;

    [SerializeField] private Puntaje puntos;

    [SerializeField] private Animator shieldUI;

    private void Start()
    {
        soulBar.SetMaxFury(maxSoul);
        healthBarJ.SetMaxHealth(health);
        healthBarA.SetMaxHealth(health);
        manaBar.SetMaxMana(mana);
        sprite = GetComponent<SpriteRenderer>();
        blink = GetComponent<BlinkEffect>();
        playerMovement = GetComponent<Movement>();
        //GameSaveData = fireCamp.SavePlayerData();
        gameSaveData = SaveManager.LoadPlayerData(this);

        LoadDataPlayer();
        inicio = true;
        puntos.updateFragmentsUI(soulFragments);
        
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
        healthBarJ.SetHealth(health);
        healthBarA.SetHealth(health);

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
            shieldUI.SetTrigger("BrokenShield");

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
        shieldUI.SetTrigger("RestoreShield");
    }

    private void SetLimitsInVariables()
    {
        if (soul < 0)
        {
            soul = 0;
        }

        if (soul >= MaximumSoul)
            soul = MaximumSoul;

        if (health > MaximumHealth)
            health = MaximumHealth;

        if(mana > MaximumMana) mana = MaximumMana;

        if (mana < 0) mana = 0;
    }

    private void FixedUpdate()
    {
        if (!playerMovement.canShield)
        {
            Stamina += 3f * Time.fixedDeltaTime;
            playerMovement.animator.SetBool("ShieldBroken", false);
        }
        SetLimitsInVariables();
        healthBarJ.SetHealth(health);
        healthBarA.SetHealth(health);
        manaBar.SetMana(mana);
        soulBar.SetFury(soul);

        if (health <= 0)
        {
            WhenPlayerDie();
        }
        if (inicio )
        {
            inicio = false;
            fireCamp.RestAndSave();
            
        }else if (!inicio && fireCamp.isResting && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(fireCamp.Wait());
        }

        mana += 3f * Time.fixedDeltaTime;

    }

    public void LoadDataPlayer()
    {

       // GameSaveData playerData = GameSaveData;
        foreach (PlayerData pd in gameSaveData.playerDataList) // itera
        {
            if (pd.sceneId == SceneManager.GetActiveScene().name)// si consigue la scena en el archivo
            {
                Debug.Log("vamo a cargar AAAAAAAAAAAAAAAAAAS");
                transform.position = new Vector3(pd.position[0], pd.position[1], pd.position[2]);
                soul = pd.soul;
                

                break;
            }
        }
        soulFragments = gameSaveData.soulFragments;

        MaximumHealth = gameSaveData.MaximumHealth;
        MaximumMana = gameSaveData.MaximumMana;
        MaximumSoul = gameSaveData.MaximumSoul;
    }

    private void WhenPlayerDie()
    {
        health = 1; //No tocar, No me borren
        playerMovement.enabled = false;
        playerMovement.GetComponent<BoxCollider2D>().enabled = false;
        gameSaveData = SaveManager.LoadPlayerData(this);
        gameSaveData.soulFragments = soulFragments * 0.5;
        SaveManager.OnlySavePlayerData(gameSaveData);
        StartCoroutine(DieAnimation());
       // SceneManager.LoadScene("GameOver");
    }

    private IEnumerator DieAnimation()
    {
        playerMovement.animator.SetTrigger("Die");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("GameOver");

    }
    public void AddFragments(double fragmentsobtained)
    {
        soulFragments += fragmentsobtained;
    }
    

}
