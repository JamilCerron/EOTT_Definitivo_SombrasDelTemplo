using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorBal : MonoBehaviour
{
    [SerializeField] private float damage = 10f; // Daño que inflige la bala
    [SerializeField] private float lifeTime = 5f; // Tiempo de vida de la bala

    private void Start()
    {
        // Destruir la bala después de un tiempo
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si la bala impacta contra un enemigo
        if (other.CompareTag("ENEMIGO")) // Asegúrate de usar el mismo tag para tus enemigos
        {
            // Aplicar daño al enemigo
            SupayMover enemy = other.gameObject.GetComponent<SupayMover>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Llama al método para reducir la vida del enemigo
            }

            // Destruir la bala tras el impacto
            Destroy(gameObject);
        }
        
    }

}
