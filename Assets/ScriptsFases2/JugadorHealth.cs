using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private bool isDead = false; // Controla si el jugador ya está muerto


    public void TakeDamage(float damage)
    {
        if (isDead) return; // Si el jugador ya está muerto, no procesa más daño

        health -= damage;
        Debug.Log("Jugador recibió daño. Vida restante: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; // Evita que este método se ejecute más de una vez

        isDead = true; 
        Debug.Log("El jugador ha muerto");
        Destroy(gameObject); 

    }
}
