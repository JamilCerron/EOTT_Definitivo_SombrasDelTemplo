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
        if (collision.gameObject.CompareTag("Jugador")) 
        {
            espectroAtaque.AtaqueBasico();
            Destroy(gameObject); 
        }
    }
}
