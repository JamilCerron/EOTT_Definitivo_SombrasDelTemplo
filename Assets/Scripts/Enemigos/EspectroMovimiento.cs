using System.Collections;
using UnityEngine;

public class EspectroMovimiento : MonoBehaviour
{
    [Header("Rango")]
    private Transform jugador; // Referencia al jugador
    [SerializeField] private PlayerStats playerStats;
    [Range(0f, 10f)]
    [SerializeField] private float rangoPersecucion = 5f; // Rango de 

    [Header("Velocidad")]
    [SerializeField] private float velocidad = 5f; // Velocidad normal del espectro

    [SerializeField] private float velocidadActual; // Velocidad modificada por el crucifijo
    private Rigidbody rb;
    private bool afectadoPorCrucifijo = false; // Indica si está bajo el efecto del crucifijo

    [SerializeField] private GameObject rangoCordura;

    [SerializeField] private float tiempoEntreReducciones = 2f;
    private bool enCooldown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocidadActual = velocidad; // Inicialmente, la velocidad actual es la normal
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerStats == null)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        }
    }

    void Update()
    {
        Movimiento();
    }

    void Movimiento()
    {
        float distanciaJugador = Vector3.Distance(transform.position, jugador.position);

        // Si el espectro no está afectado por el crucifijo, continía moviéndose normalmente
        if (distanciaJugador < rangoPersecucion)
        {
            if (distanciaJugador < rangoPersecucion)
            {
                PerseguirJugador();
            }
        }
    }

    void PerseguirJugador()
    {
        Vector3 direccion = (jugador.position - transform.position).normalized;
        rb.velocity = direccion * velocidadActual;
    }

    public bool AfectadoPorCrucifijo()
    {
        return afectadoPorCrucifijo;
    }

    // Metodo llamado por el crucifijo para reducir la velocidad
    public void ReducirVelocidad(float cantidad)
    {
        if (afectadoPorCrucifijo == true)
        {
            velocidadActual -= cantidad;

            // La velocidad no puede ser negativa
            if (velocidadActual <= 0)
            {
                velocidadActual = 0;
            }
        }
    }

    // Metodo que vuelve la velocidad a la normal despu�s de que el crucifijo deja de apuntar
    public void RestaurarVelocidad()
    {
        afectadoPorCrucifijo = false;
        velocidadActual = velocidad;
    }

    IEnumerator ReducirCorduraConCooldown()
    {
        enCooldown = true;
        playerStats.ReducirCordura(1f);  // Reduce cordura
        yield return new WaitForSeconds(tiempoEntreReducciones);  // Espera antes de volver a reducir
        enCooldown = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !enCooldown)
        {
            StartCoroutine(ReducirCorduraConCooldown());
        }
    }
}