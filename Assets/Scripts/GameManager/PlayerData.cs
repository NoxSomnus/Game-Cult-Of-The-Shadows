using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string sceneId;
    public float soul;
    //public double soulFragments;
    public float[] position = new float[3];


    public PlayerData(Parameters pPlayer, string sceneID)
    {
        sceneId = sceneID;
        soul = pPlayer.soul;
        //soulFragments = pPlayer.soulFragments;
        position[0] = pPlayer.transform.position.x;
        position[1] = pPlayer.transform.position.y;
        position[2] = pPlayer.transform.position.z;
    }
}
