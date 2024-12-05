using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GuardianStun : MonoBehaviour
{
    public float stunDuration = 10f;
    private bool isStunned = false;
    private float stunTimer = 0f;
    NavMeshAgent agent;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                isStunned = false;
                Debug.Log("El guardi�n ya no est� aturdido.");
            }
        }
    }

    public void Stun()
    {
        isStunned = true;
        stunTimer = stunDuration;

        if (agent != null)
        {
            agent.isStopped = true; // Detiene el movimiento del agente
        }

        Debug.Log("El guardi�n ha sido aturdido durante " + stunDuration + " segundos.");
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bala"))
        {
            Stun();
            Destroy(other.gameObject); // Destruye la bala al impactar
        }
    }
}
