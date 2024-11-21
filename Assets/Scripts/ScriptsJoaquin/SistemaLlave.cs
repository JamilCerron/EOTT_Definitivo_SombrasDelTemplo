using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaLlave : MonoBehaviour
{
    [SerializeField] private string idLlave; // Identificador �nico para asociar con la puerta
    [SerializeField] private GameObject se�alInteractiva; // Se�al visual
    private bool enRango = false;
    private GestorLlaves gestorLlaves; // Referencia al gestor

    private void Start()
    {
        se�alInteractiva.SetActive(false);
        gestorLlaves = GestorLlaves.instancia; // Conexi�n con el gestor de llaves
    }

    private void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E)) // Si est� en rango y presiona 'E'
        {
            RecogerLlave();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = true;
            se�alInteractiva.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = false;
            se�alInteractiva.SetActive(false);
        }
    }

    private void RecogerLlave()
    {
        gestorLlaves.RegistrarLlave(idLlave); // Notifica al gestor
        Destroy(gameObject); // Elimina la llave del mundo
    }
}
