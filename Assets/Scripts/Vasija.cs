using UnityEngine;

public class Vasija : MonoBehaviour
{
    [SerializeField] private GameObject municionPrefab;   // Prefab de la munición de escopeta
    [SerializeField] private float probabilidadDeDrop = 0.7f;  // 70% de probabilidad de soltar munición
    private GestorDeVasijas gestor;

    private void Start()
    {
        // Encontrar al gestor en la escena
        gestor = FindObjectOfType<GestorDeVasijas>();

        if (gestor == null)
        {
            Debug.LogError("No se encontró un GestorDeVasijas en la escena.");
        }
    }

    private void SoltarObjeto(Vector3 posicion)
    {
        // Generar un número aleatorio entre 0 y 1
        float randomValue = Random.value;

        // Si el valor es mayor que la probabilidad, no suelta nada
        if (randomValue > probabilidadDeDrop)
        {
            return;  // No soltar ningún objeto
        }

        // Soltar munición
        Instantiate(municionPrefab, posicion, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            Destroy(gameObject);  // Destruir la vasija al ser alcanzada por la bala
            SoltarObjeto(transform.position);  // Soltar munición en la posición de la vasija
        }
    }

    private void OnDestroy()
    {
        // Notificar al gestor que esta vasija fue destruida
        if (gestor != null)
        {
            Debug.Log($"Vasija {name} destruida. Notificando al gestor.");
        }
        else
        {
            Debug.LogWarning("No se pudo notificar la destrucción de la vasija porque el gestor no existe.");
        }
    }
}

