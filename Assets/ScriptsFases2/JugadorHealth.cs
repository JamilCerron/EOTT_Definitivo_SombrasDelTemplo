using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Jugador recibió daño. Vida restante: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto");
        // Aquí puedes reiniciar el nivel o mostrar una pantalla de Game Over
    }
}
