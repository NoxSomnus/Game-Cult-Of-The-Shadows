using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstBossDefeated : MonoBehaviour
{
    public GameObject forestBossPrefab;
    private GameObject forestBoss;
    [SerializeField] private Movement playerMovement;
    // Start is called before the first frame update
    private bool isPlayerInrange;
    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;
    [SerializeField] private CompositeCollider2D floorCollider;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

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
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        //Time.timeScale = 0f;
        // Deshabilitar el movimiento del personaje principal
        playerMovement.animator.SetBool("Running", false);
        playerMovement.animator.SetBool("Sprint", false);
        playerMovement.enabled = false;

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

            PhysicsMaterial2D physicsMaterial = new PhysicsMaterial2D();
            physicsMaterial.friction = 0.0f; // Ajusta los valores según tus necesidades

            floorCollider.sharedMaterial = physicsMaterial;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PhysicsMaterial2D physicsMaterial = new PhysicsMaterial2D();
            physicsMaterial.friction = 50f; // Ajusta los valores según tus necesidades

            floorCollider.sharedMaterial = physicsMaterial;
            isPlayerInrange = true;
            StartDialogue();
            GameObject forestBoss = Instantiate(forestBossPrefab,
                 new Vector2(playerMovement.transform.position.x + 15f, playerMovement.transform.position.y + 4), Quaternion.identity);

        }
    }
}
