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

}
