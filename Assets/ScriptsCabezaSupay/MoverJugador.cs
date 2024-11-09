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
        Debug.Log("El jugador recibió " + dano + " de daño. Salud restante: " + salud);
        if (salud <= 0)
        {
            // Aquí podrías agregar la lógica para cuando el jugador muere
            Debug.Log("El jugador ha muerto");
        }
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
