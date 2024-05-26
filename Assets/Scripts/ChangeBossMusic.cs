using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBossMusic : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource src;
    public AudioClip stageMusic;
    public AudioClip bossMusic;
    // Start is called before the first frame update
    public void StageMusic()
    {
        src.clip = stageMusic;
        src.Play();
    }

    public void BossMusic()
    {
        src.clip = bossMusic;
        src.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            BossMusic();
            Destroy(gameObject);
        }
    }
}
