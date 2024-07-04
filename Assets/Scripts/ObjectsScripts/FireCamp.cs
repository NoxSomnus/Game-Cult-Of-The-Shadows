using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireCamp : MonoBehaviour
{

    private bool isPlayerInRange;
    public bool isResting = false;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject playerMovement;
    [SerializeField] private GameObject pressE;
    [SerializeField] public List<GameObject> objectsToSave;
    [SerializeField] public List<SceneObjectData> sceneObjects;

    public PlayerData playerData;

    private void Awake()
    {
        objectsToSave = new List<GameObject>();
        FindWindDoorObjects();
    }


    void Start()
    {
        animator = GetComponent<Animator>();
        string currentSceneId = SceneManager.GetActiveScene().name;


    }

    // Update is called once per frame
    void Update()
    {


        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isResting)
        {
            RestAndSave();
            SaveisPlayerRest();
        }
        else if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && isResting)
        {
            StartCoroutine(Wait());

        }


    }

    public void RestAndSave()
    {

        pressE.SetActive(false);
        playerMovement.GetComponent<Animator>().SetBool("Rest", true);
        isResting = true;
        playerMovement.GetComponent<Movement>().enabled = false;
        playerMovement.GetComponent<Parameters>().health = 100;
        Debug.Log("desconso");

    }

    private IEnumerator Wait()
    {
        playerMovement.GetComponent<Animator>().SetBool("Rest", false);
        yield return new WaitForSeconds(1.7f); // Espera a que termine la animación de ataque
        playerMovement.GetComponent<Movement>().enabled = true;
        isResting = false;
        pressE.SetActive(false);
        isPlayerInRange = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pressE.SetActive(true);
            isPlayerInRange = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pressE.SetActive(false);
            isPlayerInRange = false;
        }

    }
    public void SaveisPlayerRest()
    {
        playerData = new PlayerData(playerMovement.GetComponent<Parameters>());
        SaveManager.SavePlayerData(playerMovement.GetComponent<Parameters>());
        string sceneId = SceneManager.GetActiveScene().name;
        GetObjectDatatoSave(objectsToSave); // saco los datos que me interezan del objeto, 
        sceneObjects = SaveManager.updateSceneData(sceneObjects); // actualizo, con respectoa  los datos que estan guardados
        SaveManager.SaveObjectData(sceneObjects); // guardo los datos 
        Debug.Log("Datos de jugador y ezena guadados");

    }

    private GameObject FindObjectInScene(string name)
    {
        // Busca el objeto por su nombre en la escena actual
        return GameObject.Find(name);
    }

    private void UpdateObjectState(GameObject existing, GameObject loaded)
    {
        // Copia los componentes y datos del objeto cargado al objeto existente
        existing.GetComponent<SpriteRenderer>().sprite = loaded.GetComponent<SpriteRenderer>().sprite;
        existing.transform.position = loaded.transform.position;
        // Actualiza más propiedades según sea necesario
    }

    #region SaveObject

    private void FindWindDoorObjects()
    {
        GameObject[] windDoorObjects = GameObject.FindGameObjectsWithTag("WindDoor");
        foreach (GameObject obj in windDoorObjects)
        {
            objectsToSave.Add(obj);
        }
    }
    private void  GetObjectDatatoSave(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            // Verificar si el objeto ya existe en la lista
            if (!sceneObjects.Exists(data => data.name == obj.name && 
                                             data.sceneId == obj.scene.name && 
                                             data.objectID == obj.GetInstanceID()))
            {
                SceneObjectData sceneObjectData = new SceneObjectData(obj);
                sceneObjects.Add(sceneObjectData);
                Debug.Log("Obtuve datos");
            }
            else
            {
                Debug.Log($"El objeto {obj.name} ya existe en la lista.");
            }
        }
    }
    private void loadObjectData()
    {
        List<SceneObjectData> savedObject = SaveManager.LoadSceneObjectData();
        foreach (GameObject sceneObject in objectsToSave)
        {
            foreach (SceneObjectData data in savedObject)
            {
                if (sceneObject.scene.name == data.sceneId && sceneObject.name == data.name)
                {
                    sceneObject.GetComponent<enabled>().activao = data.enable;
                }
            }
        }
    }
    #endregion
}
