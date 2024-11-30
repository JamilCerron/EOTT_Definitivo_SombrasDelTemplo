using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaInteractiva : MonoBehaviour
{
    [Header("Configuración de la Puerta")]
    [SerializeField] private Transform puntoRotacion; // Centro de rotación
    [SerializeField] private float anguloApertura = 90f; // Ángulo de apertura
    [SerializeField] private float anguloAperturaAutomatico = 45f; // Ángulo para cierre automático
    [SerializeField] private float duracionApertura = 2f; // Duración para abrir la puerta
    [SerializeField] private float duracionCierre = 0.5f; // Duración para cerrar automáticamente (rápido)
    [SerializeField] private GameObject señalInteractiva; // Indicación visual para interactuar

    [Header("Configuración de Cierre Automático")]
    [SerializeField] private bool permitirCerradoAutomatico = false; // Si la puerta se cierra automáticamente
    [SerializeField] private bool esPlataforma = false; // Controlado por una palanca externa

    private bool enRango = false;
    private bool abierta = false;

    private Quaternion rotacionInicial;
    private Quaternion rotacionFinal;
    private Quaternion rotacionFinalAutomatico;

    private void Start()
    {
        // Configuración inicial
        rotacionInicial = transform.rotation;
        rotacionFinal = rotacionInicial * Quaternion.Euler(0, anguloApertura, 0);
        rotacionFinalAutomatico = rotacionInicial * Quaternion.Euler(0, anguloAperturaAutomatico, 0);

        if (señalInteractiva != null)
        {
            señalInteractiva.SetActive(false);
        }
    }

    private void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E)) // Detecta si está cerca y se presiona 'E'
        {
            StartCoroutine(AbrirPuertaGradualmente());
        }
    }

    private IEnumerator AbrirPuertaGradualmente()
    {
        abierta = true;
        float tiempo = 0f;

        // Animación de apertura
        Quaternion destino = permitirCerradoAutomatico && !esPlataforma ? rotacionFinalAutomatico : rotacionFinal;

        while (tiempo < duracionApertura)
        {
            tiempo += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(rotacionInicial, destino, tiempo / duracionApertura);
            yield return null;
        }

        // Si se permite el cerrado automático y no es una plataforma, cerramos después de 1 segundo
        if (permitirCerradoAutomatico && !esPlataforma)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(CerrarPuertaRapidamente());
        }
    }

    private IEnumerator CerrarPuertaRapidamente()
    {
        float tiempo = 0f;

        // Animación de cierre rápido
        while (tiempo < duracionCierre)
        {
            tiempo += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionInicial, tiempo / duracionCierre);
            yield return null;
        }

        abierta = false;
        Debug.Log("Puerta cerrada rápidamente.");
    }

    public void EstablecerEsPlataforma(bool valor)
    {
        esPlataforma = valor; // Controlado por la palanca
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Detecta al jugador
        {
            enRango = true;
            señalInteractiva.SetActive(true); // Muestra la señal de interacción
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Sale del rango de la puerta
        {
            enRango = false;
            señalInteractiva.SetActive(false); // Oculta la señal de interacción
        }
    }
}
