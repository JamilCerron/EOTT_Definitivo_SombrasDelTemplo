using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private PlayerStats jugadorVida;

    private void Start()
    {
        jugadorVida = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugadorVida.RecibirDa�o(100);
        }
    }
}
