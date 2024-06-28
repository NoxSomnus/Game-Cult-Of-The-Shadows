using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class AltarOfEvilScript : MonoBehaviour
{
    [SerializeField] private AltarAtks altarAtksScript; 
    public bool chloeOnPosition, shadowOnPosition = false;
    public bool apollyonOblivion = false;
    public bool TriggerInvocation,firstInvocation,secondInvocation,thirdInvocation,fourthInvocation = true;
    public GameObject Chloe, ShadowLord;
    public Enemy ChloeHealth;
    public Enemy ShadowLordHealth;
    public ChloeCorner corner1;
    public ShadowCorner corner2;
    private ApollyonOblivion apollyonOblivionScript;

    private void Start()
    {
        apollyonOblivionScript = GetComponent<ApollyonOblivion>();
        altarAtksScript = GetComponent<AltarAtks>();
    }


    void Update()
    {
        if (ChloeHealth.Health <= 50 && firstInvocation) 
        {
            TriggerInvocation = true;
            apollyonOblivion = true;
            firstInvocation = false;
            corner1.enabled = false;
            corner2.enabled = false;
        }

        if (ShadowLordHealth.Health <= 50 && secondInvocation)
        {
            TriggerInvocation = true;
            apollyonOblivion = false;
            secondInvocation = false;
            corner1.enabled = false;
            corner2.enabled = false;
        }

        if (ChloeHealth.Health <= 25 && thirdInvocation)
        {
            TriggerInvocation = true;
            apollyonOblivion = false;
            thirdInvocation = false;
            corner1.enabled = false;
            corner2.enabled = false;
        }

        if (ShadowLordHealth.Health <= 25 && fourthInvocation)
        {
            TriggerInvocation = true;
            fourthInvocation = false;
            apollyonOblivion = true;
            corner1.enabled = false;
            corner2.enabled = false;
        }

        if (ShadowLordHealth.Health <= 0 || ChloeHealth.Health <= 0)
            Destroy(gameObject);

        if (TriggerInvocation)
        {
            corner1.gameObject.SetActive(true);
            corner2.gameObject.SetActive(true);
            corner1.canSpawnAltar = true;
            corner2.canSpawnAltar = true;
            if (apollyonOblivion)
            {
                if (apollyonOblivionScript.enabled == false)
                {
                    if (chloeOnPosition && shadowOnPosition)
                    {
                        apollyonOblivionScript.enabled = true;
                        Chloe.GetComponent<Animator>().SetBool("Charge", true);
                        ShadowLord.GetComponent<Animator>().SetBool("Charge", true);
                    }
                }
            }
            else
            {
                if (altarAtksScript.enabled == false)
                {
                    if (chloeOnPosition && shadowOnPosition)
                    {
                        altarAtksScript.enabled = true;
                        Chloe.GetComponent<Animator>().SetBool("Charge", true);
                        ShadowLord.GetComponent<Animator>().SetBool("Charge", true);
                    }
                }
            }
        }
        else 
        {
            Chloe.GetComponent<Animator>().SetBool("Charge", false);
            ShadowLord.GetComponent<Animator>().SetBool("Charge", false);
            corner1.canSpawnAltar = false;
            corner2.canSpawnAltar = false;
            corner1.gameObject.SetActive(false);
            corner2.gameObject.SetActive(false);
        }
    }

    
}
