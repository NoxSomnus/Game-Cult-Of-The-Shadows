using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ApollyonOblivion : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] attacks;
    public AltarOfEvilScript altarScript;
    public GameObject player;
    [SerializeField] private float timeBetweenAtks;
    public RuntimeAnimatorController animatorController;
    private float time;
    public GameObject Apollyon;
    private int atkDecision;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player 1.1");
    }
    private void OnEnable()
    {
        StartCoroutine(AltarTimer());
        Apollyon.GetComponent<Animator>().SetTrigger("Spawn");
    }

    // Update is called once per frame
    void Update()
    {

        time -= timeBetweenAtks * Time.deltaTime;
        if (time < 0)
        {
            time = timeBetweenAtks;
            atkDecision = Random.Range(0, attacks.Length);            
            GameObject atk = Instantiate(attacks[atkDecision], player.transform.position, Quaternion.identity);
        }

    }
    private IEnumerator AltarTimer()
    {
        this.enabled = true;
        yield return new WaitForSeconds(15f);
        Apollyon.GetComponent<Animator>().SetTrigger("Despawn");
        altarScript.TriggerInvocation = false;
        altarScript.chloeOnPosition = false;
        altarScript.shadowOnPosition = false;
        this.enabled = false;
    }
}
