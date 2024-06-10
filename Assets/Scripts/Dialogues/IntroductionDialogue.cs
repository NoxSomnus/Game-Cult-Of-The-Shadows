using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroductionDialogue : MonoBehaviour
{
    private bool isPlayerInrange = true;
    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;


    //[SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private void Start()
    {
        isPlayerInrange = true;
        StartDialogue();
    }

    void Update()
    {
        if (isPlayerInrange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueText.text == dialogueLines[lineIndex] && Input.GetKeyDown(KeyCode.E))
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
        //dialoguePanel.SetActive(true);
        lineIndex = 0;
        //Time.timeScale = 0f;
        // Deshabilitar el movimiento del personaje principal
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            //dialoguePanel.SetActive(false);
            // Volver a habilitar el movimiento del personaje principal
            Destroy(gameObject);

        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

}
