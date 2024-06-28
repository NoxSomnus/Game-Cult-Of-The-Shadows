using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowLordVoice : MonoBehaviour
{
    public AudioSource src;
    public AudioClip[] brutalCutVoices;
    public AudioClip[] cries;
    private int number;
    
    // Start is called before the first frame update
    public void Atk()
    {
        number = Random.Range(0,11);
        if (number > 0 && number <= 3)
        {
            src.clip = brutalCutVoices[0];
            src.Play();
        }
        else 
        {
            if (number > 8 && number <= 10) 
            {
                src.clip = brutalCutVoices[1];
                src.Play();
            }
        }
    }

    public void Cuts()
    {
        number = Random.Range(0, 11);
        if (number > 0 && number <= 5)
        {
            src.clip = cries[0];
            src.Play();
        }
    }

    public void AltarCry() 
    {
        src.clip = cries[1];
        src.Play();
    }
}
