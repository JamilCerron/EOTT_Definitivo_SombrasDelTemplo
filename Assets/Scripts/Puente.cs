using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puente : MonoBehaviour
{
    public Transform puente; // La referencia al objeto del puente que se mover�
    public Vector3 posicionAbierta; // Posici�n a la que se mover� el puente
    public Vector3 posicionCerrada; // Posici�n inicial del puente
    public float velocidad = 2f;    // Velocidad de movimiento

    private bool estaAbriendo = false;

    // M�todo para abrir el puente
    public void AbrirPuente()
    {
        Debug.Log("Se ha llamado a AbrirPuente.");
        estaAbriendo = true;
    }

    private void Start()
    {
        // Asegurarse de que el puente est� en la posici�n cerrada al inicio
        if (puente != null)
        {
            puente.localPosition = posicionCerrada;
            Debug.Log("Puente en posici�n cerrada.");
        }
        else
        {
            Debug.LogWarning("Puente no est� asignado.");
        }
    }

    private void Update()
    {
        if (estaAbriendo)
        {
            Debug.Log("El puente se est� abriendo...");
            puente.localPosition = Vector3.Lerp(puente.localPosition, posicionAbierta, Time.deltaTime * velocidad);
        }
    }
}
