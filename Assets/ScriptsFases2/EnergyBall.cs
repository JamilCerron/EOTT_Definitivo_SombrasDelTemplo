using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] private float damage = 20f; // Daño que causa al jugador

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLAYER"))
        {
            other.GetComponent<JugadorHealth>().TakeDamage(damage);
            Destroy(gameObject); // Destruir la bola al impactar
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destruir la bola si sale de la pantalla
    }

}
