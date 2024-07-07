using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateIchigoAI : MonoBehaviour
{
    public IchigoAI ichigoAI;
    public Enemy ichigoStats;

    private void Start()
    {
        ichigoAI.GetComponent<IchigoAI>();
        ichigoStats = ichigoAI.GetComponent<Enemy>();
    }

    public void Transform()
    {
        ichigoStats.enabled = true;
        ichigoAI.enabled = true;
    }
}
