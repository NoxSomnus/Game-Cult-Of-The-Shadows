using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedOrbsFolllowsPlayer : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private float runSpeed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player 1.1");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, runSpeed * Time.deltaTime);
    }
}
