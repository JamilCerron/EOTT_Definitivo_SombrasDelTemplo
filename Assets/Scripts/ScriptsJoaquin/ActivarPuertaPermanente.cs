using UnityEngine;

public class ActivarPuertaPermanente : MonoBehaviour
{
    [SerializeField] private PuertaInteractiva puertaInteractiva;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puertaInteractiva.VariarBloqueado(false); // Desactiva la apertura
            puertaInteractiva.CerrarPuertaPermanente(); // Cierra inmediatamente
            Destroy(gameObject); // Elimina el trigger tras activarse
        }
    }
}