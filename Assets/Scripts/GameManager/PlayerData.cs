using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]

public class PlayerData 
{
   // public float health;
    public float soul;
  //  public float stamina;
    public double soulFragments;
    public float[] position = new float[3];
    

    public PlayerData(Parameters pPlayer)
    {
      //  health = pPlayer.health;
        soul = pPlayer.soul;
      //  stamina = pPlayer.Stamina;
        soulFragments = pPlayer.soulFragments;
        position[0] = pPlayer.transform.position.x;
        position[1] = pPlayer.transform.position.y;
        position[2] = pPlayer.transform.position.z;

    }
}
