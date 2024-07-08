using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchigoVoice : MonoBehaviour
{
    public AudioSource src;
    public AudioClip[] voices;
    private int number;

    public void NotForgetDevilsPower() 
    {
        src.clip = voices[0];
        src.Play();
    }

    public void Atk0() 
    {
        src.clip = voices[1];
        src.Play();
    }

    public void Atk1()
    {
        src.clip = voices[2];
        src.Play();
    }

    public void Atk2()
    {
        src.clip = voices[3];
        src.Play();
    }

    public void OutOfMyWay()
    {

        src.clip = voices[4];
        src.Play();

    }

    public void TooSlow()
    {
        src.clip = voices[5];
        src.Play();
    }

    public void KneelBeforeMe()
    {

        src.clip = voices[6];
        src.Play();

    }

    public void AirAtk() 
    {
        number = Random.Range(0, 101);
        if (number > 0 && number <= 50)
        {
            src.clip = voices[6]; // KneelBeforeMe
            src.Play();
        }

        if (number > 50 && number <= 100)
        {
            src.clip = voices[4]; //Out Of My Way
            src.Play();
        }
    }

    public void Charge()
    {
        number = Random.Range(0, 101);
        if (number > 0 && number <= 25) 
        {
            src.clip = voices[7]; // This is Power
            src.Play();
        }

        if (number > 25 && number <= 50) 
        {
            src.clip = voices[8]; //Motivated
            src.Play();
        }

        if (number > 50 && number <= 75)
        {
            src.clip = voices[11]; //ShallDie
            src.Play();
        }

        if (number > 75 && number <= 100)
        {
            src.clip = voices[10]; //Will be your end
            src.Play();
        }

    }

    public void ThisIsPower()
    {
        src.clip = voices[7]; // This is Power
        src.Play();
    }

        public void Motivated()
    {
        src.clip = voices[8];
        src.Play();
    }

    public void NightmareBeginsHere()
    {
        src.clip = voices[9];
        src.Play();
    }

    public void WillBeYourEnd()
    {
        src.clip = voices[10];
        src.Play();
    }

    public void YouShallDie()
    {
        src.clip = voices[11];
        src.Play();
    }

}
