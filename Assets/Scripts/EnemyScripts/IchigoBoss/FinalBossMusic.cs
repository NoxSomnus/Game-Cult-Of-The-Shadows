using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossMusic : MonoBehaviour
{
    public AudioSource worldMusic;
    public AudioSource bossMusicSrc;
    public AudioClip[] musics;
    public void ActivateBossMusic() 
    {
        //añadir que se detenga el otro audio source
        bossMusicSrc.clip = musics[0];
        bossMusicSrc.Play();
    }

    public void DesactivateBossMusic() 
    {
        bossMusicSrc.Stop();
        worldMusic.Play();
    }
}
