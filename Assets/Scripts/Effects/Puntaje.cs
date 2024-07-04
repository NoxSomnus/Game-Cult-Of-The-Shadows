using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puntaje : MonoBehaviour
{

    private double soulFragments;
    private TextMeshProUGUI textMesh;


    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
      
        textMesh.text = soulFragments.ToString("0");
    }

    public void updateFragmentsUI(double fragmentsobtained)
    {
        soulFragments += fragmentsobtained;
    }
}
