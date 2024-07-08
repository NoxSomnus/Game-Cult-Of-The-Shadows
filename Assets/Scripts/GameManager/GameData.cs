using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    //public List<SceneObjectData> sceneObjects;
    public List<PlayerData> playerDataList;
    public double soulFragments;


    public GameSaveData(Parameters pPlayer)
    {
        //sceneObjects = new List<SceneObjectData>();
        soulFragments = pPlayer.soulFragments;

        playerDataList = new List<PlayerData>();
    }
}