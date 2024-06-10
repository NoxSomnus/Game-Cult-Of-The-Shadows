using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnAnyKey : MonoBehaviour
{
    public AudioSource src;
    public AudioClip StartSound;
    public string sceneName; // Nombre de la escena que deseas cargar

    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(Cargar());
        }
    }

    IEnumerator Cargar()
    {
        src.clip = StartSound;
        src.Play();
        
        yield return new WaitForSeconds(2);
        TransitionManager.Instance.LoadScene(sceneName);
    }
}
