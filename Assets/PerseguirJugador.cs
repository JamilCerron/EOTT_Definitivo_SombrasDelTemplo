using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PerseguirJugador : MonoBehaviour
{
    public Transform jugador;
    private NavMeshAgent agente;
    private EspectroAtaque espectroAtaque;
    private bool atacando = false;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        espectroAtaque = GetComponent<EspectroAtaque>();
    }

    void Update()
    {
        if (jugador != null)
        {
            agente.SetDestination(jugador.position);

            if (!agente.pathPending && agente.remainingDistance <= agente.stoppingDistance && !atacando && Mathf.Abs(agente.transform.position.y - jugador.transform.position.y) <= 2.0f)
            {
                StartCoroutine(RealizarAtaque());
            }

        }
    }

    IEnumerator RealizarAtaque()
    {
        Debug.Log("Atacando");
        atacando = true;
        agente.isStopped = true; 

        yield return new WaitForSeconds(1f);
        espectroAtaque.AtaqueBasico();
        agente.isStopped = false; 
        atacando = false;
    }
}
