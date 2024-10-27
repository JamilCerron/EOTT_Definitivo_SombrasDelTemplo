using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puente : MonoBehaviour
{
    public Transform puente; // La referencia al objeto del puente que se moverá
    public Vector3 posicionAbierta; // Posición a la que se moverá el puente
    public Vector3 posicionCerrada; // Posición inicial del puente
    public float velocidad = 2f;    // Velocidad de movimiento

    private bool estaAbriendo = false;

    // Método para abrir el puente
    public void AbrirPuente()
    {
        Debug.Log("Se ha llamado a AbrirPuente.");
        estaAbriendo = true;
    }

    private void Start()
    {
        // Asegurarse de que el puente esté en la posición cerrada al inicio
        if (puente != null)
        {
            puente.localPosition = posicionCerrada;
            Debug.Log("Puente en posición cerrada.");
        }
        else
        {
            Debug.LogWarning("Puente no está asignado.");
        }
    }

    private void Update()
    {
        if (estaAbriendo)
        {
            Debug.Log("El puente se está abriendo...");
            puente.localPosition = Vector3.Lerp(puente.localPosition, posicionAbierta, Time.deltaTime * velocidad);
        }
    }
}
