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

    private int golpesRecibidos = 0;
    private bool puedeAtacar = true;
    private float tiempoProximoDisparo = 0f;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("PLAYER").transform;
    }

    void Update()
    {
        if (jugador != null && puedeAtacar)
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
        Vector3 direccionAlJugador = (jugador.position - puntoDisparo.position).normalized;

        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);

        bala.transform.rotation = Quaternion.LookRotation(direccionAlJugador);

        bala.GetComponent<Rigidbody>().velocity = direccionAlJugador * velocidadBala;

    }

    public void RecibirGolpe(int dano)
    {
        golpesRecibidos += dano;
        Debug.Log("Enemigo recibió un golpe. Golpes recibidos: " + golpesRecibidos);

        if (golpesRecibidos >= 3)
        {
            Debug.Log("Enemigo bajo la cabeza");
            StartCoroutine(DesactivarTemporalmente());
            golpesRecibidos = 0; // Reinicia el conteo de golpes
        }
    }

    private IEnumerator DesactivarTemporalmente()
    {
        puedeAtacar = false; // Desactiva el ataque
        yield return new WaitForSeconds(10f); // Espera 10 segundos
        puedeAtacar = true; // Reactiva el ataque
        Debug.Log("El enemigo ha vuelto a atacar.");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangoDisparo);
    }

}
