using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnAtks : MonoBehaviour
{
    [SerializeField] private GameObject[] attacks;
    [SerializeField] private float waitTime;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform floor;
    private Vector3 floorPosition;
    private Enemy enemyStats;
    private int atk;
    private bool isWaiting;
    [SerializeField] public Animator animator;
    private PatrolAi patrolAi;
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<Enemy>();
        patrolAi = GetComponent<PatrolAi>();
        floorPosition = floor.transform.position;
        player = GameObject.Find("Player 1.1");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isWaiting)
        {
            StartCoroutine(Attack());
        }

        if (enemyStats.Health <= 0) 
        {
            animator.SetTrigger("Die");
            this.enabled = false;
            patrolAi.enabled = false;
        }

    }

    private IEnumerator Attack()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        atk = Random.Range(0, attacks.Length);
        animator.SetTrigger("CastAtk");
        switch (atk)
        {
            case 0:
                SpawnAtk0();
                break;
            case 1:
                SpawnAtk1();
                break;
            case 2:
                SpawnAtk2();
                break;
        }

        isWaiting = false;
    }

    private void SpawnAtk0()
    {
        GameObject atk = Instantiate(attacks[0],player.transform.position, Quaternion.identity);
    }

    private void SpawnAtk1() 
    {
        GameObject atk = Instantiate(attacks[1], 
            new Vector2(player.transform.position.x, floorPosition.y + 7.7f), Quaternion.identity);
    }

    private void SpawnAtk2()
    {
        GameObject atk = Instantiate(attacks[2],
            new Vector2(player.transform.position.x, floorPosition.y + 4f), Quaternion.identity);
    }
}
