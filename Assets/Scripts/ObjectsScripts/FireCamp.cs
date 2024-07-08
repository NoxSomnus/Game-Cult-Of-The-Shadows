using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireCamp : MonoBehaviour
{
    [SerializeField] private Parameters parameters;
    private bool isPlayerInRange;
    public bool isResting = false;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject playerMovement;
    [SerializeField] private GameObject pressE;
    [SerializeField] public List<GameObject> objectsToSave;
    [SerializeField] public List<SceneObjectData> sceneObjects;
    [SerializeField] public List<string> StingobjectTagToSave;
    [SerializeField] public GameSaveData gameSaveData;

    public PlayerData playerData;

    private void Awake()
    {
        objectsToSave = new List<GameObject>();
        sceneObjects = new List<SceneObjectData>();

        FindObjectstoSave();
    }


    void Start()
    {
        animator = GetComponent<Animator>();
        string currentSceneId = SceneManager.GetActiveScene().name;
        updateSceneData(SaveManager.LoadSceneObjectData());
        playerMovement = GameObject.Find("Player 1.1");

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

    public void RestAndSave() // animacioines
    {

        pressE.SetActive(false);
        playerMovement.GetComponent<Animator>().SetBool("Rest", true);
        isResting = true;
        playerMovement.GetComponent<Movement>().enabled = false;
        playerMovement.GetComponent<Parameters>().health = 100;
        Debug.Log("desconso");

    }

    public IEnumerator Wait()
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
        SaveManager.SavePlayerData(SavePlayerData());
        string sceneId = SceneManager.GetActiveScene().name;
        GetObjectDatatoSave(objectsToSave); // saco los datos que me interezan del objeto, 
        //sceneObjects = SaveManager.updateSceneData(sceneObjects); // actualizo, con respectoa  los datos que estan guardados
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

    #region SavePlayer
    public GameSaveData SavePlayerData()
    {
        // Datos en el archivo
        GameSaveData data = SaveManager.LoadPlayerData(parameters); 

        //datos actuales del juego
        playerData = new PlayerData(parameters, SceneManager.GetActiveScene().name);
            
        if (data.playerDataList.Count == 0) // agrega los datos atuales si no hay y  guarda
        {
            Debug.Log(" no hay nada pa guardado");
            data.playerDataList.Add(playerData);
            //GameSaveData = data;
           // return GameSaveData;
        }
        else // si hay, actualiza los datos
        {
            Debug.Log("hay cositas");

            data = UpdateDataPlayer(playerData, data);

            //return GameSaveData;
        }
        gameSaveData = data;
        return data;

    }

    public GameSaveData UpdateDataPlayer( PlayerData pd, GameSaveData data)
    {
        bool find = false;
        GameSaveData updateData = data; //datos guardados del archivo
        foreach ( PlayerData pdata in  updateData.playerDataList ) // itera
        {
            Debug.Log("veamos que hay");
            if (pdata.sceneId == SceneManager.GetActiveScene().name)// si consigue la scena en el archivo
            {
                pdata.position = pd.position;
                pdata.soul = pd.soul;
                updateData.soulFragments = parameters.soulFragments;
                find = true;
                Debug.Log("eureca");

                break;
               
            }
            
        }
        if (!find)
        {
            Debug.Log("no habia, uno nuevo");

            updateData.playerDataList.Add(pd);
            updateData.soulFragments = parameters.soulFragments;
        }
        gameSaveData = updateData;
        return updateData;
    }
    #endregion
    #region SaveObject

    private void FindObjectstoSave()
    {
        foreach (string tag in StingobjectTagToSave)
        {
            GameObject[] windDoorObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in windDoorObjects)
            {
                objectsToSave.Add(obj);
            }
        }
    }
    private void GetObjectDatatoSave(List<GameObject> objects)
    {
        Debug.Log("GetObjectDatatoSave");

        foreach (GameObject obj in objects)
        {
            Debug.Log("foreach del get");
            bool objectExists = false;

            foreach (SceneObjectData data in sceneObjects)
            {
                if (obj.name == data.name && obj.scene.name == data.sceneId && obj.GetInstanceID() == data.objectID)
                {
                    objectExists = true;
                    data.enable = obj.GetComponent<enabled>().activao;
                    break;
                }
            }

            if (!objectExists)
            {
                SceneObjectData sceneObjectData = new SceneObjectData(obj);
                sceneObjects.Add(sceneObjectData);
                Debug.Log("Obtuve datos por primera vez");
            }
            else
            {
                Debug.Log($"El objeto {obj.name} ya existe en la lista.");
                
            }
        }
        
    }
    public void updateSceneData(List<SceneObjectData> objectData)
        {
            // Cargar los datos existentes del archivo

            foreach (SceneObjectData savedSceneObject in objectData)
            {
                foreach (GameObject currentSceneObject in objectsToSave)
                {
                    if (savedSceneObject.sceneId == currentSceneObject.scene.name &&
                        savedSceneObject.name == currentSceneObject.name &&
                        savedSceneObject.objectID == currentSceneObject.GetInstanceID() && savedSceneObject != null)
                    {
                        currentSceneObject.GetComponent<enabled>().activao = savedSceneObject.enable;
                    }
                }
            }


        }
    #endregion
}
