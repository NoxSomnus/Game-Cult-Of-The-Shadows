using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBossSoundEffects : MonoBehaviour
{
    public AudioSource src;
    public AudioClip demonSpawnBolitas;
    public AudioClip explosion;
    // Start is called before the first frame update
    public void Bolitas()
    {
        src.volume = 0.8f;
        src.clip = demonSpawnBolitas;
        src.Play();
    }

    public void StopBolitas() 
    {
        src.Stop();
    }

    public void Explosion() 
    {
        src.volume = 0.3f;
        src.clip = explosion;
        src.Play();
    }
}
