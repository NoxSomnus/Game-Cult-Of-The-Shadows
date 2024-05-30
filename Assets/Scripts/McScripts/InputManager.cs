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
        controls.Player.Shield.performed += AttackPerformed;
        controls.Player.Shield.canceled += AttackCanceled;
    }

    public bool GetAttack() 
    {
        return attacking;
    }

    private void AttackCanceled(InputAction.CallbackContext context)
    {
        attacking = false;
    }

    private void AttackPerformed(InputAction.CallbackContext context)
    {
        attacking = true;
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
