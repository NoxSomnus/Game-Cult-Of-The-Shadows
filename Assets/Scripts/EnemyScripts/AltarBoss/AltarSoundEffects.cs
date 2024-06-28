using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarSoundEffects : MonoBehaviour
{
    public AudioSource src;
    public AudioClip altarOfEvilVoice;
    public AudioClip rainOfChaosVoice;
    public AudioClip oblivion;
    // Start is called before the first frame update
    public void AltarOfEvil() 
    {
        src.clip = altarOfEvilVoice;
        src.Play();
    }

    public void RainOfChaos() 
    {
        src.clip = rainOfChaosVoice;
        src.Play();
    }

    public void Oblivion() 
    {
        src.clip = oblivion;
        src.Play();
    }
}
