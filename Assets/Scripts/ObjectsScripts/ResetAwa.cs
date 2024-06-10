using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetAwa : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform teleportLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.gameObject);
        }
    }

    private void TeleportPlayer(GameObject playerObject)
    {
        // Obtener la escena actual
        Scene currentScene = SceneManager.GetActiveScene();

        // Verificar si el jugador está en la misma escena que este script
        if (currentScene.name == playerObject.scene.name)
        {
            // Teletransportar al jugador a la ubicación establecida
            playerObject.transform.position = teleportLocation.position;
        }
        else
        {
            // Cargar la escena donde está el jugador y luego teletransportarlo
            SceneManager.LoadScene(playerObject.scene.name, LoadSceneMode.Additive);
            playerObject.transform.position = teleportLocation.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player 1.1");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
