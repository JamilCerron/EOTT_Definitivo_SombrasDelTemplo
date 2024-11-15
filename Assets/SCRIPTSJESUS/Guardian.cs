using UnityEngine;
using UnityEngine.AI;

public class Guardian : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 30f;
    public float attackRange = 10f;
    public Vector3 initialPosition;
    private GuardianCabezazo guardianCabezazo;
    private NavMeshAgent navMeshAgent;

    public enum GuardianState { Idle, Chasing, Returning, Attacking }
    public GuardianState currentState = GuardianState.Idle;

    private float timeSincePlayerLost = 0f;
    public float timeBeforeReturning = 2f;  // Tiempo de espera antes de que regrese a la posición inicial

    void Start()
    {
        initialPosition = transform.position;
        guardianCabezazo = GetComponent<GuardianCabezazo>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Asegúrate de que el NavMeshAgent esté habilitado y configurado correctamente.
        navMeshAgent.isStopped = true; // Empieza detenido.
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case GuardianState.Idle:
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = GuardianState.Chasing;
                    navMeshAgent.isStopped = false;  // Permite que el agente se mueva.
                    Debug.Log("Entrando en rango de detección: Persiguiendo al jugador");
                    navMeshAgent.SetDestination(player.position);  // Comienza a moverse hacia el jugador.
                }
                break;

            case GuardianState.Chasing:
                if (distanceToPlayer <= attackRange)
                {
                    currentState = GuardianState.Attacking;
                    navMeshAgent.isStopped = true;  // Detiene el movimiento.
                    Debug.Log("Entrando en rango de ataque: Iniciando ataque");
                    guardianCabezazo.AttemptAttack();
                }
                else if (distanceToPlayer > detectionRange)
                {
                    // Si el jugador se aleja, empezamos a contar el tiempo antes de que el guardián regrese.
                    timeSincePlayerLost += Time.deltaTime;
                    if (timeSincePlayerLost >= timeBeforeReturning)
                    {
                        currentState = GuardianState.Returning;
                        navMeshAgent.SetDestination(initialPosition);  // Dirige el agente hacia la posición inicial.
                        Debug.Log("Jugador fuera de rango durante 2 segundos: Regresando a posición inicial");
                    }
                }
                else
                {
                    // Si el jugador sigue dentro del rango de detección, sigue persiguiéndolo.
                    timeSincePlayerLost = 0f;  // Resetea el temporizador si el jugador está cerca.
                    navMeshAgent.SetDestination(player.position);  // Sigue al jugador.
                }
                break;

            case GuardianState.Attacking:
                if (distanceToPlayer > attackRange)
                {
                    currentState = GuardianState.Chasing;
                    navMeshAgent.isStopped = false;  // Permite que el agente se mueva.
                    Debug.Log("Fuera de rango de ataque: Volviendo a perseguir al jugador");
                    navMeshAgent.SetDestination(player.position);  // Sigue al jugador.
                }
                break;

            case GuardianState.Returning:
                if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
                {
                    currentState = GuardianState.Idle;
                    navMeshAgent.isStopped = true;  // Detiene el agente al llegar a la posición inicial.
                    Debug.Log("Regresado a posición inicial: Estado Idle");
                }
                else
                {
                    // Si el jugador vuelve a entrar en el rango mientras regresa, reinicia el proceso de persecución.
                    if (distanceToPlayer <= detectionRange)
                    {
                        currentState = GuardianState.Chasing;
                        navMeshAgent.isStopped = false;  // Reinicia el movimiento hacia el jugador.
                        Debug.Log("Jugador dentro de rango mientras regresa: Volviendo a perseguir al jugador");
                        navMeshAgent.SetDestination(player.position);
                    }
                }
                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
