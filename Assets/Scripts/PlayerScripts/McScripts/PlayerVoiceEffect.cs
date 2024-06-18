using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoiceEffect : MonoBehaviour
{
    public AudioSource src;
    public AudioClip thirdAtkVoice;
    public AudioClip lightCutVoice;
    public AudioClip lightCutVoiceEnd;
    public AudioClip holySlashVoice;

    public void LightCutVoice()
    {
        src.clip = lightCutVoice;
        src.Play();
    }

    public void LightCutVoiceEnd()
    {
        src.clip = lightCutVoiceEnd;
        src.Play();
    }

    public void HolySlashVoice()
    {
        src.clip = holySlashVoice;
        src.Play();
    }

    public void ThirdAtkVoice()
    {
        src.clip = thirdAtkVoice;
        src.Play();
    }
}
