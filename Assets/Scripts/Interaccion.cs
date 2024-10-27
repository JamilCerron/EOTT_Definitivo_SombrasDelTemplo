using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaccion : MonoBehaviour
{
    public Transform palanca;
    public Vector3 estadoApagadoPosition;
    public Vector3 estadoPrendidoPosition;
    protected bool puedeInteractuar = false;

   // private bool estaPrendida = false;

    private void Update()
    {
        if (puedeInteractuar &&  Input.GetKeyDown(KeyCode.E))
        {
            Interactuar();
        }


    }

    protected virtual void Interactuar()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            puedeInteractuar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            puedeInteractuar=false;

        }
    }

}
