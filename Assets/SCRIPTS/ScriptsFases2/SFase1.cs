using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SFase1 : MonoBehaviour
{
    [SerializeField] private float health = 198f; // Vida inicial del Supay
    [SerializeField] private float detectionRange = 15f; // Rango para detectar al jugador
    [SerializeField] private GameObject energyBallPrefab; // Prefab de la bola de energía
    [SerializeField] private Transform firePoint; // Punto desde donde se disparan las bolas de energía
    [SerializeField] private float fireRate = 1.5f; // Tiempo entre disparos
    [SerializeField] private float energyBallSpeed = 10f; // Velocidad de la bola de energía
    private Transform player; // Referencia al jugador
    private NavMeshAgent navMeshAgent; // Agente NavMesh para futuras fases
    private bool isShooting = false; // Controla si está disparando
    private bool isPhase2 = false; // Controla si la Fase 2 está activa

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = true; // El Supay estático en esta fase
        player = GameObject.FindGameObjectWithTag("PLAYER").transform;
    }

    void Update()
    {
        if (player != null && !isPhase2)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange && !isShooting)
            {
                StartCoroutine(ShootEnergyBalls());
            }
        }
    }

    private IEnumerator ShootEnergyBalls()
    {
        isShooting = true;
        while (player != null && Vector3.Distance(transform.position, player.position) <= detectionRange && !isPhase2)
        {
            // Crear la bola de energía
            GameObject energyBall = Instantiate(energyBallPrefab, firePoint.position, firePoint.rotation);

            // Agregar velocidad a la bola de energía
            Rigidbody rb = energyBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (player.position - firePoint.position).normalized;
                rb.velocity = direction * energyBallSpeed;
            }

            yield return new WaitForSeconds(fireRate); // Esperar antes del próximo disparo
        }
        isShooting = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Supay recibió daño. Vida restante: " + health);

        // Verificar si la vida baja al 66%
        if (!isPhase2 && health <= 198f * 0.66f) // 66% de la vida inicial
        {
            ActivatePhase2();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void ActivatePhase2()
    {
        isPhase2 = true;
        Debug.Log("Supay entra en Fase 2");

        //Detener Fase 1
        StopAllCoroutines(); 

        navMeshAgent.isStopped = false; // Reactivar Nav Mesh para moverse en Fase 2

        // Activar el comportamiento de la Fase 2
        SFase2 fase2Script = gameObject.AddComponent<SFase2>();
        fase2Script.SetPlayer(player); // Pasar la referencia al jugador a la nueva fase
        fase2Script.SetNavMeshAgent(navMeshAgent); // Pasar el NavMeshAgent a la fase 2

        // Desactivar SFase1 sin destruir el objeto
        this.enabled = false;
    }

    private void Die()
    {
        Debug.Log("Supay ha muerto");
        Destroy(gameObject); // Destruir al enemigo
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Color del Gizmo (rojo para rango de detección)
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Dibuja un círculo que indica el rango de detección
    }

}
