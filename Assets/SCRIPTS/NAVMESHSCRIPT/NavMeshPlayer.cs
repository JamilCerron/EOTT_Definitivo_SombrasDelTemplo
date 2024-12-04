using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class NavMeshPlayer : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GameObject bolaDestino;
    private Camera cinemachineCamera;

    public int maxHealth = 100; 
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();

        if (brain != null && brain.OutputCamera != null)
        {
            cinemachineCamera = brain.OutputCamera;
        }

        else
        {
            Debug.LogError("No se encontró una CinemachineBrain o una cámara activa.");
        }

        if (bolaDestino != null)
        {
            navMeshAgent.destination = bolaDestino.transform.position;
        }
        else
        {
            Debug.LogWarning("No se ha asignado 'bolaDestino'.");
        }
    }

   
    void Update()
    {
        if (Input.GetMouseButton(0) && cinemachineCamera != null)
        {
            Ray ray = cinemachineCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                navMeshAgent.destination = hit.point; 
            }
        }
    }

    public void TakeDamage(int damage)
    {
        
        currentHealth -= damage;

        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("El jugador ha muerto.");
    }
}
