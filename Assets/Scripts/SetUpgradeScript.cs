using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpgradeScript : MonoBehaviour
{
    public Parameters parameters;

    public Button MaximumHealthUpgrade;
    public Button MaximumManaUpgrade;
    public Button MaximumSoulUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        if (MaximumHealthUpgrade != null)
        {
            MaximumHealthUpgrade.onClick.AddListener(() => OnButtonClicked(MaximumHealthUpgrade));
        }
        else
        {
            Debug.LogError("El botón 1 no está asignado en el inspector.");
        }

        if (MaximumManaUpgrade != null)
        {
            MaximumManaUpgrade.onClick.AddListener(() => OnButtonClicked(MaximumManaUpgrade));
        }
        else
        {
            Debug.LogError("El botón 2 no está asignado en el inspector.");
        }

        if (MaximumSoulUpgrade != null)
        {
            MaximumSoulUpgrade.onClick.AddListener(() => OnButtonClicked(MaximumSoulUpgrade));
        }
        else
        {
            Debug.LogError("El botón 3 no está asignado en el inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked(Button clickedButton)
    {
        if (clickedButton == MaximumHealthUpgrade)
        {
            if(parameters.soulFragments >= 50)
            {
                parameters.MaximumHealth += 10;
                parameters.soulFragments -= 50;

            }
        }
        else if (clickedButton == MaximumManaUpgrade)
        {
            if (parameters.soulFragments >= 50)
            {
                parameters.MaximumMana += 10;
                parameters.soulFragments -= 50;
            }
        }
        else if (clickedButton == MaximumSoulUpgrade)
        {
            if (parameters.soulFragments >= 50)
            {
                parameters.MaximumSoul += 10;
                parameters.soulFragments -= 50;
            }
        }
        
    }
}
