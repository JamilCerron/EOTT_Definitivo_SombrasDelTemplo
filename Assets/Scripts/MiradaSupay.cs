using UnityEngine;

public class MiradaSupay : MonoBehaviour
{
    public Transform jugador;  // Referencia al jugador
    public float velocidadRotacion = 2f;  // Velocidad de rotaci�n

    void Update()
    {
        SeguirConLaMirada();
    }

    void SeguirConLaMirada()
    {
        // Si el jugador no est� asignado, busca el objeto con la etiqueta "Player"
        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Calcula la direcci�n hacia el jugador
        Vector3 direccionHaciaJugador = jugador.position - transform.position;
        direccionHaciaJugador.y = 0;  // Mantener la rotaci�n solo en el eje Y

        // Solo si hay alguna diferencia significativa en la direcci�n
        if (direccionHaciaJugador.magnitude > 0.1f)
        {
            // Rotaci�n suave hacia el jugador
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionHaciaJugador);

            // A�adir un offset si el modelo no est� alineado con el eje Z positivo
            rotacionObjetivo *= Quaternion.Euler(0, 90, 0);  // Ajusta este valor seg�n la orientaci�n de tu enemigo

            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);
        }
    }
}
