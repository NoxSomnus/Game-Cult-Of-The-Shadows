using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeSoundEffects : MonoBehaviour
{
    public AudioSource src;
    public AudioClip[] throwKnife;
    public AudioClip[] cuts;
    public AudioClip arrow;
    // Start is called before the first frame update
    public void KnifeSpawn()
    {
        src.clip = throwKnife[0];
        src.Play();
    }

    public void ThrowKnife()
    {
        src.clip = throwKnife[1];
        src.Play();
    }

    public void Cuts0()
    {
        src.clip = cuts[0];
        src.Play();
    }

    public void Cuts1()
    {
        src.clip = cuts[1];
        src.Play();
    }

    public void Arrow()
    {
        src.clip = arrow;
        src.Play();
    }
}
