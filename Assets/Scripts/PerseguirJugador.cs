using UnityEngine;
using UnityEngine.AI;

public class PerseguirJugador : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public float distanciaCriterio = 10f; // Distancia para activar aumento de velocidad
    private NavMeshAgent agente;
    private EspectroAtaque espectroAtaque;
    private float velocidadBase;
    private float tiempoAcumulado = 0f; // Temporizador para incremento de velocidad

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        espectroAtaque = GetComponent<EspectroAtaque>();
        velocidadBase = agente.speed; // Guardar la velocidad inicial
    }

    void Update()
    {
        if (jugador != null)
        {
            // Calcular la distancia al jugador
            float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

            // Continuar persiguiendo al jugador
            agente.SetDestination(jugador.position);

            // Aumentar velocidad si está dentro del rango
            if (distanciaAlJugador <= distanciaCriterio)
            {
                tiempoAcumulado += Time.deltaTime;
                if (tiempoAcumulado >= 5f)
                {
                    agente.speed += 0.5f; // Incrementar velocidad
                    tiempoAcumulado = 0f; // Reiniciar temporizador
                }
            }
            else
            {
                // Si está fuera del rango, restablecer temporizador (pero no detenerse)
                tiempoAcumulado = 0f;
                 // Restaurar velocidad base
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crucifijo"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            espectroAtaque.AtaqueBasico();
            Destroy(gameObject);
        }
    }

    // Dibujar un gizmo para visualizar el rango en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Color del gizmo
        Gizmos.DrawWireSphere(transform.position, distanciaCriterio); // Dibuja un círculo en el rango
    }
}
