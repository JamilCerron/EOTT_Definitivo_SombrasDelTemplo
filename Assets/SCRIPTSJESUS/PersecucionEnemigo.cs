using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersecucionEnemigo : MonoBehaviour
{
    //Referencia al jugador
    public GameObject jugador;

    //Velocidad de persecucion
    [SerializeField] private float velocidadPersecucion = 5f;

    //Rango de deteccion el jugador
    [SerializeField] private float rangoDeteccion = 10f;

    //Posicion Inicial del enemigo
    private Vector3 posicionInicial;

    private bool jugadorDetectado = false;

    public Rigidbody rb;

     void Start()
    {
        //Establece la posicion inicial del enemigo
        posicionInicial = transform.position;
    }

    void Update()
    {
        //Verifica si el jugador esta dentro del rango de deteccion
        if (Vector3.Distance(transform.position, jugador.transform.position)<= rangoDeteccion)
        {
            jugadorDetectado = true;

            //Verificar si el enemigo esta Aturdido
            Enemigo enemigo = GetComponent<Enemigo>();
            if(enemigo!=null && !enemigo.IsStunned())
            {
                PerseguirJugador();
            }
        }
        else
        {
            jugadorDetectado = false;

            //Imprime mensaje en Consola
            Debug.Log("Jugador salio del rango del enemigo");
        }

        //Si el jugador esta detectado, persiguelo
        if (jugadorDetectado)
        {
            PerseguirJugador();
        }
        else
        {
            //Si el jugador no esta detectado, vuelve a la posicion inicial 
            VolverPosicionInicial();
        }
    }

    void PerseguirJugador()
    {
        //Calcula la direccion hacia el jugador
        Vector3 direccion = (jugador.transform.position - transform.position).normalized;

        //Mueve el enemigo hacia el jugaoor 
        transform.position += direccion * velocidadPersecucion * Time.deltaTime;

        //Rotar el enemigo hacia el jugador
        transform.rotation = Quaternion.LookRotation(direccion);

        //Hacer que el enemigo ruede
        rb.AddTorque(direccion * velocidadPersecucion * Time.deltaTime);
    }

    void VolverPosicionInicial()
    {
        //Mueve el enemigo hacia la posicion inicial
        transform.position = Vector3.Lerp(transform.position, posicionInicial, velocidadPersecucion * Time.deltaTime);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }

}
