using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SupayJump : MonoBehaviour
{
    private NavMeshAgent agent;
    
   
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        if (agent.isOnOffMeshLink)
        {
            // Llamamos a la función para completar el salto después de un pequeño retraso
            StartCoroutine(CompleteJump());
        }
    }

    private IEnumerator CompleteJump()
    {
        
        yield return new WaitForSeconds(1f);

        
        agent.CompleteOffMeshLink();
    }
}
