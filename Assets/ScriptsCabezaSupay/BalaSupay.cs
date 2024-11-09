using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaSupay : MonoBehaviour
{
    public float velocidad = 10f; // Velocidad de la bala
    public int dano = 10; // Da�o que causa la bala al jugador

    private void Start()
    {
        // Mover la bala hacia adelante
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * velocidad;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Verificar si la bala colisiona con el jugador
        if (other.gameObject.CompareTag("PLAYER"))
        {
            // Llamar al m�todo de da�o en el jugador (si existe)
            MoverJugador player = other.gameObject.GetComponent<MoverJugador>();
            if (player != null)
            {
                player.RecibirDanio(dano);
                Debug.Log("El jugador recibi� da�o. Salud restante: " + player.salud);
            }

            // Destruir la bala despu�s de colisionar
            Destroy(gameObject);
        }

        else
        {
            // Si la bala colisiona con otro objeto que no es el jugador, tambi�n la destruye
            Destroy(gameObject);
        }
  
    }
}
