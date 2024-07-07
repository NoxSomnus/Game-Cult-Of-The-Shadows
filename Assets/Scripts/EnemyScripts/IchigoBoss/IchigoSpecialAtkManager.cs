using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchigoSpecialAtkManager : MonoBehaviour
{
    public IchigoAI ichigoAI;
    public Enemy ichigoStats;
    public BrutalCombo brutalCombo;
    public bool triggeredCombo0 = false;
    public bool triggeredCombo1 = false;
    

    // Update is called once per frame
    void Update()
    {

        if (ichigoStats.Health <= 75 && !triggeredCombo0) 
        {
            triggeredCombo0 = true;
            ichigoAI.isAttacking = true;
            ichigoAI.enabled = false;
            brutalCombo.enabled = true;
        }

        if (ichigoStats.Health <= 25 && !triggeredCombo1)
        {
            triggeredCombo1 = true;
            ichigoAI.isAttacking = true;
            ichigoAI.enabled = false;
            brutalCombo.enabled = true;
        }

    }
}
