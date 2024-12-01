using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscopetaApuntarJ : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private Camera camaraJugador;  // La cámara principal o virtual del jugador

    private void Update()
    {
        AjustarDireccionEscopeta();
    }

    private void AjustarDireccionEscopeta()
    {
        // Genera un rayo desde el centro de la cámara hacia adelante
        Ray ray = camaraJugador.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); // Centro de la pantalla
        //transform.rotation = Quaternion.LookRotation(ray.direction);
        //transform.rotation = camaraJugador.transform.rotation;
        return;
        /*
        RaycastHit hit;

        // Comprueba si el rayo impacta algo (sin filtro de capas)
        if (Physics.Raycast(ray, out hit, distanciaMaxima))
        {
            // Si impacta, apunta hacia el punto de impacto
            transform.LookAt(hit.point);
        }
        else
        {
            // Si no impacta nada, apunta hacia la dirección de la cámara
            transform.rotation = Quaternion.LookRotation(ray.direction);
        }
        */
    }
}
