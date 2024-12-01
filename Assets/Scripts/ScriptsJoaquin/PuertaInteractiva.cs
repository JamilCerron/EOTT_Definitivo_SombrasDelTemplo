using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaInteractiva : MonoBehaviour
{
    [Header("Configuraci�n de la Puerta")]
    [SerializeField] private Transform puntoRotacion; // Centro de rotaci�n
    [SerializeField] private float anguloApertura = 90f; // �ngulo de apertura
    [SerializeField] private float duracionApertura = 2f; // Duraci�n para abrir la puerta
    [SerializeField] private float duracionCierre = 0.5f; // Duraci�n para cerrar autom�ticamente (r�pido)

    [SerializeField] private GameObject se�alInteractiva; // Indicaci�n visual para interactuar

    [Header("Configuraci�n de Cierre Autom�tico")]
    [SerializeField] private bool permitirCerradoAutomatico = false; // Si la puerta se cierra autom�ticamente
    [SerializeField] private bool esPlataforma = false; // Controlado por una palanca externa

    private bool enRango = false;
    private bool abierta = false;

    private Quaternion rotacionInicial;
    private Quaternion rotacionFinal;

    private void Start()
    {
        // Configuraci�n inicial
        rotacionInicial = transform.rotation;
        rotacionFinal = rotacionInicial * Quaternion.Euler(0, anguloApertura, 0);

        if (se�alInteractiva != null)
        {
            se�alInteractiva.SetActive(false);
        }
    }

    private void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E)) // Detecta si est� cerca y se presiona 'E'
        {
            IntentarAbrir();
        }
    }

    public void IntentarAbrir()
    {
        if (permitirCerradoAutomatico) return; // No hace nada si ya est� abierta

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
            se�alInteractiva.SetActive(true); // Muestra la se�al de interacci�n
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Sale del rango de la puerta
        {
            enRango = false;
            se�alInteractiva.SetActive(false); // Oculta la se�al de interacci�n
        }
    }
}
