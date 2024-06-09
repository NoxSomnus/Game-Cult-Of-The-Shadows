using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_RespawnZone : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private bool isPlayerInRespawnZone;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player 1.1");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRespawnZone &&( Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)))
        {
            Debug.Log("coito");
            StartCoroutine(GetUp());
        }
        else if (!isPlayerInRespawnZone)
        {
            gameObject.SetActive(false);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Animator>().SetBool("Rest", true);
            player.GetComponent<Movement>().enabled= false;
            isPlayerInRespawnZone = true;
        }
        else
        {
            isPlayerInRespawnZone = false;

        }
    }
    
    private IEnumerator GetUp()
    {

        player.GetComponent<Animator>().SetBool("Rest",false);
        yield return new WaitForSeconds(1.6f);

        player.GetComponent<Movement>().enabled = true;
        gameObject.SetActive(false);
        isPlayerInRespawnZone = false;
        Debug.Log("mondongo");
    }
}
