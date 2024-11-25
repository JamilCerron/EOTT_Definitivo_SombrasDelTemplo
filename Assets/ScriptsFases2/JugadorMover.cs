using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;


public class JugadorMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Camera cinemachineCamera;
    public Transform Destino;
    void Start()
    {
      

        if (cinemachineCamera == null)
        {
            cinemachineCamera = Camera.main;
        }
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(cinemachineCamera != null && Physics.Raycast(cinemachineCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                navMeshAgent.destination = hit.point;
            }
            else
            {
                CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
                if (brain != null && Physics.Raycast(brain.OutputCamera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    navMeshAgent.destination = hit.point;
                }
            }
        }
    }
}
