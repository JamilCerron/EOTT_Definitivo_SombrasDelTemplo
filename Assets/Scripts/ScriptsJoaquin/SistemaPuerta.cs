using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SistemaPuerta : MonoBehaviour
{
    [Header("Configuraci�n de la puerta")]
    [SerializeField] private string idLlave; // Identificador de la llave que abre esta puerta
    [SerializeField] private Transform puntoRotacion; // Centro de rotaci�n
    [SerializeField] private float anguloApertura = 90f; // �ngulo de apertura
    [SerializeField] private bool esTecho = false; // Si esta puerta activa el mecanismo del techo
    [SerializeField] private Transform techoMovible; // Referencia al techo
    [SerializeField] private float desplazamientoTecho = -5f; // Movimiento en Y del techo
    [SerializeField] private float duracionMovimiento = 10f; // Tiempo del movimiento
    [SerializeField] private TextMeshProUGUI textoTemporizador; // Temporizador UI
    [SerializeField] private GameObject se�alInteractiva; // Indicaci�n visual para interactuar

    private GestorLlaves gestorLlaves;
    private bool enRango = false;
    private bool abierta = false;

    private void Start()
    {
        if (textoTemporizador != null)
        {
            textoTemporizador.gameObject.SetActive(false);
        }

        if (se�alInteractiva != null)
        {
            se�alInteractiva.SetActive(false);
        }
        gestorLlaves = GestorLlaves.instancia; // Conexi�n con el gestor de llaves
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
        if (abierta) return; // No hace nada si ya est� abierta

        if (gestorLlaves.TieneLlave(idLlave))
        {
            if (esTecho)
            {
                ActivarTecho();
            }
            else
            {
                AbrirPuerta();
            }
        }
        else
        {
            Debug.Log($"No tienes la llave para abrir esta puerta: {idLlave}");
        }
    }

    private void AbrirPuerta()
    {
        abierta = true;
        transform.RotateAround(puntoRotacion.position, Vector3.up, anguloApertura);
        Debug.Log($"Puerta {idLlave} abierta.");
    }

    private void ActivarTecho()
    {
        abierta = true;
        StartCoroutine(MoverTecho());
    }

    private IEnumerator MoverTecho()
    {
        textoTemporizador.gameObject.SetActive(true);
        float timer = duracionMovimiento;
        Vector3 posicionInicial = techoMovible.position;
        Vector3 posicionFinal = posicionInicial + Vector3.up * desplazamientoTecho;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            techoMovible.position = Vector3.Lerp(posicionInicial, posicionFinal, 1 - (timer / duracionMovimiento));

            int minutos = Mathf.FloorToInt(timer / 60);
            int segundos = Mathf.FloorToInt(timer % 60);
            textoTemporizador.text = $"{minutos:00}:{segundos:00}";
            yield return null;
        }

        textoTemporizador.gameObject.SetActive(false);
        Debug.Log("El techo ha llegado a su destino.");
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
