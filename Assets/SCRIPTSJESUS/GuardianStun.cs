using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianStun : MonoBehaviour
{
    public float stunDuration = 10f;
    private bool isStunned = false;
    private float stunTimer = 0f;

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
        Debug.Log("El guardi�n ha sido aturdido durante " + stunDuration + " segundos.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BULLET"))
        {
            Stun();
            Destroy(other.gameObject); // Destruye la bala al impactar
        }
    }
}
