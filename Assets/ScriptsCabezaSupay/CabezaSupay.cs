using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CabezaSupay : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float rangoDisparo = 10f;
    public float tiempoEntreDisparos = 1.5f;
    [SerializeField] float velocidadBala = 4f;

    private Transform jugador;
    private float tiempoProximoDisparo = 0f;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("PLAYER").transform;
    }

    void Update()
    {
        if (jugador != null)
        {
            float distancia = Vector3.Distance(transform.position, jugador.position);

            if (distancia <= rangoDisparo)
            {
                // Disparar si el tiempo entre disparos ha pasado
                if (Time.time >= tiempoProximoDisparo)
                {
                    Disparar();
                    tiempoProximoDisparo = Time.time + tiempoEntreDisparos;
                }
            }
        }
    }

    void Disparar()
    {
       //Calcula la dirección desde el punto de disparo hacia el jugador
        Vector3 direccionAlJugador = (jugador.position - puntoDisparo.position).normalized;

        // Crea la bala
        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);

        bala.transform.rotation = Quaternion.LookRotation(direccionAlJugador);

        bala.GetComponent<Rigidbody>().velocity = direccionAlJugador * velocidadBala;

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangoDisparo);
    }

}
