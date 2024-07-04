using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneObjectData
{
    public string name;
    public string sceneId;
    public bool enable;
    public int objectID;

    public SceneObjectData(GameObject objeto)
    {
        //  health = pPlayer.health;
        name = objeto.name;
        sceneId = objeto.scene.name;
        enable = objeto.GetComponent<enabled>().activao;
        objectID = objeto.GetInstanceID();

    }
}


