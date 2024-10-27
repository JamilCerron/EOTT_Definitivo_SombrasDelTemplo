using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : Interaccion
{
    public Puerta puerta; // Referencia al script de la puerta
    public bool estaPrendida = false;

    protected override void Interactuar()
    {
        // Asegurarse de que la palanca est� asignada
        if (palanca == null)
        {
            return;
        }

        // Alternar entre los estados de la palanca
        estaPrendida = !estaPrendida;

        // Mover la palanca a la posici�n correspondiente
        if (estaPrendida)
        {
            palanca.localPosition = estadoPrendidoPosition; // Mueve la palanca a la posici�n de encendido
            Debug.Log("Palanca encendida");
            puerta.AbrirPuerta(); // Llamar a la funci�n para abrir la puerta
        }
        else
        {
            palanca.localPosition = estadoApagadoPosition; // Mueve la palanca a la posici�n de apagado
            Debug.Log("Palanca apagada");
            puerta.CerrarPuerta(); // Llamar a la funci�n para cerrar la puerta
        }
    }


}
