using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChloeBehaviour : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private float atkRange;
    [SerializeField] private float distance;
    public Animator animator;
    public Enemy enemyStats;
    public int atkDecision;
    public int lastAtkDecision;
    private Vector3 projectileDirection;
    public GameObject ProjectilePrefab;
    public bool isAttacking = false;
    private float lookAt;
    private Vector3 direction;
    private float spawnOffset;
    public MonoBehaviour[] attacks;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player 1.1");
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<Enemy>();
        attacks[0] = GetComponent<AirArrowAtk>();
        attacks[1] = GetComponent<SwordAtks>();
        lastAtkDecision = 88;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(target.transform.position, transform.position);

        if (enemyStats.Health <= 0)
        {
            this.enabled = false;
            attacks[0].enabled = false; attacks[1].enabled = false;
            animator.SetTrigger("Die");

        }

        if (distance <= atkRange) 
        {
            if (!isAttacking) 
            {
                atkDecision = Random.Range(0, 2);
                if (atkDecision != lastAtkDecision)
                {
                    attacks[atkDecision].enabled = true;
                    lastAtkDecision = atkDecision;
                    isAttacking = true;
                }
            }

        }

        if (!isAttacking)
        {

            direction = target.transform.position - transform.position;
            lookAt = target.transform.position.x - transform.position.x;
            spawnOffset = lookAt > 0 ? 1.7f : -1.7f;
            projectileDirection = new Vector3(transform.position.x + spawnOffset, transform.position.y, transform.position.z);
            if (lookAt > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (lookAt < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            animator.SetBool("ThrowKnife", true);
        }
        else 
        {
            animator.SetBool("ThrowKnife", true);
        }
        
    }

    public void StopSwordAtks() 
    {
        attacks[1].enabled = false;
        isAttacking = false;
    }

    public void SetIsAttakingTrue() 
    {
        isAttacking = true;
    }

    public void SetIsAttackingFalse() 
    {
        isAttacking = false;
    }

    private void Shoot()
    {
        GameObject shockwave = Instantiate(ProjectilePrefab, projectileDirection, Quaternion.identity);
        shockwave.GetComponent<Projectiles>().SetDirection(direction);
    }

}
