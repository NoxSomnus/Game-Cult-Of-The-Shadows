using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBossSoundEffects : MonoBehaviour
{
    public AudioSource src;
    public AudioClip slash;
    public void Slash()
    {
        src.clip = slash;
        src.Play();
    }
}
