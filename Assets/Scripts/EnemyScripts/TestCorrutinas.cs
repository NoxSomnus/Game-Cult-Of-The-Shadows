using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TestCorrutinas : MonoBehaviour
{

    public GameObject target;
    public float range;
    public float timer;
    public Animator animator;
    public float runSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bossmove(5f));
    }

    // Update is called once per frame
    IEnumerator Bossmove( float timeBetwenMove)
    {
        while (true)
        {
            print("a");
            range = target.transform.position.x - transform.position.x;
            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, runSpeed * Time.deltaTime);
            if (range > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }
            yield return new WaitForSeconds(timer);
            yield return Bossmove(timeBetwenMove); 
        }
    }
}
