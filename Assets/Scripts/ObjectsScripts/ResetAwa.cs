using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public Transform respawPos;
    public Quaternion respawRot;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player 1.1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            respawPos = collision.GetComponent<Transform>();
        }

        if (collision.gameObject.CompareTag("falldamage"))
        {
            transform.position = respawPos.position;
        }
    }
}