using UnityEngine;

public class Vasija : MonoBehaviour
{
    [SerializeField] private GameObject municionPrefab;   // Prefab de la munici�n de escopeta
    [SerializeField] private float probabilidadDeDrop = 0.7f;  // 70% de probabilidad de soltar munici�n
    public GestorDeVasijas gestor;

    private void Start()
    {
        // Encontrar al gestor en la escena
        //gestor = FindObjectOfType<GestorDeVasijas>();

        //if (gestor == null)
        //{
        //    Debug.LogError("No se encontr� un GestorDeVasijas en la escena.");
        //}
    }

    private void SoltarObjeto(Vector3 posicion)
    {
        // Generar un n�mero aleatorio entre 0 y 1
        float randomValue = Random.value;

        // Si el valor es mayor que la probabilidad, no suelta nada
        if (randomValue > probabilidadDeDrop)
        {
            return;  // No soltar ning�n objeto
        }

        // Soltar munici�n
        Instantiate(municionPrefab, posicion, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
           
            SoltarObjeto(transform.position);  // Soltar munici�n en la posici�n de la vasija
            Destroy(gameObject);  // Destruir la vasija al ser alcanzada por la bala
        }
    }

 
}

