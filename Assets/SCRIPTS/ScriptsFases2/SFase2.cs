using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SFase2 : MonoBehaviour
{
    [SerializeField] private float damage = 20f; // Daño de Supay en Fase 2
    [SerializeField] private float meleeRange = 3f; // Rango para infligir daño cuerpo a cuerpo
    [SerializeField] private float jumpHeight = 5f; // Altura de salto
    private Transform player; // Referencia al jugador
    private NavMeshAgent navMeshAgent; // Agente de NavMesh para el movimiento
    private Rigidbody rb;
    private bool isJumping = false; // Controla si el Supay está saltando

   
    void Start()
    {
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void SetNavMeshAgent(NavMeshAgent agent)
    {
        navMeshAgent = agent;
    }




    void Update()
    {
        if (player != null)
        {
            // Persigue al jugador lentamente
            navMeshAgent.SetDestination(player.position);

            // Verificar si está dentro del rango de melee
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= meleeRange)
            {
                player.GetComponent<JugadorHealth>().TakeDamage(damage);
                Debug.Log("Supay inflige daño cuerpo a cuerpo");

                // Salta si está cerca
                if (!isJumping)
                {
                    StartCoroutine(Jump());
                }
            }
        }
    }

    private IEnumerator Jump()
    {
        navMeshAgent.isStopped = true;
        isJumping = true;

        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        }

        yield return new WaitForSeconds(1f); // Tiempo para esperar antes de permitir otro salto

        // Reactivar el NavMeshAgent después del salto
        navMeshAgent.isStopped = false;

        isJumping = false;
    }

    private void ActivatePhase3()
    {
        // Aquí activas la Fase 3
        Debug.Log("Supay entra en Fase 3");
        // Llama a la lógica de la Fase 3 aquí
    }
}
