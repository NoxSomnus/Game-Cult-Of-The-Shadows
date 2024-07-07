using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoubleBossDialogue : MonoBehaviour
{
    private bool isPlayerInrange;
    private int lineIndex;
    private float typingTime = 0.05f;

    [SerializeField] private Movement playerMovement;
    [SerializeField] private Enemy chloeStats;
    [SerializeField] private Enemy shadowStats;
    [SerializeField] private ChloeBehaviour chloeBehaviour;
    [SerializeField] private ShadowLordBehaviour shadowBehaviour;
    [SerializeField] private MoveToCorners moveToCorners;
    //[SerializeField] private CompositeCollider2D floorCollider;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private void Start()
    {
        chloeStats.enabled = false;
        shadowStats.enabled = false;
        chloeBehaviour.enabled = false;
        shadowBehaviour.enabled = false;
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
        dialoguePanel.SetActive(true);
        //windWall.GetComponent<Rigidbody2D>().gravityScale = 1;
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
            dialoguePanel.SetActive(false);
            // Volver a habilitar el movimiento del personaje principal
            playerMovement.GetComponent<Movement>().enabled = true;
            chloeStats.enabled = true;
            shadowStats.enabled = true;
            shadowBehaviour.enabled = true;
            chloeBehaviour.enabled = true;
            moveToCorners.enabled = true;
            //musicManager.BossMusic();


            /*PhysicsMaterial2D physicsMaterial = new PhysicsMaterial2D();
            physicsMaterial.friction = 0.0f; // Ajusta los valores según tus necesidades            
            floorCollider.sharedMaterial = physicsMaterial;*/
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

            //floorCollider.sharedMaterial = physicsMaterial;
            isPlayerInrange = true;
            StartDialogue();

        }
    }
}
