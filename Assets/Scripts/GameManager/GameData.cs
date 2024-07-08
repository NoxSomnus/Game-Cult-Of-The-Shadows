using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public int MaximumHealth;
    public float MaximumMana;
    public float MaximumSoul;

    //public List<SceneObjectData> sceneObjects;
    public List<PlayerData> playerDataList;
    public double soulFragments;


    public GameSaveData(Parameters pPlayer)
    {
        //sceneObjects = new List<SceneObjectData>();
        soulFragments = pPlayer.soulFragments;

        playerDataList = new List<PlayerData>();

        MaximumHealth = pPlayer.MaximumHealth;
        MaximumMana = pPlayer.MaximumMana;
        MaximumSoul = pPlayer.MaximumSoul;
    }
}