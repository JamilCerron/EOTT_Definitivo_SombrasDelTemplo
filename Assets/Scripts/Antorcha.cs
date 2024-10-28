using UnityEngine;

public class Antorcha : Interaccion
{


    public Light luzAntorcha;  // Luz que se encender�
    public Puente puente;      // Referencia al puente que se activar�
    private bool antorchaEncendida = false; // Para evitar que se active m�s de una vez

    private void Start()
    {
        // Asegurarse de que la luz comience apagada
        if (luzAntorcha != null)
        {
            luzAntorcha.enabled = false;
        }
    }

    protected override void Interactuar()
    {
        // Solo activar si la antorcha no est� ya encendida
        if (!antorchaEncendida)
        {
            EncenderAntorcha();
        }
    }

    private void EncenderAntorcha()
    {
        Debug.Log("Encendiendo antorcha...");

        if (luzAntorcha != null)
        {
            luzAntorcha.enabled = true; // Encender la luz de la antorcha
            Debug.Log("Luz de la antorcha encendida.");
        }
        else
        {
            Debug.LogWarning("Luz de la antorcha no est� asignada.");
        }

        if (puente != null)
        {
            puente.AbrirPuente(); // Activar la apertura del puente
            Debug.Log("Puente activado.");
        }
        else
        {
            Debug.LogWarning("El puente no est� asignado.");
        }

        // Marcar como encendida para que no se repita la interacci�n
        antorchaEncendida = true;
    }
}
