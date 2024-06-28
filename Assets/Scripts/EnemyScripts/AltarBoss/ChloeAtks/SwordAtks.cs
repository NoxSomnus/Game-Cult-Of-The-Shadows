using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordAtks : MonoBehaviour
{
    public Animator animator;
    private float lookAt;
    [SerializeField] private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player 1.1");
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        lookAt = target.transform.position.x - transform.position.x;
        if (lookAt > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (lookAt < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        animator.SetTrigger("SwordAtks");
    }

}
