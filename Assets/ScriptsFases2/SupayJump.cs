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
            // Llamamos a la funci�n para completar el salto despu�s de un peque�o retraso
            StartCoroutine(CompleteJump());
        }
    }

    private IEnumerator CompleteJump()
    {
        
        yield return new WaitForSeconds(1f);

        
        agent.CompleteOffMeshLink();
    }
}
