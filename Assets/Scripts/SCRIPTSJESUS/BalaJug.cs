using UnityEngine;

public class Baljug : MonoBehaviour
{
    public float lifeTime = 3f; // Tiempo antes de destruir la bala

    void Start()
    {
        // Destruir la bala despu�s de que pase el tiempo de vida
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si la bala ha chocado con el guardi�n
        if (other.CompareTag("Guardian"))
        {
            // L�gica cuando impacta al guardi�n (aturdimiento o da�o)
            GuardianStun guardian = other.GetComponent<GuardianStun>();
            if (guardian != null)
            {
                guardian.Stun(); // M�todo en el guardi�n para manejar el aturdimiento
            }

            // Destruir la bala despu�s del impacto
            Destroy(gameObject);
        }
    }

}
