using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaInteractiva : MonoBehaviour
{
    [Header("Configuraci�n de la Puerta")]
    [SerializeField] private Transform puntoRotacion; // Centro de rotaci�n
    [SerializeField] private float anguloApertura = 90f; // �ngulo de apertura
    [SerializeField] private float anguloAperturaAutomatico = 45f; // �ngulo para cierre autom�tico
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
    private Quaternion rotacionFinalAutomatico;

    private void Start()
    {
        // Configuraci�n inicial
        rotacionInicial = transform.rotation;
        rotacionFinal = rotacionInicial * Quaternion.Euler(0, anguloApertura, 0);
        rotacionFinalAutomatico = rotacionInicial * Quaternion.Euler(0, anguloAperturaAutomatico, 0);

        if (se�alInteractiva != null)
        {
            se�alInteractiva.SetActive(false);
        }
    }

    private void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E)) // Detecta si est� cerca y se presiona 'E'
        {
            StartCoroutine(AbrirPuertaGradualmente());
        }
    }

    private IEnumerator AbrirPuertaGradualmente()
    {
        abierta = true;
        float tiempo = 0f;

        // Animaci�n de apertura
        Quaternion destino = permitirCerradoAutomatico && !esPlataforma ? rotacionFinalAutomatico : rotacionFinal;

        while (tiempo < duracionApertura)
        {
            tiempo += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(rotacionInicial, destino, tiempo / duracionApertura);
            yield return null;
        }

        // Si se permite el cerrado autom�tico y no es una plataforma, cerramos despu�s de 1 segundo
        if (permitirCerradoAutomatico && !esPlataforma)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(CerrarPuertaRapidamente());
        }
    }

    private IEnumerator CerrarPuertaRapidamente()
    {
        float tiempo = 0f;

        // Animaci�n de cierre r�pido
        while (tiempo < duracionCierre)
        {
            tiempo += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionInicial, tiempo / duracionCierre);
            yield return null;
        }

        abierta = false;
        Debug.Log("Puerta cerrada r�pidamente.");
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
