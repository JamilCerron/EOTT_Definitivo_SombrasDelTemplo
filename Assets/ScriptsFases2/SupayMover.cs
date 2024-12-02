using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SupayMover: MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float shootingRange = 10f; 
    [SerializeField] private GameObject energyBallPrefab; 
    [SerializeField] private Transform shootingPoint; 
    [SerializeField] private float shootingCooldown = 2f;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float meleeDamage = 20f;
    [SerializeField] private float meleeSpeed = 2f;
    [SerializeField] private float meleeRange = 3f;
    [SerializeField] private float chargeSpeed = 15f; // Velocidad de la embestida
    [SerializeField] private float chargeDuration = 5f; // Duración de la embestida
    [SerializeField] private Transform arenaCenter; // Centro de la arena
    [SerializeField] private float attackRange = 5f; // Rango de embestida



    private float nextShotTime;
    private Transform player;
    private bool isPhaseTwo = false;
    private bool isPhaseThree = false;
    private bool isJumpingToGround = false;
    private bool isCharging = false;  // Para evitar iniciar la embestida más de una vez
    private bool hasArrivedAtCenter = false;
    private float jumpStartHeight; // Altura inicial del salto
    private float jumpStartTime;   // Tiempo en que comenzó el salto
    private float jumpDuration = 1.5f; // Duración total del salto
    private bool hasChargedOnce = false; // Evitar que se embista repetidamente en una sola embestida
    private bool isAttacking = false;
    private bool canChargeMultipleTimes = true;  // Para controlar las embestidas múltiples
    private float timeBetweenCharges = 1.5f; // Tiempo entre cada embestida (en segundos)


    private Vector3 centerPosition = new Vector3(0, 0, 0); // Definir la posición del centro, ajusta esto según sea necesario




    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER").transform;
        // Validar que el NavMeshAgent esté activo
        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogError("El NavMeshAgent no está en una NavMesh al inicio.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Cambiar a Fase 2 si la salud es menor al 66%
        if (!isPhaseTwo && health <= 66f)
        {
            ActivatePhaseTwo();
        }
        else if (!isPhaseThree && health <= 33f) // Transición a Fase 3
        {
            ActivatePhaseThree();
        }
        if (isJumpingToGround)
        {
            JumpToGround();
        }

        if (isPhaseThree)
        {
            StartCoroutine(PhaseThreeBehavior());

        }

        else if (isPhaseTwo)
        {
            PhaseTwoBehavior(distanceToPlayer);
        }
        else if ( distanceToPlayer <= shootingRange)
        {
            ShootAtPlayer();
        }

        // Comienza la embestida si está en fase 3 y el jugador está dentro del rango de embestida
        if (isPhaseThree && !isAttacking && distanceToPlayer <= shootingRange)  // Puedes usar meleeRange o shootingRange dependiendo de tu lógica
        {
            StartCoroutine(ChargeAttack());
            
        }
    }

    private void MoveToCenter()
    {
        // Usa arenaCenter como el centro real
        Vector3 directionToCenter = (arenaCenter.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, arenaCenter.position, 5f * Time.deltaTime);
    }

    private IEnumerator ChargeAttack()
    {
        // Aseguramos que no haya múltiples embestidas simultáneas
        if (isCharging || isAttacking ) yield break;

        isCharging = true; // Activar el estado de embestida
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 chargeDirection = (player.position - transform.position).normalized;
        float chargeTime = 0f;
      

        // Iniciar embestida
        while (chargeTime < chargeDuration)
        {
            rb.velocity = chargeDirection * chargeSpeed; // Ajusta la velocidad de la embestida
            chargeTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector3.zero; // Detener la velocidad al finalizar la embestida

       
        // Regresar al centro después de embestir
        MoveToCenter();

        // Baja la vida del jugador por cada embestida
        ApplyDamageToPlayer();

        isCharging = false;

        // Espera un poco antes de permitir la próxima embestida
        yield return new WaitForSeconds(timeBetweenCharges);
    }


    private void ApplyDamageToPlayer()
    {
        if (player != null)
        {
            JugadorHealth playerScript = player.GetComponent<JugadorHealth>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(20); // Aplicar daño al jugador
            }
            else
            {
                Debug.LogError("No se encontró el script JugadorHealth en el jugador.");
            }
        }
    }


    private void ShootAtPlayer()
    {
        if (Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + shootingCooldown;

            // Instanciar la bola de energía
            GameObject energyBall = Instantiate(energyBallPrefab, shootingPoint.position, Quaternion.identity);

            // Calcular la dirección hacia el jugador
            Vector3 direction = (player.position - shootingPoint.position).normalized;
            energyBall.GetComponent<Rigidbody>().velocity = direction * 10f; // Velocidad de la bola
        }
    }

    private void ActivatePhaseTwo()
    {
        isPhaseTwo = true;
        isJumpingToGround = true; // Activar el salto al suelo
        jumpStartHeight = transform.position.y; // Altura inicial del salto
        jumpStartTime = Time.time; // Tiempo inicial del salto
        Debug.Log("Supay ha entrado en la Fase 2");

    }

    private void JumpToGround()
    {
        if (navMeshAgent.enabled)
        {
            navMeshAgent.isStopped = true; // Asegúrate de que esté detenido
            navMeshAgent.enabled = false; // Desactiva completamente el NavMeshAgent
        }

        float elapsedTime = Time.time - jumpStartTime; // Tiempo transcurrido desde el inicio del salto
        float normalizedTime = elapsedTime / jumpDuration; // Normalizar entre 0 y 1

        if (normalizedTime >= 1f) // Si el tiempo normalizado supera 1, el salto termina
        {
            isJumpingToGround = false; // Desactivar estado de salto
            navMeshAgent.enabled = true; // Reactivar el NavMeshAgent
            navMeshAgent.isStopped = false; // Asegúrate de que pueda moverse
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z); // Asegurar que esté en el suelo

            Debug.Log("Supay ha aterrizado y comenzará a perseguir al jugador.");
            return;
        }

        // Movimiento parabólico del salto
        float newY = Mathf.Lerp(jumpStartHeight, 0f, normalizedTime) + Mathf.Sin(normalizedTime * Mathf.PI) * 2f;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void PhaseTwoBehavior(float distanceToPlayer)
    {
        if (isJumpingToGround) return; // No perseguir al jugador si está saltando

        if (!navMeshAgent.isOnNavMesh)
        {
            PlaceOnNavMesh();
        }

        navMeshAgent.isStopped = false; // Asegúrate de que el agente pueda moverse


        if (distanceToPlayer <= meleeRange)
        {
            DealMeleeDamage();
        }
        else
        {
            navMeshAgent.destination = player.position; // Perseguir al jugador
        }
    }

    private void DealMeleeDamage()
    {
        // Lógica para infligir daño al jugador
        JugadorHealth playerScript = player.GetComponent<JugadorHealth>();
        if (playerScript != null)
        {
            playerScript.TakeDamage(meleeDamage); // Llama al método para reducir la vida del jugador
        }
    }

    private void ActivatePhaseThree()
    {
        isPhaseThree = true;
        hasArrivedAtCenter = false;  // Asegurarse de que el Supay regrese al centro al entrar en fase 3
        Debug.Log("Supay ha entrado en la Fase 3");

        // Iniciar movimiento hacia el centro inmediatamente al activar la fase 3
        MoveToCenter();
    }

    private IEnumerator PhaseThreeBehavior()
    {
        // Primero, mueve al Supay al centro si no ha llegado
        if (!hasArrivedAtCenter)
        {
            MoveToCenter();
            hasArrivedAtCenter = true;  // Solo permitir que se mueva al centro una vez
            yield return new WaitForSeconds(1f); // Espera para asegurarte de que haya llegado al centro
        }

        // Luego, intenta la embestida solo si el jugador está dentro del rango
        while (isPhaseThree) // Mantén el ciclo mientras la fase 3 esté activa
        {

            if (player == null)
            {
                // Si el jugador ha sido destruido, salimos del ciclo
                yield break;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && !isCharging && !isAttacking)
            {
                StartCoroutine(ChargeAttack());
                yield return new WaitForSeconds(chargeDuration); // Espera que termine la embestida
                MoveToCenter(); // Regresar al centro después de cada embestida
            }
            else
            {
                yield return null; // Si el jugador está fuera del rango de embestida, espera
            }
        }


    }


    private void PlaceOnNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
       
        }
        else
        {
            Debug.LogError("No se encontró una posición válida en la NavMesh.");
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Vida restante del enemigo: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Supay ha sido derrotado");
        Destroy(gameObject);
    }

   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }

}
