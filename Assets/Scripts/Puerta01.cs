using UnityEngine;

public class Puerta01 : MonoBehaviour
{
    public Transform ejePuerta; // El punto de giro de la puerta
    public float anguloAbierto = -90f; // Ángulo al que se abrirá la puerta
    public float anguloCerrado = 0f; // Ángulo al que estará la puerta cuando esté cerrada
    public float velocidad = 2f; // Velocidad de rotación
    private bool estaAbriendo = false; // Si la puerta está abriéndose
    private bool jugadorCerca = false; // Si el jugador está cerca de la puerta
    private bool puertaAbierta = false; // Si la puerta está abierta o cerrada

    void Update()
    {
        // Si el jugador está cerca y presiona la tecla E, abre o cierra la puerta
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            estaAbriendo = !estaAbriendo;
            puertaAbierta = !puertaAbierta;
        }

        // Lógica para abrir o cerrar la puerta suavemente
        if (estaAbriendo)
        {
            Quaternion rotacionObjetivo = Quaternion.Euler(0, anguloAbierto, 0);
            ejePuerta.localRotation = Quaternion.Slerp(ejePuerta.localRotation, rotacionObjetivo, Time.deltaTime * velocidad);
        }
        else
        {
            Quaternion rotacionObjetivo = Quaternion.Euler(0, anguloCerrado, 0);
            ejePuerta.localRotation = Quaternion.Slerp(ejePuerta.localRotation, rotacionObjetivo, Time.deltaTime * velocidad);
        }
    }

    // Detectar cuando el jugador entra en el área de la puerta
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    // Detectar cuando el jugador sale del área de la puerta
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }

}
