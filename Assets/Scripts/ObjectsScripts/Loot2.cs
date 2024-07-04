using UnityEngine;

public class Loot2 : MonoBehaviour
{
    [SerializeField] private float minVerticalForce = 5f;
    [SerializeField] private float maxVerticalForce = 10f;
    [SerializeField] private float minHorizontalForce = -5f;
    [SerializeField] private float maxHorizontalForce = 5f;
    [SerializeField] private float maxImpactForce = 20f;
    [SerializeField] private float soulFragments;
    [SerializeField] private Parameters parameters;
    [SerializeField] private Puntaje puntaje;

    private Rigidbody2D itemRb;

    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        parameters = GameObject.Find("Player 1.1").GetComponent<Parameters>();
        puntaje = GameObject.Find("Text (TMP)").GetComponent<Puntaje>();
        // Aplicar fuerza horizontal y vertical aleatorias
        float verticalForce = Random.Range(minVerticalForce, maxVerticalForce);
        float horizontalForce = Random.Range(minHorizontalForce, maxHorizontalForce);
        itemRb.AddForce(new Vector2(horizontalForce, verticalForce), ForceMode2D.Impulse);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la fuerza de impacto es demasiado alta
        if (collision.relativeVelocity.magnitude > maxImpactForce)
        {
            // Reducir la fuerza de impacto a un nivel aceptable
            itemRb.velocity = collision.relativeVelocity.normalized * maxImpactForce;
            
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Detiene el movimiento del objeto
            itemRb.velocity = Vector2.zero;
            itemRb.angularVelocity = 0f;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            parameters.AddFragments(soulFragments);
            puntaje.updateFragmentsUI(soulFragments);
            Destroy(gameObject);
        }
    }
}

