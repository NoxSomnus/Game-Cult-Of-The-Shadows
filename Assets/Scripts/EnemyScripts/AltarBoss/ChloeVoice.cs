using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeVoice : MonoBehaviour
{
    public AudioSource src;
    public AudioClip arrowVoice;
    public AudioClip[] groundAtk;
    public AudioClip[] throwKnife;
    private int number;
    // Start is called before the first frame update
    public void ArrowAtk()
    {
        number = Random.Range(0, 11);
        if (number > 0 && number <= 5)
        {
            src.clip = arrowVoice;
            src.Play();
        }

    }

    public void Cuts()
    {
        number = Random.Range(0, 11);
        if (number > 0 && number <= 5)
        {
            src.clip = groundAtk[Random.Range(0,2)];
            src.Play();
        }
    }

    public void ThrowingKnife() 
    {
        number = Random.Range(0, 11);
        if (number > 0 && number <= 3)
        {
            src.clip = throwKnife[0];
            src.Play();
        }
        else
        {
            if (number > 8 && number <= 10)
            {
                src.clip = throwKnife[1];
                src.Play();
            }
        }
    }


}
