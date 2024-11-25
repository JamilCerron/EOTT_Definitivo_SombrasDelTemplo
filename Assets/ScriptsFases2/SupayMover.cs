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


    private float nextShotTime;
    private Transform player;
    private bool isPhaseTwo = false;
    private bool isPhaseThree = false;
    private bool isJumpingToGround = false;

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
        else if (isPhaseTwo)
        {
            PhaseTwoBehavior(distanceToPlayer);
        }
        else if (!isPhaseTwo && distanceToPlayer <= shootingRange)
        {
            ShootAtPlayer();
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
        // Mover al Supay hacia la posición en el suelo (y = 0)
        Vector3 targetPosition = new Vector3(transform.position.x, 0f, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, jumpSpeed * Time.deltaTime);

        // Detener el salto al suelo cuando llegue
        if (transform.position.y <= 0.1f)
        {
            isJumpingToGround = false;
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
        // Implementa el comportamiento de la Fase 3 aquí
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
