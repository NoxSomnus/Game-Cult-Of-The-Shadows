using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchigoDarkCut : MonoBehaviour
{
    public GameObject target;
    [SerializeField] IchigoAI ichigoIA;
    [SerializeField] private float lookAt;
    [SerializeField] private float distance;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player 1.1");
        animator = GetComponent<Animator>();
        ichigoIA = GetComponent<IchigoAI>();
    }

    private void OnEnable()
    {
        target = GameObject.Find("Player 1.1");
        lookAt = target.transform.position.x - transform.position.x;
        if (lookAt > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (lookAt < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        animator.SetTrigger("DarkCut");
    }

    // Update is called once per frame


    public void DarkCut() 
    {
        float offset;
        if (lookAt > 0)
            offset = 3.5f;
        else
            offset = -3.5f;
       
        gameObject.transform.position = new Vector3(target.transform.position.x + offset, transform.position.y, transform.position.z);
    }

    public void EndAtk() 
    {
        ichigoIA.isAttacking = false;
        this.enabled = false;
    }
}
