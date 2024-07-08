using UnityEngine;
using UnityEngine.UI;

public class SimpleButtonHandler : MonoBehaviour
{
    public Button LevelUpButton;

    public bool isPressed = false;

    public GameObject UpgradeHealthButton;
    public GameObject UpgradeManaButton;
    public GameObject UpgradeSoulButton;
    public GameObject UpgradeCostText;

    void Start()
    {
        Debug.Log("Start ejecutado");

        if (LevelUpButton != null)
        {
            LevelUpButton.onClick.AddListener(OnButtonClicked);
            LevelUpButton.gameObject.SetActive(false);
            Debug.Log("Listener agregado al botón");
        }
        else
        {
            Debug.LogError("El botón no está asignado en el inspector.");
        }        
        UpgradeHealthButton.SetActive(false);
        UpgradeManaButton.SetActive(false);
        UpgradeSoulButton.SetActive(false);
        UpgradeCostText.SetActive(false) ;
    }

    void OnButtonClicked()
    {

        Debug.Log("Botón presionado!");

        isPressed = !isPressed;
        ControllerUpgradeButtons(isPressed);

    }

    public void ActivateLevelUpButton()
    {
        LevelUpButton.gameObject.SetActive(true);
    }

    public void DeactivasteLevelUpButton()
    {
        LevelUpButton.gameObject.SetActive(false);
        ControllerUpgradeButtons(false);
    }

    public void ControllerUpgradeButtons(bool isActive)
    {
        UpgradeHealthButton.SetActive(isActive);
        UpgradeManaButton.SetActive(isActive);
        UpgradeSoulButton.SetActive(isActive);
        UpgradeCostText.SetActive(isActive);
    }
}
