using UnityEngine;

public class MiradaSupay : MonoBehaviour
{
    public Transform jugador;  // Referencia al jugador
    public float velocidadRotacion = 2f;  // Velocidad de rotación

    void Update()
    {
        SeguirConLaMirada();
    }

    void SeguirConLaMirada()
    {
        // Si el jugador no está asignado, busca el objeto con la etiqueta "Player"
        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Calcula la dirección hacia el jugador
        Vector3 direccionHaciaJugador = jugador.position - transform.position;
        direccionHaciaJugador.y = 0;  // Mantener la rotación solo en el eje Y

        // Solo si hay alguna diferencia significativa en la dirección
        if (direccionHaciaJugador.magnitude > 0.1f)
        {
            // Rotación suave hacia el jugador
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionHaciaJugador);

            // Añadir un offset si el modelo no está alineado con el eje Z positivo
            rotacionObjetivo *= Quaternion.Euler(0, 90, 0);  // Ajusta este valor según la orientación de tu enemigo

            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);
        }
    }
}
