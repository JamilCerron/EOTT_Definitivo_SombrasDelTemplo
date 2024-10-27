using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{

    private bool controlPresionadoEsteFrame = false; // Bandera para saber si Control fue presionado en el mismo frame

    public CambioArma cambioArma;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Recarga();
        }

        // Verificar si Control fue presionado para agacharse
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controlPresionadoEsteFrame = true; // Marcar que Control fue presionado en este frame
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controlPresionadoEsteFrame = false; // Resetear cuando se suelta Control
        }

        // Solo permitir disparar si se ha presionado "Fire1" y Control no fue presionado en este mismo frame
        if (Input.GetButtonDown("Fire1") && !controlPresionadoEsteFrame && cambioArma.EsEscopetaActiva())
        {
            Disparar();
        }

        // Resetear la bandera después de un frame para permitir disparos en los siguientes frames
        controlPresionadoEsteFrame = false;
    }

    protected virtual void Disparar()
    {

    }

    protected virtual void Recarga()
    {

    }



}
