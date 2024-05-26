using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public AudioSource src;
    public AudioClip normalAtk;
    public AudioClip specialAtk;
    public AudioClip hit;
    public AudioClip aura;
    // Start is called before the first frame update
    public void NormalAtkClip() 
    {
        src.clip = normalAtk;
        src.Play();
    }

    public void SpecialAtkClip()
    {
        src.clip = specialAtk;
        src.Play();
    }

    public void HitClip()
    {
        src.clip = hit;
        src.Play();
    }

    public void AuraTransition() 
    {
        src.clip = aura;
        src.Play();
    }
}
