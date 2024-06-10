using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    public void LoadGameScene()
    {
        TransitionManager.Instance.LoadScene(sceneName);
    }

    public void LoadMenuScene()
    {
        TransitionManager.Instance.LoadScene(TransitionManager.SCENE_NAME_MAIN_MENU);
    }
}
