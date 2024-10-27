using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public Palanca palanca; // Referencia al objeto Palanca
    public Vector3 puertaAbiertaPosition; // Posición cuando la puerta está abierta
    public Vector3 puertaCerradaPosition; // Posición cuando la puerta está cerrada

    public bool puertaAbierta = false; // Bandera para controlar el estado de la puerta

    private void Update()
    {
        // Verificar si la palanca está encendida
        if (palanca != null && palanca.estaPrendida && !puertaAbierta)
        {
            // Abrir la puerta
            AbrirPuerta();
        }
        // Verificar si la palanca está apagada y la puerta está abierta
        else if (palanca != null && !palanca.estaPrendida && puertaAbierta)
        {
            // Cerrar la puerta
            CerrarPuerta();
        }
    }

    public void AbrirPuerta()
    {
        transform.localPosition = puertaAbiertaPosition; // Mueve la puerta a la posición abierta
        puertaAbierta = true;
        Debug.Log("Puerta abierta");
    }

    public void CerrarPuerta()
    {
        transform.localPosition = puertaCerradaPosition; // Mueve la puerta a la posición cerrada
        puertaAbierta = false;
        Debug.Log("Puerta cerrada");
    }
}

    
