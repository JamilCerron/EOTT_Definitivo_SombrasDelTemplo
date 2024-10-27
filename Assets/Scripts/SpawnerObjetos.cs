using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObjetos : MonoBehaviour
{
    [SerializeField] private GameObject crucifijoPrefab;  // Prefab del crucifijo
    [SerializeField] private GameObject municionPrefab;   // Prefab de la munici�n de escopeta
    [SerializeField] private float probabilidadDeDrop = 0.5f;  // 50% de probabilidad de soltar un objeto

    private void SoltarObjeto(Vector3 posicion)
    {
        // Generar un n�mero aleatorio entre 0 y 1
        float randomValue = Random.value;

        // Si el valor es mayor que la probabilidad, no suelta nada
        if (randomValue > probabilidadDeDrop)
        {
            return;  // No soltar ning�n objeto
        }

        // Si va a soltar algo, generar aleatoriamente cu�l objeto soltar
        int objetoAleatorio = Random.Range(0, 3);  // 0, 1 o 2 (2 significa no soltar nada)

        switch (objetoAleatorio)
        {
            case 0:
                // Soltar crucifijo
                Instantiate(crucifijoPrefab, posicion, Quaternion.identity);
                break;
            case 1:
                // Soltar munici�n
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

            // Soltar un objeto en la posici�n de la bacija (no la posici�n de la bala)
            SoltarObjeto(transform.position);  // Usar la posici�n de la bacija para soltar el objeto
        }
    }

}
