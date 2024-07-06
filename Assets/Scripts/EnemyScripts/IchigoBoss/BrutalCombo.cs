using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrutalCombo : MonoBehaviour
{
    public GameObject target;
    public Transform centerBattlefield;
    private Vector3 projectileDirection;
    public GameObject ProjectilePrefab;
    [SerializeField] IchigoAI ichigoIA;
    [SerializeField] private float lookAt;
    public Animator animator;
    public Rigidbody2D rigidbody2d;
    public float jumpOffset;
    public float spawnOffset;
    bool canLook = false;
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
        animator = GetComponent<Animator>();
        ichigoIA = GetComponent<IchigoAI>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        canLook = false;

    }

    public void GoToCenter() 
    {
        gameObject.transform.position = new Vector3(centerBattlefield.position.x, centerBattlefield.position.y, transform.position.z);
    }

    private void Update()
    {

        if (canLook) 
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
        }

        projectileDirection = new Vector3(transform.position.x, transform.position.y + spawnOffset, transform.position.z);
    }

    public void Teleport()
    {
        rigidbody2d.gravityScale = 0f;
        gameObject.transform.position = new Vector3(target.transform.position.x, transform.position.y + jumpOffset, transform.position.z);
    }

    public void RestoreGravity0()
    {
        rigidbody2d.gravityScale = 10f;

    }

    public void DarkCut0()
    {
        float offset;
        if (lookAt > 0)
            offset = 3.5f;
        else
            offset = -3.5f;

        gameObject.transform.position = new Vector3(target.transform.position.x + offset, transform.position.y, transform.position.z);
    }

    public void HighCut0() 
    {
        float offset;
        if (lookAt > 0)
            offset = 3.5f;
        else
            offset = -3.5f;
        gameObject.transform.position = new Vector3(target.transform.position.x + offset, transform.position.y, transform.position.z);
    }

    public void SetCanLookFalse() 
    {
        canLook = false;
    }

    public void SetCanLookTrue()
    {
        canLook = true;
    }

    

    public void AtkEnded0()
    {
        ichigoIA.isAttacking = false;
        this.enabled = false;
    }

    public void ShootDownShockwave0()
    {
        GameObject shockwave = Instantiate(ProjectilePrefab, projectileDirection, Quaternion.identity);
        shockwave.GetComponent<Projectiles>().SetDirectionShockwave();
    }

}
