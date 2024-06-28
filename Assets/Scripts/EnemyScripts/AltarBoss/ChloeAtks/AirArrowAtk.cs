using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirArrowAtk : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private GameObject[] atkPositions;
    [SerializeField] private float runSpeed;
    [SerializeField] private float atkRange;
    [SerializeField] private float lookAt;
    public Animator animator;
    private int cornerDecision = 3;
    private Rigidbody2D rgd;
    private bool isOnArrowPosition = false;
    private bool isOnAirArrowPosition = false;
    [SerializeField]private ChloeBehaviour chloeBehaviour;
    public bool atkIsDone;

    private Vector3 projectileDirection;
    public GameObject ProjectilePrefab;
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player 1.1");
        rgd = GetComponent<Rigidbody2D>();
        chloeBehaviour = GetComponent<ChloeBehaviour>();

    }

    private void OnEnable()
    {
        atkIsDone = false;
    }

    // Update is called once per frame
    void Update()
    {        
        if (lookAt > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (lookAt < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (!atkIsDone)
        {
            if (!isOnArrowPosition)
            {
                lookAt = atkPositions[0].transform.position.x - transform.position.x;
                animator.SetBool("Run", true);
                MoveToArrowPosition();
            }
            else
            {
                lookAt = target.transform.position.x - transform.position.x;
                if (!isOnAirArrowPosition)
                {

                    animator.SetBool("Run", false);
                    Jump();
                }
                StartCoroutine(ArrowLoop());
            }
        }
        else 
        {
            MoveToCornerPosition();
        }

    }

    private void MoveToCornerPosition() 
    {
        lookAt = atkPositions[cornerDecision].transform.position.x - transform.position.x;
        animator.SetBool("Run", true);
        transform.position = Vector3.MoveTowards(transform.position, atkPositions[cornerDecision].transform.position, runSpeed * Time.deltaTime);
    }

    private void MoveToArrowPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, atkPositions[0].transform.position, runSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        animator.SetBool("Jump", true);
        rgd.gravityScale = 0;
        transform.position = Vector3.MoveTowards(transform.position, atkPositions[1].transform.position, runSpeed * 2 * Time.deltaTime);

    }

    private IEnumerator ArrowLoop()
    {

        animator.SetTrigger("AirArrow");
        yield return new WaitForSeconds(2f);
        ChangeGravity();
    }

    private void ChangeGravity()
    {
        animator.SetBool("Jump", false);
        animator.SetTrigger("HeavyAirAtk");
        rgd.gravityScale = 5;
        isOnAirArrowPosition = false;
        atkIsDone = true;
    }

    private void ShootArrow()
    {
        projectileDirection = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Crea una rotación en 2D solo en el eje Z
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        GameObject shockwave = Instantiate(ProjectilePrefab, projectileDirection, rotation);
        shockwave.GetComponent<Projectiles>().SetDirectionArrow(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ArrowPosition")
        {
            isOnArrowPosition = true;
        }

        if (collision.gameObject.name == "AirArrowPosition")
        {
            isOnAirArrowPosition = true;
        }

        if (collision.tag == "AtkFinished" && atkIsDone) 
        {
            animator.SetBool("Run", false);
            chloeBehaviour.isAttacking = false;
            isOnArrowPosition = false;
            this.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AirArrowPosition")
        {
            isOnAirArrowPosition = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {

            animator.SetTrigger("Ground");
            cornerDecision = Random.Range(2, atkPositions.Length);

        }
    }

}
