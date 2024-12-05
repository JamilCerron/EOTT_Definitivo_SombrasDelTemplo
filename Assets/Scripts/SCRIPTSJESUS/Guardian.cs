using UnityEngine;

public class Guardian : MonoBehaviour
{
    public Transform player;
    public float speed = 4f;
    public float detectionRange = 10f;
    public float attackRange = 5f;
    private Vector3 initialPosition;
    private GuardianCabezazo guardianCabezazo;

    private enum GuardianState { Idle, Chasing, Returning, Attacking }
    private GuardianState currentState = GuardianState.Idle;

    void Start()
    {
        initialPosition = transform.position;
        guardianCabezazo = GetComponent<GuardianCabezazo>();
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
                    Debug.Log("Entrando en rango de detecci�n: Persiguiendo al jugador");
                }
                break;

            case GuardianState.Chasing:
                if (distanceToPlayer <= attackRange)
                {
                    currentState = GuardianState.Attacking;
                    Debug.Log("Entrando en rango de ataque: Iniciando ataque");
                    guardianCabezazo.AttemptAttack();
                }
                else if (distanceToPlayer > detectionRange)
                {
                    currentState = GuardianState.Returning;
                    Debug.Log("Jugador fuera de rango: Regresando a posici�n inicial");
                }
                else
                {
                    MoveTowards(player.position);
                }
                break;

            case GuardianState.Attacking:
                if (distanceToPlayer > attackRange)
                {
                    currentState = GuardianState.Chasing;
                    Debug.Log("Fuera de rango de ataque: Volviendo a perseguir al jugador");
                }
                break;

            case GuardianState.Returning:
                if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
                {
                    currentState = GuardianState.Idle;
                    Debug.Log("Regresado a posici�n inicial: Estado Idle");
                }
                else
                {
                    MoveTowards(initialPosition);
                }
                break;
        }

    }

    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
