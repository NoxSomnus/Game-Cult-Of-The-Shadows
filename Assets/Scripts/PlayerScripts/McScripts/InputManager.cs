using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    static InputManager instance;
    public static InputManager Instance
    {
        get { return instance; }
    }

    GameControls controls;
    bool attacking;
    bool shield;

    private void Awake()
    {
        if (instance != null && instance == this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        controls = new GameControls();
        controls.Player.Shield.performed += ShieldPerformed;
        controls.Player.Shield.canceled += ShieldCanceled;
        controls.Player.HolySlash.performed += HolySlashPerfomed;
        controls.Player.HolySlash.canceled += HolySlashCanceled;
    }

    public bool Shield() 
    {
        return shield;
    }

    public bool HolySlash() 
    {
        return attacking;
    }

    private void ShieldCanceled(InputAction.CallbackContext context)
    {
        shield = false;
    }

    private void ShieldPerformed(InputAction.CallbackContext context)
    {
        shield = true;
    }

    private void HolySlashPerfomed(InputAction.CallbackContext context)
    {
        attacking = true;
    }

    private void HolySlashCanceled(InputAction.CallbackContext context)
    {
        attacking = false;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
