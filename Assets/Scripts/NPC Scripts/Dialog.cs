using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class Dialog : MonoBehaviour
{
    private bool isPlayerInrange;
    private bool didDialogueStart;
    private bool completed;
    private int lineIndex;
    private float typingTime = 0.05f;

    [SerializeField] private GameObject playerMovement;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialogueMarkOn;
    [SerializeField] private GameObject dialogueMarkOff;
    [SerializeField] private GameObject PressE ;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    
    void Update()
    {
        if (isPlayerInrange && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue(); 
            }
            else if(dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }
    
    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMarkOn.SetActive(false);
        dialogueMarkOff.SetActive(true);
        PressE.SetActive(false);
        lineIndex = 0;
        //Time.timeScale = 0f;
        // Deshabilitar el movimiento del personaje principal
        playerMovement.GetComponent<Movement>().enabled = false;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            completed = true;
            // Time.timeScale = 1f;
            // Volver a habilitar el movimiento del personaje principal
            playerMovement.GetComponent<Movement>().enabled = true;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInrange = true;
            dialogueMarkOn.SetActive(false);
            dialogueMarkOff.SetActive(false);
            PressE.SetActive(true);
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInrange = false;
            PressE.SetActive(false);

            if (completed == false)
            {
                dialogueMarkOn.SetActive(true);
            }
            else
                dialogueMarkOff.SetActive(true);
        }
    }


}
