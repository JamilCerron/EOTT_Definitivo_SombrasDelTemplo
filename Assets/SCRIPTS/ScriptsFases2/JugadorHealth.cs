using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private bool isDead = false; // Controla si el jugador ya est� muerto


    public void TakeDamage(float damage)
    {
        if (isDead) return; // Si el jugador ya est� muerto, no procesa m�s da�o

        health -= damage;
        Debug.Log("Jugador recibi� da�o. Vida restante: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; // Evita que este m�todo se ejecute m�s de una vez

        isDead = true; 
        Debug.Log("El jugador ha muerto");
        Destroy(gameObject); 

    }
}
