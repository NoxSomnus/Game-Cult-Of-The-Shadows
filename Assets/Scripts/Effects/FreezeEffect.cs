using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class FreezeEffect : MonoBehaviour
{
    public float freezeTime;
    public List<GameObject> currentCollisions = new List<GameObject>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentCollisions.Add(collision.gameObject);
            StartCoroutine(FreezePosition());
        }
    }

    private IEnumerator FreezePosition()
    {

        foreach (var enemy in currentCollisions)
        {
            Debug.Log("Estoy en el for antes del yield");
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            enemy.GetComponent<Animator>().enabled = false;
        }
        yield return new WaitForSeconds(2f);
        Debug.Log("Estoy despues del yield");
        foreach (var enemy in currentCollisions)
        {
            Debug.Log("Estoy en el for despues del yield");
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            enemy.GetComponent<Animator>().enabled = false;
            currentCollisions.Remove(enemy);
        }
    }
}
