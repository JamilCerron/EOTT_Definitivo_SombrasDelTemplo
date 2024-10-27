using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public Palanca palanca; // Referencia al objeto Palanca
    public Vector3 puertaAbiertaPosition; // Posici�n cuando la puerta est� abierta
    public Vector3 puertaCerradaPosition; // Posici�n cuando la puerta est� cerrada

    public bool puertaAbierta = false; // Bandera para controlar el estado de la puerta

    private void Update()
    {
        // Verificar si la palanca est� encendida
        if (palanca != null && palanca.estaPrendida && !puertaAbierta)
        {
            // Abrir la puerta
            AbrirPuerta();
        }
        // Verificar si la palanca est� apagada y la puerta est� abierta
        else if (palanca != null && !palanca.estaPrendida && puertaAbierta)
        {
            // Cerrar la puerta
            CerrarPuerta();
        }
    }

    public void AbrirPuerta()
    {
        transform.localPosition = puertaAbiertaPosition; // Mueve la puerta a la posici�n abierta
        puertaAbierta = true;
        Debug.Log("Puerta abierta");
    }

    public void CerrarPuerta()
    {
        transform.localPosition = puertaCerradaPosition; // Mueve la puerta a la posici�n cerrada
        puertaAbierta = false;
        Debug.Log("Puerta cerrada");
    }
}

    
