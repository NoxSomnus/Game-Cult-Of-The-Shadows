using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchigoCuts : MonoBehaviour
{
    public GameObject target;
    public float runSpeed;
    private float originalRunSpeed;
    [SerializeField] IchigoAI ichigoAi;
    [SerializeField] private bool canMove;
    [SerializeField] private float lookAt;
    [SerializeField] private float distance;
    public Animator animator;
    bool atkIsDone;
    [SerializeField] private bool changeLook;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player 1.1");

        animator = GetComponent<Animator>();
        canMove = true;
        changeLook = true;
        ichigoAi = GetComponent<IchigoAI>();
        runSpeed = ichigoAi.runSpeed;
        originalRunSpeed = runSpeed;
    }

    private void OnEnable()
    {
        atkIsDone = false;
    }
    public void SetAnimator()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!atkIsDone)
        {
            lookAt = target.transform.position.x - transform.position.x;
            distance = Vector2.Distance(target.transform.position, transform.position);
            if (lookAt > 0 && changeLook)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (changeLook && lookAt < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (distance <= 3f)
            {
                if (canMove)
                    StartCoroutine(Attack());
            }
            if (canMove)
            {
                Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, runSpeed * Time.deltaTime);
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }
    }

    private IEnumerator Attack()
    {
        animator.SetTrigger("Cuts");
        canMove = false;
        runSpeed = 0;
        changeLook = false;
        yield return new WaitForSeconds(3.5f);
        atkIsDone = true;
        canMove = true;
        runSpeed = originalRunSpeed;
        changeLook = true;
        ichigoAi.isAttacking = false;
        this.enabled = false;
    }
}
