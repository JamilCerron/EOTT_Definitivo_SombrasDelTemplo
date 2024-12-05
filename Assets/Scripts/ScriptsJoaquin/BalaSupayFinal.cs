using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BalaSupayFinal : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f; // Velocidad de la bala
    [SerializeField] private GameObject canvasFinal; // Panel o canvas a activar
    [SerializeField] private string tagJugador = "Player"; // Tag del jugador
    [SerializeField] private float tiempoDestruccion = 5f; // Tiempo antes de autodestruirse si no choca

    private void Start()
    {
        if (canvasFinal == null)
        {
            Debug.LogWarning("CanvasFinal no est� asignado en el inspector.");
        }

        // Auto destruir despu�s de cierto tiempo si no choca
        Destroy(gameObject, tiempoDestruccion);
    }

    private void Update()
    {
        // Mueve la bala hacia adelante
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta colisi�n con el jugador
        if (other.CompareTag(tagJugador))
        {
            // Activa el canvas si est� asignado
            if (canvasFinal != null)
            {
                canvasFinal.SetActive(true);
            }
            else
            {
                Debug.LogWarning("CanvasFinal no asignado, no se puede activar.");
            }

            // Destruye la bala
            Destroy(gameObject);
        }
    }
}
