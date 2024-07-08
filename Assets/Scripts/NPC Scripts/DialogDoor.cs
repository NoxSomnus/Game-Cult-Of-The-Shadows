using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class DialogDoor : MonoBehaviour
{
    private bool isPlayerInrange;
    private bool didDialogueStart;
    private bool completed;
    private int lineIndex;
    private float typingTime = 0.05f;

  
    [SerializeField] private GameObject PressE ;
 
    
    void Update()
    {
        if (isPlayerInrange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue(); 
            }
        }
    }
    
    private void StartDialogue()
    {
        TransitionManager.Instance.LoadScene("Creditos");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInrange = true;
            PressE.SetActive(true);
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInrange = false;
            PressE.SetActive(false);

   
        }
    }


}
