using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CabezaSupay : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float rangoDisparo = 20f;
    public float tiempoEntreDisparos = 1.5f;
    [SerializeField] float velocidadBala = 4f;
     private Transform jugador;

    private int golpesRecibidos = 0;
    private bool puedeAtacar = true;
    private float tiempoProximoDisparo = 0f;
    private bool golpeando = false;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
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
        if (!golpeando)  
        {
            StartCoroutine(RegistrarGolpe(dano));
        }
    }

    private IEnumerator RegistrarGolpe(int dano)
    {
        golpeando = true;  

        
        golpesRecibidos += dano;
        Debug.Log("Enemigo recibió un golpe. Golpes recibidos: " + golpesRecibidos);

        
        if (golpesRecibidos >= 3)
        {
            Debug.Log("Enemigo bajo la cabeza");
            golpesRecibidos = 0;

            // El enemigo deja de atacar por 10 segundos
            puedeAtacar = false;
            Debug.Log("Enemigo deja de atacar por 10 segundos...");

            // Esperamos 10 segundos antes de que el enemigo vuelva a atacar
            yield return new WaitForSeconds(10f);

            // Después de los 10 segundos, el enemigo puede volver a atacar
            puedeAtacar = true;
            Debug.Log("ENEMIGO VUELVE A ATACAR");
        }

        
        yield return new WaitForSeconds(0.1f);  

        golpeando = false;  
    }

    public void IntentarAtacar()
    {
        if (puedeAtacar)
        {
            Debug.Log("El enemigo está atacando...");
        }
        else
        {
            Debug.Log("El enemigo no puede atacar aún. Esperando...");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangoDisparo);
    }

}
