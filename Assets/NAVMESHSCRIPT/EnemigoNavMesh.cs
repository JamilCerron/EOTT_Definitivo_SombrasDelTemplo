using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoNavMesh : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] patrolPoints;      
    public float detectionRadius = 5f;    
    public float chaseDuration = 15f;     
    public int damage = 20;

    private int currentPatrolIndex = 0;   
    private Transform player;             
    private bool isChasing = false;      
    private float chaseTimer = 0f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER")?.transform;

        if (patrolPoints.Length > 0)
        {
            navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        }
        else
        {
            Debug.LogError("No se encontró un jugador con la etiqueta 'PLAYER'.");
        }
    }

   
    void Update()
    {
        if (isChasing)
        {

            chaseTimer += Time.deltaTime;

            if (chaseTimer >= chaseDuration || Vector3.Distance(transform.position, player.position) > detectionRadius)
            {
                StopChasing();
            }
            else
            {
                
                navMeshAgent.destination = player.position;

               
                if (Vector3.Distance(transform.position, player.position) < 1f)
                {
                    AttackPlayer();
                }
            }
        }
        else
        {
            
            Patrol();
        }

    }
    void Patrol()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        }

        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            StartChasing();
        }
    }

    void StartChasing()
    {
        isChasing = true;
        chaseTimer = 0f; 
    }

    void StopChasing()
    {
        isChasing = false;

        
        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
    }

    void AttackPlayer()
    {
        
        player.GetComponent<NavMeshPlayer>()?.TakeDamage(damage);

        Debug.Log("El enemigo embistió al jugador e infligió daño.");
       
        StopChasing();
    }

    private void OnDrawGizmosSelected()
    {
      
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
