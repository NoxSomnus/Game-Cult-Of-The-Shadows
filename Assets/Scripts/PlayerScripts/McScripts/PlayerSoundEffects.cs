using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    public AudioSource src;
    public AudioClip firstNormalAtk;
    public AudioClip secondNormalAtk;
    public AudioClip thirdNormalAtk;
    public AudioClip heavyAirAtk;
    public AudioClip hitGround;
    public AudioClip holySlash;
    public AudioClip lightCutBegin;
    public AudioClip lightCutEnd;
    public AudioClip hit;

    //Aurora Effects
    public AudioClip firstFireAtk;
    public AudioClip secondFireAtk;
    public AudioClip thirdFireAtk;
    public AudioClip firsticeAtk;
    // Start is called before the first frame update
    public void FirstNormalAtk()
    {
        src.clip = firstNormalAtk;
        src.Play();
    }

    public void SecondNormalAtk()
    {
        src.clip = secondNormalAtk;
        src.Play();
    }

    public void ThirdNormalAtk()
    {
        src.clip = thirdNormalAtk;
        src.Play();
    }

    public void HeavyAirEffect() 
    {
        src.clip = heavyAirAtk;
        src.Play();
    }

    public void HitGroundEffect()
    {
        src.clip = heavyAirAtk;
        src.Play();
    }


    public void LightCutBegin()
    {
        src.clip = lightCutBegin;
        src.Play();
    }

    public void HolySlashEffect()
    {
        src.clip = holySlash;
        src.Play();
    }

    public void LightCutEnd()
    {
        src.clip = lightCutEnd;
        src.Play();
    }

    public void FirstFireAtk()
    {
        src.clip = firstFireAtk;
        src.Play();
    }

    public void SecondFireAtk()
    {
        src.clip = secondFireAtk;
        src.Play();
    }

    public void ThirdFireAtk()
    {
        src.clip = thirdFireAtk;
        src.Play();
    }

    public void FirstIceAtk()
    {
        src.clip = firsticeAtk;
        src.Play();
    }

}
