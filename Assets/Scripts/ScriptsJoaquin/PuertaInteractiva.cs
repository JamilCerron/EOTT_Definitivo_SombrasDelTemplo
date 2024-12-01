using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaInteractiva : MonoBehaviour
{
    [Header("Configuración de la Puerta")]
    [SerializeField] private Transform puntoRotacion; // Centro de rotación
    [SerializeField] private float anguloApertura = 90f; // Ángulo de apertura
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

    private void Start()
    {
        // Configuración inicial
        rotacionInicial = transform.rotation;
        rotacionFinal = rotacionInicial * Quaternion.Euler(0, anguloApertura, 0);

        if (señalInteractiva != null)
        {
            señalInteractiva.SetActive(false);
        }
    }

    private void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E)) // Detecta si está cerca y se presiona 'E'
        {
            IntentarAbrir();
        }
    }

    public void IntentarAbrir()
    {
        if (permitirCerradoAutomatico) return; // No hace nada si ya está abierta

        if (!abierta)
        {
            StartCoroutine(AbrirPuertaGradualmente());
        }
        else
        {
            StartCoroutine(CerrarPuertaRapidamente());
        }
    }

    private IEnumerator AbrirPuertaGradualmente()
    {
        abierta = true;
        float tiempo = 0f;

        while (tiempo < duracionApertura)
        {
            tiempo += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionFinal, tiempo / duracionApertura);
            yield return null;
        }

        Debug.Log("Puerta abierta.");

    }

    private IEnumerator CerrarPuertaRapidamente()
    {
        abierta = false;
        float tiempo = 0f;

        while (tiempo < duracionCierre)
        {
            tiempo += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(rotacionFinal, rotacionInicial, tiempo / duracionCierre);
            yield return null;
        }

        Debug.Log("Puerta cerrada.");
    }

    public void EstablecerEsPlataforma(bool valor)
    {
        esPlataforma = valor; // Controlado por la palanca
    }

    private void OnTriggerStay(Collider other)
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
