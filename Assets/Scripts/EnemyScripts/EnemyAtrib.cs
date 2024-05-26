using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAtrib : MonoBehaviour
{
    public float HP;
    public float damage;
    public EnemyMove movement;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }
    public void Hit(int dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

}
