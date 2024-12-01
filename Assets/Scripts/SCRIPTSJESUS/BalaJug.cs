using UnityEngine;

public class Baljug : MonoBehaviour
{
    public float lifeTime = 3f; // Tiempo antes de destruir la bala

    void Start()
    {
        // Destruir la bala después de que pase el tiempo de vida
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si la bala ha chocado con el guardián
        if (other.CompareTag("Guardian"))
        {
            // Lógica cuando impacta al guardián (aturdimiento o daño)
            GuardianStun guardian = other.GetComponent<GuardianStun>();
            if (guardian != null)
            {
                guardian.Stun(); // Método en el guardián para manejar el aturdimiento
            }

            // Destruir la bala después del impacto
            Destroy(gameObject);
        }
    }

}
