using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioSource src;
    public AudioClip ConfirmSound;
    public string sceneName;
    // Start is called before the first frame update
    public void LoadGameScene()
    {
        SaveManager.DeletePlayerData();
        src.clip = ConfirmSound;
        src.Play();
        TransitionManager.Instance.LoadScene(sceneName);
    }

    public void LoadMenuScene()
    {
        src.clip = ConfirmSound;
        src.Play();
        TransitionManager.Instance.LoadScene("LoadTest");
    }
    public void LoadTutorialScene()
    {
         src.clip = ConfirmSound;
        src.Play();
        TransitionManager.Instance.LoadScene("Tutorial");
    }
    public void LoadCreditsScene()
    {
        src.clip = ConfirmSound;
        src.Play();
        TransitionManager.Instance.LoadScene("Creditos");
    }

    public void ContinueGame()
    {
        
        src.clip = ConfirmSound;
        src.Play();
        TransitionManager.Instance.LoadScene("Void Menu");
    }

    public void CloseGame()
    {
        src.clip = ConfirmSound;
        src.Play();
        Application.Quit();
    }
}
