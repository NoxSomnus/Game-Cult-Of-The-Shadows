using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MoveToCorners : MonoBehaviour
{
    public ChloeBehaviour chloe;
    public ShadowLordBehaviour shadowLord;
    public AltarOfEvilScript altarOfEvilScript;
    public GameObject CholeCorner, ShadowCorner;
    public float runSpeed;

    private void Update()
    {
        if (altarOfEvilScript.TriggerInvocation)
        {
            if (!altarOfEvilScript.chloeOnPosition)
            {
                chloe.transform.position = Vector3.MoveTowards(chloe.transform.position, CholeCorner.transform.position, runSpeed * 2 * Time.deltaTime);
                chloe.animator.SetBool("Run", true);
                chloe.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                chloe.animator.SetBool("Charge", true);
                chloe.animator.SetBool("Run", false);
                chloe.transform.localScale = new Vector3(-1, 1, 1);
            }

            if (!altarOfEvilScript.shadowOnPosition)
            {
                shadowLord.transform.position = Vector3.MoveTowards(shadowLord.transform.position, ShadowCorner.transform.position, runSpeed * 2 * Time.deltaTime);
                shadowLord.transform.localScale = new Vector3(-1, 1, 1);
                shadowLord.animator.SetBool("Run", true);
            }
            else 
            {
                shadowLord.animator.SetBool("Charge", true);
                shadowLord.animator.SetBool("Run", false);
                shadowLord.transform.localScale = new Vector3(1, 1, 1);
            }
            chloe.enabled = false;
            shadowLord.enabled = false;
            for (int i = 0; i < chloe.attacks.Length; ++i)
            {
                chloe.attacks[i].enabled = false;
            }
        }
        else 
        {
            chloe.enabled = true;
            shadowLord.enabled = true;
        }

    }

}
