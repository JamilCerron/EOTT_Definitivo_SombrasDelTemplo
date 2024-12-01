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

    private bool bloqueada = false; // Nueva variable para manejar el estado de bloqueo

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
        if (bloqueada) return; // Si est� bloqueada, no se puede interactuar

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

    public void CerrarPuertaPermanente()
    {
        StartCoroutine(CerrarPuertaRapidamente());
    }

    public IEnumerator CerrarPuertaRapidamente()
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

    public void VariarBloqueado(bool estado)
    {
        bloqueada = !estado; // Bloquea o desbloquea la puerta
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // Detecta al jugador
        {
            enRango = true;
            if (!bloqueada)
            {
                se�alInteractiva.SetActive(true);
            }
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
