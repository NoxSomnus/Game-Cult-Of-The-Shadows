using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public GameObject pauseMenu;

    public void pauseButton()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void resumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void mainmenu()
    {
        SceneManager.LoadScene("LoadTest");
    }
  
    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseButton();
        }
    }

}
