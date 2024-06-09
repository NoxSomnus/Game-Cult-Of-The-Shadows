using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class FirstBossDialogue : MonoBehaviour
{
    private bool isPlayerInrange;
    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;
    public GameObject AuroraPrefab;

    [SerializeField] private float runSpeed;

    [SerializeField] private Movement playerMovement;
    [SerializeField] private Enemy forestBossStats;
    [SerializeField] private ForestBossMove forestBossBehaviour;
    [SerializeField] private GameObject redEye;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;


    private void Start()
    {
        forestBossStats.enabled = false;
        forestBossBehaviour.enabled = false;
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
                StartCoroutine(DespawnRedEye());
                forestBossBehaviour.enabled = true;
                forestBossStats.enabled = true;
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        //Time.timeScale = 0f;
        // Deshabilitar el movimiento del personaje principal
        playerMovement.animator.SetBool("Running", false);
        playerMovement.animator.SetBool("Sprint", false);
        playerMovement.enabled = false;
        GameObject Aurora = Instantiate(AuroraPrefab,
            new Vector2(playerMovement.transform.position.x - 3, playerMovement.transform.position.y), Quaternion.identity);
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
            dialoguePanel.SetActive(false);
            // Volver a habilitar el movimiento del personaje principal
            playerMovement.GetComponent<Movement>().enabled = true;
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

    private IEnumerator DespawnRedEye()
    {
        redEye.transform.position = Vector2.MoveTowards(redEye.transform.position,
            new Vector2(redEye.transform.position.x + 20, redEye.transform.position.y), runSpeed * Time.deltaTime);
        yield return new WaitForSecondsRealtime(10);
        Destroy(redEye);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInrange = true;
            StartDialogue();

        }
    }
}
