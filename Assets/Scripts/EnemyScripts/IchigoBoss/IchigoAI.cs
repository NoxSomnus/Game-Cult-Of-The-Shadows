using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchigoAI : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private float atkRange;
    [SerializeField] private float distance;
    public Animator animator;
    public Enemy enemyStats;
    public int atkDecision;
    public float runSpeed;
    public bool isAttacking = false;
    private float lookAt;
    public MonoBehaviour[] attacks;
    public bool triggeredBrutalCombo = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player 1.1");
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<Enemy>();
        attacks[0] = GetComponent<IchigoChase>();
        attacks[1] = GetComponent<IchigoAirShockwave>();
        attacks[2] = GetComponent<IchigoCuts>();
        attacks[3] = GetComponent<IchigoDarkCut>();
        attacks[4] = GetComponent<BrutalCombo>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(target.transform.position, transform.position);

        if (enemyStats.Health <= 0)
        {
            this.enabled = false;
            for (var i = 0; i < attacks.Length; i++) 
            {
                attacks[i].enabled = false; 
            }

            animator.SetTrigger("Die");

        }

        if(enemyStats.Health <= 50 && !triggeredBrutalCombo) 
        {

            attacks[4].enabled = true;
            triggeredBrutalCombo = true;
            isAttacking = true;
        }


        if (!isAttacking)
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

            //decidir ataque
            if (distance > 20)
            {
                atkDecision = Random.Range(2, 4);
                isAttacking = true;
                attacks[3].enabled = true;

            }
            else 
            {
                atkDecision = Random.Range(0, 3);
                attacks[atkDecision].enabled = true;
                isAttacking = true;
            }               

        }
    }
}
