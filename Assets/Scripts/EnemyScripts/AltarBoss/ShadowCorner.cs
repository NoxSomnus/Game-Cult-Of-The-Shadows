using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCorner : MonoBehaviour
{
    public AltarOfEvilScript altarOfEvilScript;
    public bool canSpawnAltar = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canSpawnAltar)
        {
            if (collision.name == "ShadowLord")
            {
                altarOfEvilScript.shadowOnPosition = true;
                Debug.Log("shadow en posicion");
            }
        }
    }
}
