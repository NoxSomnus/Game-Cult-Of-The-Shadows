using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSoundEffects : MonoBehaviour
{
    public AudioSource src;
    public AudioClip brutalCut;
    // Start is called before the first frame update
    public void BrutalCut()
    {
        src.clip = brutalCut;
        src.Play();
    }
}
