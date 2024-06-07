using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBossMove : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private float runSpeed;
    private float originalRunSpeed;
    [SerializeField] private bool canMove;
    [SerializeField] private float lookAt;
    [SerializeField] private float distance;
    private Animator animator;
    private bool canAttack;
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
        canAttack = true;
    }

    // Update is called once per frame
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
        if(changeLook && lookAt < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (distance <= 8f) 
        {
            if(canMove)
                StartCoroutine(Attack());
        }
        if (canMove)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, runSpeed * Time.deltaTime);
        }
    }

    private IEnumerator Attack()
    {
        animator.SetTrigger("Slash");
        canAttack = false;
        canMove = false;
        runSpeed = 0;
        changeLook = false;
        yield return new WaitForSeconds(2f);
        canMove = true;
        runSpeed = originalRunSpeed;
        changeLook = true;
        canAttack = true;
    }
}