using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IchigoAirShockwave : MonoBehaviour
{
    public GameObject target;
    private Vector3 projectileDirection;
    public GameObject ProjectilePrefab;
    [SerializeField] IchigoAI ichigoIA;
    [SerializeField] private float lookAt;
    public Animator animator;
    private Rigidbody2D rigidbody2d;
    public float jumpOffset;
    public float spawnOffset;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player 1.1");
        animator = GetComponent<Animator>();
        ichigoIA = GetComponent<IchigoAI>();
        rigidbody2d = GetComponent<Rigidbody2D>();
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
        animator.SetTrigger("VanishJump");

    }

    private void Update()
    {
        projectileDirection = new Vector3(transform.position.x, transform.position.y + spawnOffset, transform.position.z);
    }

    public void Telepor() 
    {
        rigidbody2d.gravityScale = 0f;
        gameObject.transform.position = new Vector3(target.transform.position.x, transform.position.y + jumpOffset, transform.position.z);
    }

    public void RestoreGravity() 
    {
        rigidbody2d.gravityScale = 10f;

    }

    public void AtkEnded() 
    {
        ichigoIA.isAttacking = false;
        this.enabled = false;
        animator.SetTrigger("AtkEnded");
    }

    public void ShootDownShockwave() 
    {
        GameObject shockwave = Instantiate(ProjectilePrefab, projectileDirection, Quaternion.identity);
        shockwave.GetComponent<Projectiles>().SetDirectionShockwave();
    }
}
