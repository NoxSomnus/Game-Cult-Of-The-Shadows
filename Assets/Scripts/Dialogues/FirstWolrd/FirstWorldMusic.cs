using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWorldMusic : MonoBehaviour
{
    public AudioClip bossMusic;
    public AudioClip worldMusic;
    public AudioSource src;
    public void BossMusic()
    {
        src.clip = bossMusic;
        src.Play();
    }

    public void WorldMusic()
    {
        src.clip = worldMusic;
        src.Play();
    }
}
