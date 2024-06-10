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
    public Animator animator;
    private bool canAttack;
    [SerializeField] private FirstWorldMusic musicManager;
    public Enemy enemyStats;
    private BoxCollider2D boxCollider;
    [SerializeField] private bool changeLook;
    [SerializeField] private GameObject windWall;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player 1.1");
        originalRunSpeed = runSpeed;
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<Enemy>();
        boxCollider = GetComponent<BoxCollider2D>();
        canMove = true;
        changeLook = true;
        canAttack = true;
    }

    public void SetAnimator() 
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyStats.Health <= 0) 
        {
            animator.SetTrigger("Die");
            musicManager.WorldMusic();
            Destroy(windWall);
            //boxCollider.enabled = false;
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
