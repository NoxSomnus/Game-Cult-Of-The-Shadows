using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;
using UnityEngine.U2D;

public class Parameters : MonoBehaviour
{
    public HealthBar healthBar;
    public FuryBar furyBar;
    public int health = 100;
    public float fury = 0;
    public float Stamina = 100;
    SpriteRenderer sprite;
    BlinkEffect blink;

    private void Start()
    {
        furyBar.SetMaxFury(100f);
        healthBar.SetMaxHealth(health);
        sprite = GetComponent<SpriteRenderer>();
        blink = GetComponent<BlinkEffect>();
    }

    // Start is called before the first frame update
    public void RestoreFuryEvent(float furyObtained)
    {
        fury += furyObtained;
        //NUEVO
        furyBar.SetFury(fury);
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


    private IEnumerator Damager()
    {
        sprite.material = blink.blink;
        yield return new WaitForSeconds(0.5f);
        sprite.material = blink.original;
    }
}
