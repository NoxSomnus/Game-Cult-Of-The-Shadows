using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowLordBehaviour : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private float runSpeed;
    private float originalRunSpeed;
    [SerializeField] private bool canMove;
    [SerializeField] private float atkRange;
    [SerializeField] private float lookAt;
    [SerializeField] private float distance;
    public Animator animator;
    public Enemy enemyStats;
    [SerializeField] private bool changeLook;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player 1.1");
        originalRunSpeed = runSpeed;
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<Enemy>();
        canMove = true;
        changeLook = true;
    }

    void Update()
    {

        if (enemyStats.Health <= 0)
        {
            animator.SetTrigger("Die");
            this.enabled = false;
        }

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

        if (distance <= atkRange)
        {
            if (canMove)
                StartCoroutine(AttackSlash());
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

    private IEnumerator AttackSlash()
    {
        animator.SetTrigger("Slash");
        canMove = false;
        runSpeed = 0;
        changeLook = false;
        yield return new WaitForSeconds(2f);
        StartCoroutine(BrutalCuts());

    }

    private IEnumerator BrutalCuts() 
    {
        animator.SetTrigger("BrutalCut");
        yield return new WaitForSeconds(2.8f);
        canMove = true;
        runSpeed = originalRunSpeed;
        changeLook = true;
    }
}
