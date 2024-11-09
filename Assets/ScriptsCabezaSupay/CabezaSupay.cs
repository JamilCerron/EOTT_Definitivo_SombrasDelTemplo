using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabezaSupay : MonoBehaviour
{
    public float rangoDeDisparo = 10f;  // Rango dentro del cual la Cabeza de Supay dispara
    public GameObject balaPrefab;       // Prefab de la bala a disparar
    public Transform puntoDeDisparo;    // Lugar desde donde se dispara la bala
    public float tiempoEntreDisparos = 2f; // Tiempo entre disparos
    public int impactosParaDesactivar = 3; // Cantidad de disparos que recibe antes de apagarse
    public float tiempoDeReactivacion = 10f; // Tiempo antes de reactivarse

    private int impactosRecibidos = 0;
    private bool estaActiva = true;
    private Transform jugador;
    private float tiempoUltimoDisparo;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("PLAYER").transform;
    }

    void Update()
    {
        if (estaActiva && jugador != null)
        {
            float distancia = Vector3.Distance(transform.position, jugador.position);

            if (distancia <= rangoDeDisparo && Time.time >= tiempoUltimoDisparo + tiempoEntreDisparos)
            {
                Disparar();
                tiempoUltimoDisparo = Time.time;
            }
        }
    }

    private void Disparar()
    {
        // Crear la bala y dispararla hacia el jugador
        Instantiate(balaPrefab, puntoDeDisparo.position, Quaternion.LookRotation(jugador.position - puntoDeDisparo.position));
        Debug.Log("Cabeza de Supay dispara al jugador.");
    }

    public void RecibirDisparo()
    {
        if (!estaActiva) return;

        impactosRecibidos++;
        Debug.Log("Cabeza de Supay recibió un disparo. Impactos recibidos: " + impactosRecibidos);

        if (impactosRecibidos >= impactosParaDesactivar)
        {
            StartCoroutine(DesactivarTemporalmente());
        }
    }

    private IEnumerator DesactivarTemporalmente()
    {
        estaActiva = false;
        Debug.Log("Cabeza de Supay bajó ligeramente la cabeza y se desactivó.");

        yield return new WaitForSeconds(tiempoDeReactivacion);

        estaActiva = true;
        impactosRecibidos = 0;
        Debug.Log("Cabeza de Supay se ha reactivado.");
    }

    void OnDrawGizmosSelected()
    {
        // Color del Gizmo (rojo)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDeDisparo);
    }

}
