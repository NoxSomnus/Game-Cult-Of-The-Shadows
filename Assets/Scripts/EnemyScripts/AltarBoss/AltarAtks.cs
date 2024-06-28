using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AltarAtks : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject[] AtkPositions;
    [SerializeField] private List<GameObject> AtkWarnings = new List<GameObject>();
    private bool isAttacking = false;
    private int atkDecision;
    private int spawnDecision;
    public AltarOfEvilScript altarScript;
    public RuntimeAnimatorController animatorController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        StartCoroutine(AltarTimer());
        animator.runtimeAnimatorController = animatorController;
        animator.SetTrigger("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            atkDecision = Random.Range(0, 2);
            if(atkDecision == 0) 
                StartCoroutine(SpawnWarning());

            if (atkDecision == 1)
                StartCoroutine(SpawnWarningLeftToRight());
        }
    }
    private IEnumerator SpawnWarning()
    {
        isAttacking = true;
        spawnDecision = Random.Range(0, 2);
        if (spawnDecision == 0)
        {
            foreach (var warnings in AtkWarnings)
            {
                if (AtkWarnings.IndexOf(warnings) % 2 == 0)
                {
                    warnings.gameObject.SetActive(true);
                }
            }
        }
        if (spawnDecision == 1)
        {
            foreach (var warnings in AtkWarnings)
            {
                if (AtkWarnings.IndexOf(warnings) % 2 == 1)
                {
                    warnings.gameObject.SetActive(true);
                }
            }
        }
        yield return new WaitForSeconds(3f);
        foreach (var warnings in AtkWarnings)
        {
            warnings.gameObject.SetActive(false);
        }
        StartCoroutine(SpawnAtks());
    }

    private IEnumerator SpawnWarningLeftToRight()
    {
        altarScript.gameObject.GetComponent<AltarSoundEffects>().RainOfChaos();
        isAttacking = true;
        foreach (var warnings in AtkWarnings)
        {
            warnings.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            warnings.gameObject.SetActive(false);
            if (warnings == AtkWarnings.LastOrDefault())
            {
                StartCoroutine(SpawnWarningRightToLeft());
            }
            else 
            {
                StartCoroutine(AtkSpawnDespawn(warnings));
            }

        }

    }

    private IEnumerator SpawnWarningRightToLeft()
    {
        for (var i = AtkWarnings.Count - 1; i >= 0; i--)
        {
            AtkWarnings[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            AtkWarnings[i].gameObject.SetActive(false);
            StartCoroutine(AtkSpawnDespawn(AtkWarnings[i]));
        }

    }

    private IEnumerator AtkSpawnDespawn(GameObject warnings) 
    {
        AtkPositions[AtkWarnings.IndexOf(warnings)].gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        AtkPositions[AtkWarnings.IndexOf(warnings)].gameObject.SetActive(false);
    }

    private IEnumerator SpawnAtks()
    {
        if (spawnDecision == 0)
        {           
            for (int i = 0; i < AtkPositions.Length; ++i)
            {
                if (i % 2 == 0)
                {
                    AtkPositions[i].gameObject.SetActive(true);
                }
            }
        }
        if (spawnDecision == 1)
        {
            for (int i = 0; i < AtkPositions.Length; ++i)
            {
                if (i % 2 == 1)
                {
                    AtkPositions[i].gameObject.SetActive(true);
                }
            }
        }
        yield return new WaitForSeconds(3f);
        isAttacking = false;
        foreach (var atks in AtkPositions)
        {
            atks.gameObject.SetActive(false);
        }
    }

    private IEnumerator AltarTimer()
    {
        this.enabled = true;
        yield return new WaitForSeconds(15f);
        animator.SetTrigger("Despawn");
        altarScript.TriggerInvocation = false;
        altarScript.chloeOnPosition = false;
        altarScript.shadowOnPosition = false;
        isAttacking = false;
        this.enabled = false;
    }
}
