using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacija : MonoBehaviour
{

    public GameObject municionPrefab;
    public Vector3 offsetSpawn = new Vector3(0, 0, 0);



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Bala"))
        {
            Debug.Log("Colisión con bala detectada. Destruyendo baciija y spawneando municiones.");

            // Guarda la posición antes de destruir
            Vector3 spawnPosition = transform.position + offsetSpawn;

            // Destruye la baciija
            Destroy(gameObject);
            Debug.Log("Baciija destruida.");

            // Instancia el prefab de municiones
            if (municionPrefab != null)
            {
                Instantiate(municionPrefab, spawnPosition, Quaternion.identity);
                Debug.Log("Municiones instanciadas en: " + spawnPosition);
            }
            else
            {
                Debug.LogWarning("Prefab de munición no asignado.");
            }
        }
    }
}
