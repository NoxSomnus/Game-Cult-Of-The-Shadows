using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchigoSFX : MonoBehaviour
{
    public AudioSource src;
    public AudioSource src2;
    public AudioClip[] sfx;

    public void Slash0() 
    {
        src.clip = sfx[0];
        src.Play();
    }

    public void Slash1() 
    {
        src2.clip = sfx[1];
        src2.Play();
    }

    public void Slash2() 
    {
        src.clip = sfx[2];
        src.Play();
    }

    public void CutsSFX() 
    {
        src.clip = sfx[3];
        src.Play();
    }

    public void DarkCutSFX() 
    {
        src2.clip = sfx[4];
        src2.Play();
    }

    public void AirAtkDownSFX() 
    {
        src.clip = sfx[5];
        src.Play();
    }
}
