using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.U2D;

public class Dummy : MonoBehaviour
{
    public bool isdamage;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D dummy)
    {
        if (dummy.tag == "Weapon")
        {
            StartCoroutine(Damager());
        }
    }
    private IEnumerator Damager()
    {
        animator.SetBool("is damage", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("is damage", false);

    }
}
