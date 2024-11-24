using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JugadorMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    public Transform Destino;
    void Start()
    {
       
    }

    
    void Update()
    {
        navMeshAgent.SetDestination(Destino.position);
    }
}
