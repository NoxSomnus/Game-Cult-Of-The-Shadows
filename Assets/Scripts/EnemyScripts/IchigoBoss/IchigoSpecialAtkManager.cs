using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchigoSpecialAtkManager : MonoBehaviour
{
    public IchigoAI ichigoAI;
    public Enemy ichigoStats;
    public BrutalCombo brutalCombo;
    public Animator animator;

    public bool triggeredCombo0 = false;
    public bool triggeredCombo1 = false;
    public bool triggeredCombo2 = false;
    

    // Update is called once per frame
    void Update()
    {
        /*if (ichigoStats != null)
        {
            if (ichigoStats.Health <= 0)
            {
                animator.SetBool("Die",true);
            }
        }*/

        if (ichigoStats != null)
        {
            if (ichigoStats.Health <= 200 && !triggeredCombo0)
            {
                triggeredCombo0 = true;
                ichigoAI.isAttacking = true;
                ichigoAI.enabled = false;
                brutalCombo.enabled = true;
            }

            if (ichigoStats.Health <= 100 && !triggeredCombo1)
            {
                triggeredCombo1 = true;
                ichigoAI.isAttacking = true;
                ichigoAI.enabled = false;
                brutalCombo.enabled = true;
            }

            if (ichigoStats.Health <= 50 && !triggeredCombo2)
            {
                triggeredCombo2 = true;
                ichigoAI.isAttacking = true;
                ichigoAI.enabled = false;
                brutalCombo.enabled = true;
            }
        }


    }
}
