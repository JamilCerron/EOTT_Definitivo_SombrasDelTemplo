using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaLlave : MonoBehaviour
{
    [SerializeField] private string idLlave; // Identificador único para asociar con la puerta
    [SerializeField] private GameObject señalInteractiva; // Señal visual
    private bool enRango = false;
    private GestorLlaves gestorLlaves; // Referencia al gestor

    private void Start()
    {
        señalInteractiva.SetActive(false);
        gestorLlaves = GestorLlaves.instancia; // Conexión con el gestor de llaves
    }

    private void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E)) // Si está en rango y presiona 'E'
        {
            RecogerLlave();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = true;
            señalInteractiva.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = false;
            señalInteractiva.SetActive(false);
        }
    }

    private void RecogerLlave()
    {
        gestorLlaves.RegistrarLlave(idLlave); // Notifica al gestor
        Destroy(gameObject); // Elimina la llave del mundo
    }
}
