using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverJugador : MonoBehaviour
{
    public float velocidad = 10f; // Velocidad de movimiento

    private Rigidbody rb;

    public int salud = 100;


    public void RecibirDanio(int dano)
    {
        salud -= dano;
        Debug.Log("El jugador recibi� " + dano + " de da�o. Salud restante: " + salud);
        // Aqu� podr�as agregar efectos visuales o sonidos de da�o, si lo deseas.
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical).normalized;
        transform.position += movimiento * velocidad * Time.deltaTime;

    }

}
