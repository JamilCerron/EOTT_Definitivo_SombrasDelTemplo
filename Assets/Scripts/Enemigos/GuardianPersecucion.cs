using UnityEngine;
using UnityEngine.AI;

public class GuardianPersecucion : MonoBehaviour
{
    private NavMeshAgent agente;
    public Transform jugador;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (jugador != null && Vector3.Distance(agente.destination, jugador.position) > 0.1f)
        {
            agente.SetDestination(jugador.position);
        }

        SubidonVelocidad();
    }

    void SubidonVelocidad()
    {
        if (jugador != null)
        {
            float distancia = Vector3.Distance(jugador.position, transform.position);

            if (distancia <= 6)
            {
                agente.speed = 30;
            }
            else
            {
                agente.speed = 5;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            var cabezazo = GetComponent<GuardianCabezazo>();
            if (cabezazo != null)
            {
                cabezazo.AttemptAttack();
            }
            else
            {
                Debug.LogWarning("GuardianCabezazo no está asignado.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            agente.isStopped = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            agente.isStopped = true;
        }
    }
}
