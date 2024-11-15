using Unity.VisualScripting;
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
        if (jugador != null)
        {
            agente.SetDestination(jugador.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            GetComponent<GuardianCabezazo>().AttemptAttack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Jugador"))
        {
            agente.isStopped = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("Jugador"))
        {
            agente.isStopped = true;
        }
    }
}
