using System.Collections;
using TMPro;
using UnityEngine;

public class GestorMensajes : MonoBehaviour
{
    public static GestorMensajes Instance; // Singleton para acceso global

    [SerializeField] private TextMeshProUGUI mensajeTexto; // Referencia al componente TextMeshPro
    [SerializeField] private float duracionMensaje = 3f; // Duración predeterminada del mensaje

    private Coroutine mensajeActual; // Referencia a la corutina activa

    private void Awake()
    {
        // Configuración del Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Asegurar que el texto comienza desactivado
        if (mensajeTexto != null)
        {
            mensajeTexto.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Muestra un mensaje por un tiempo determinado.
    /// </summary>
    /// <param name="mensaje">Texto del mensaje.</param>
    /// <param name="duracion">Duración opcional del mensaje (usa la predeterminada si no se especifica).</param>
    public void MostrarMensaje(string mensaje, float? duracion = null)
    {
        if (mensajeTexto == null) return;

        // Detener el mensaje actual si hay uno en curso
        if (mensajeActual != null)
        {
            StopCoroutine(mensajeActual);
        }

        // Iniciar una nueva corutina para mostrar el mensaje
        mensajeActual = StartCoroutine(MostrarMensajeCoroutine(mensaje, duracion ?? duracionMensaje));
    }

    /// <summary>
    /// Corutina que controla el tiempo que el mensaje permanece visible.
    /// </summary>
    private IEnumerator MostrarMensajeCoroutine(string mensaje, float duracion)
    {
        mensajeTexto.text = mensaje; // Actualizar el texto
        mensajeTexto.gameObject.SetActive(true); // Activar el texto
        yield return new WaitForSeconds(duracion); // Esperar el tiempo especificado
        mensajeTexto.gameObject.SetActive(false); // Ocultar el texto
    }
}
