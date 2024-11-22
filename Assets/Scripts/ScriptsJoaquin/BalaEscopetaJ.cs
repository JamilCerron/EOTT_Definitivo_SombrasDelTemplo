using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEscopetaJ : MonoBehaviour
{
    private Rigidbody rb;
    [Header("Atributos")]
    [SerializeField] private float speed = 20f; // Velocidad de la bala
    [SerializeField] private Vector3 direction; // Dirección de la bala
    [SerializeField] private float tiempoDeVida = 5f; // Tiempo máximo antes de destruir la bala automáticamente

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Destruye la bala automáticamente después de cierto tiempo
        Destroy(gameObject, tiempoDeVida);
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Suelo") || collision.gameObject.CompareTag("Muro") || collision.gameObject.CompareTag("Techo"))
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"La bala chocó con un objeto inesperado: {collision.gameObject.name}");
        }
    }
}
