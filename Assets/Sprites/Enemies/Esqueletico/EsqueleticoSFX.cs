using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueleticoSFX : MonoBehaviour
{
    public AudioSource src;

    public void dibitybayadode() 
    {
        src.Play();
    }

    public void Stopdibitybayadode()
    {
        src.Stop();
    }


}
