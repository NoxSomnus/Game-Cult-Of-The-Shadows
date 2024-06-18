using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharactersManager : MonoBehaviour
{
    [SerializeField] private Movement JoanneMovement;
    [SerializeField] private AuroraMovement AuroraMovement;
    [SerializeField] private RuntimeAnimatorController JoanneAnimator;
    [SerializeField] private RuntimeAnimatorController AuroraAnimator;
    Animator animator;


    public bool canSwitch;

    // Start is called before the first frame update
    void Start()
    {
        JoanneMovement = GetComponent<Movement>();
        AuroraMovement = GetComponent<AuroraMovement>();
        JoanneMovement.enabled = true;
        AuroraMovement.enabled = false;
        canSwitch = true;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = JoanneAnimator;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !JoanneMovement.attacking && !AuroraMovement.attacking)
            {
                JoanneMovement.enabled = true;

                AuroraMovement.enabled = false;
                JoanneMovement.combo = 0;
                SetJoanneAnimator();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && !AuroraMovement.attacking && !JoanneMovement.attacking)
            {
                JoanneMovement.enabled = false;

                AuroraMovement.enabled = true;
                AuroraMovement.combo = 0;
                SetAuroraAnimator();
            }
        }
    }

    private void SetJoanneAnimator()
    {
        animator.runtimeAnimatorController = JoanneAnimator;
    }
    private void SetAuroraAnimator()
    {
        animator.runtimeAnimatorController = AuroraAnimator;
    }
}
