using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GuardianCabezazo : MonoBehaviour
{
    public float attackForce = 10f;
    private bool canAttack = false;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (canAttack)
        {
            Vector3 direction = (GameObject.FindWithTag("Jugador").transform.position - transform.position).normalized;
            direction.y = 0; // Ignora el eje Y para el empuje
            navMeshAgent.isStopped = true;
            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(direction * attackForce, ForceMode.Impulse);
            }

            canAttack = false;
            StartCoroutine(ResumeMovementAfterAttack());
        }
    }

    public void AttemptAttack()
    {
        if (!canAttack)
        {
            canAttack = true;
            navMeshAgent.isStopped = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            AttemptAttack();
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // Calcula la direcci�n de empuje solo en los ejes X y Z
                Vector3 pushDirection = (collision.transform.position - transform.position);
                pushDirection.y = 0; // Asegura que no haya empuje en el eje Y
                pushDirection.Normalize(); // Normaliza la direcci�n para que tenga una magnitud de 1

                // Aplica m�s fuerza para comprobar el efecto
                float enhancedAttackForce = attackForce * 2; // Aumenta temporalmente para verificar el empuje

                playerRb.AddForce(pushDirection * enhancedAttackForce, ForceMode.Impulse);
            }
        }
    }

    IEnumerator ResumeMovementAfterAttack()
    {
        yield return new WaitForSeconds(1f); // Ajusta el tiempo seg�n lo necesario
        navMeshAgent.isStopped = false;
    }
}

