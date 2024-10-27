using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObjetos : MonoBehaviour
{
    [SerializeField] private GameObject crucifijoPrefab;  // Prefab del crucifijo
    [SerializeField] private GameObject municionPrefab;   // Prefab de la munición de escopeta
    [SerializeField] private float probabilidadDeDrop = 0.5f;  // 50% de probabilidad de soltar un objeto

    private void SoltarObjeto(Vector3 posicion)
    {
        // Generar un número aleatorio entre 0 y 1
        float randomValue = Random.value;

        // Si el valor es mayor que la probabilidad, no suelta nada
        if (randomValue > probabilidadDeDrop)
        {
            return;  // No soltar ningún objeto
        }

        // Si va a soltar algo, generar aleatoriamente cuál objeto soltar
        int objetoAleatorio = Random.Range(0, 3);  // 0, 1 o 2 (2 significa no soltar nada)

        switch (objetoAleatorio)
        {
            case 0:
                // Soltar crucifijo
                Instantiate(crucifijoPrefab, posicion, Quaternion.identity);
                break;
            case 1:
                // Soltar munición
                Instantiate(municionPrefab, posicion, Quaternion.identity);
                break;
            case 2:
                // No soltar nada (opcional)
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detecta si el objeto que colisiona es una "Bala"
        if (collision.gameObject.CompareTag("Bala"))
        {
            // Destruir la bacija (este objeto)
            Destroy(gameObject);

            // Soltar un objeto en la posición de la bacija (no la posición de la bala)
            SoltarObjeto(transform.position);  // Usar la posición de la bacija para soltar el objeto
        }
    }

}
