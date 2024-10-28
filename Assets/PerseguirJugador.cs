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
            
            if (agente.remainingDistance <= agente.stoppingDistance && !atacando)
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
