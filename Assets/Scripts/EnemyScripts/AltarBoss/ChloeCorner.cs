using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeCorner : MonoBehaviour
{
    public AltarOfEvilScript altarOfEvilScript;
    public bool canSpawnAltar = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canSpawnAltar) 
        {
            if (collision.name == "Chloe") 
            {
                altarOfEvilScript.chloeOnPosition = true;
                Debug.Log("chloe en posicion");
            }
        }              
    }
}
