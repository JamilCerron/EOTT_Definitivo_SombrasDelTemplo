using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPlayer : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GameObject bolaDestino;
    void Start()
    {
        navMeshAgent.destination = bolaDestino.transform.position;
    }

   
    void Update()
    {
        
    }
}
