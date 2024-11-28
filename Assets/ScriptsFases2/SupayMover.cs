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
    


    private float nextShotTime;
    private Transform player;
    private bool isPhaseTwo = false;
    private bool isPhaseThree = false;
    private bool isJumpingToGround = false;
    private bool isCharging = false;  // Para evitar iniciar la embestida más de una vez
    private bool hasArrivedAtCenter = false;
    private bool isPlayerHit = false;



  

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER").transform;
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
            HandlePhaseThreeBehavior(distanceToPlayer);
           
        }

        else if (isPhaseTwo)
        {
            PhaseTwoBehavior(distanceToPlayer);
        }
        else if (!isPhaseTwo && distanceToPlayer <= shootingRange)
        {
            ShootAtPlayer();
        }
    }


    private void HandlePhaseThreeBehavior(float distanceToPlayer)
    {
        if (!hasArrivedAtCenter)
        {
            // Mover al centro de la arena si aún no hemos llegado
            float distanceToCenter = Vector3.Distance(transform.position, arenaCenter.position);

            // Imprimir las posiciones para verificar si es la correcta
            Debug.Log($"Posición del Supay: {transform.position}");
            Debug.Log($"Posición del centro de la arena: {arenaCenter.position}");
            Debug.Log($"Distancia al centro: {distanceToCenter}");


            if (distanceToCenter > 0.2f)
            {
                Debug.Log("Moviéndose al centro de la arena...");
                navMeshAgent.destination = arenaCenter.position;
            }
            else 
            {
                // Asegurarnos de que hemos llegado al centro
                Debug.Log("Supay ya está en el centro de la arena.");
                navMeshAgent.isStopped = true;  // Detener el agente
                transform.position = arenaCenter.position;
                hasArrivedAtCenter = true;  // Marcar que ha llegado
                StartCoroutine(ChargeAttack()); // Iniciar la embestida
            }
        }
    }

    private IEnumerator ChargeAttack()
    {
        isCharging = true;  // Activar el estado de embestida
        isPlayerHit = false; // Resetear el estado de daño

        // Desactivar el NavMeshAgent durante la embestida
        navMeshAgent.enabled = false;

        // Espera la duración de la embestida
        yield return new WaitForSeconds(chargeDuration);

        // Reactivar el NavMeshAgent después de la embestida
        navMeshAgent.enabled = true;

        // Al finalizar la embestida, reinicia el estado
        isCharging = false;

        // Reiniciar el estado para que pueda ir al centro si es necesario
        hasArrivedAtCenter = false;

    }

    private void OnTriggerEnter(Collider collision )
    {
        if (collision.gameObject.CompareTag("PLAYER") && isCharging && !isPlayerHit)
        {
            isPlayerHit = true;
            Debug.Log("El jugador fue impactado por la embestida.");
            JugadorHealth playerScript = collision.gameObject.GetComponent<JugadorHealth>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(meleeDamage); // Aplicar el daño
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
        Debug.Log("Supay ha entrado en la Fase 2");
        
    }

    private void JumpToGround()
    {

        // Desactiva el NavMeshAgent para permitir el movimiento manual
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }

        // Mover al Supay hacia la posición en el suelo (y = 0)
        Vector3 targetPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, jumpSpeed * Time.deltaTime);

        // Detener el salto al suelo cuando llegue
        if (transform.position.y <= 0.1f)
        {
            isJumpingToGround = false;
            // Reactivar el NavMeshAgent una vez que el salto haya terminado
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = true;
            }
        }
    }

    private void PhaseTwoBehavior(float distanceToPlayer)
    {
        // Persigue al jugador lentamente
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        transform.position += directionToPlayer * meleeSpeed * Time.deltaTime;

        // Si está dentro del rango melee, inflige daño al jugador
        if (distanceToPlayer <= meleeRange)
        {
            DealMeleeDamage();
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
        Debug.Log("Supay ha entrado en la Fase 3");
        StartCoroutine(PhaseThreeBehavior());
        // Implementa el comportamiento de la Fase 3 aquí
    }

    private IEnumerator PhaseThreeBehavior()
    {
       
        while (isPhaseThree)
        {
            // 1. Mover al centro de la arena
            Debug.Log("Moviendo al centro de la arena...");

            if (navMeshAgent != null && navMeshAgent.enabled)
            {
                // Verificar si el NavMeshAgent está sobre una NavMesh válida
                if (navMeshAgent.isOnNavMesh)
                {
                    navMeshAgent.destination = arenaCenter.position; // O la posición que estés asignando
                }
                else
                {
                    Debug.LogError("El Supay no está en una NavMesh.");
                    // Intentar reposicionar el Supay en una NavMesh válida
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
                    {
                        transform.position = hit.position; // Coloca al enemigo en una posición válida
                        navMeshAgent.enabled = true; // Asegúrate de que el agente esté habilitado
                        navMeshAgent.destination = arenaCenter.position; // Asigna el destino después de haber corregido la posición
                    }
                    else
                    {
                        Debug.LogError("No se encontró una posición válida en la NavMesh para el Supay.");
                        yield break; // Termina la ejecución si no se puede corregir la posición
                    }
                }
            }

            else
            {
                Debug.LogError("El NavMeshAgent no está habilitado o el Supay no está en una NavMesh.");

                yield break;
            }


            while (Vector3.Distance(transform.position, arenaCenter.position) > 0.1f)
            {
                yield return null;
            }
           

            // 2. Apuntado
            Debug.Log("Supay está apuntando al jugador...");
            Vector3 chargeDirection = (player.position - transform.position).normalized;
            yield return new WaitForSeconds(3f);

            // 3. Embestida
            Debug.Log("Supay realiza una embestida");
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                float chargeTime = 0f;
                while (chargeTime < chargeDuration)
                {
                    // Mover al enemigo hacia la dirección del jugador
                    rb.velocity = chargeDirection * chargeSpeed;

                    // Verificar colisiones con el jugador durante la embestida
                    if (Vector3.Distance(transform.position, player.position) <= meleeRange)
                    {
                        Debug.Log("El jugador fue impactado por la embestida.");
                        DealMeleeDamage();
                        break; // Detener la embestida si impacta al jugador
                    }

                    chargeTime += Time.deltaTime;
                    yield return null;
                }
            }

            // 4. Detener la embestida y regresar al centro
            Debug.Log("Supay regresa al centro de la arena.");
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Detener el movimiento
            }

            if (navMeshAgent.enabled && navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.destination = arenaCenter.position; // O la posición que estés asignando
            }
            else
            {
                Debug.LogError("El NavMeshAgent no está habilitado o el Supay no está en una NavMesh.");
               
                NavMeshHit hit;
                if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
                {
                    transform.position = hit.position; // Coloca al enemigo en una posición válida
                    navMeshAgent.enabled = true; // Habilita el agente
                }
                else
                {
                    Debug.LogError("No se encontró una posición válida en la NavMesh para el Supay.");
                }
            }


            while (Vector3.Distance(transform.position, arenaCenter.position) > 0.1f)
            {
                yield return null;
            }

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

    private void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PLAYER") && isCharging && !isPlayerHit)
        {
            isPlayerHit = true;
            Debug.Log("El jugador fue impactado por la embestida.");
            JugadorHealth playerScript = collision.gameObject.GetComponent<JugadorHealth>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(meleeDamage); // Aplicar el daño
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }

}
