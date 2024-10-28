using UnityEngine;

public class Puerta01 : MonoBehaviour
{
    public Transform ejePuerta; // El punto de giro de la puerta
    public float anguloAbierto = -90f; // �ngulo al que se abrir� la puerta
    public float anguloCerrado = 0f; // �ngulo al que estar� la puerta cuando est� cerrada
    public float velocidad = 2f; // Velocidad de rotaci�n
    private bool estaAbriendo = false; // Si la puerta est� abri�ndose
    private bool jugadorCerca = false; // Si el jugador est� cerca de la puerta
    private bool puertaAbierta = false; // Si la puerta est� abierta o cerrada

    void Update()
    {
        // Si el jugador est� cerca y presiona la tecla E, abre o cierra la puerta
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            estaAbriendo = !estaAbriendo;
            puertaAbierta = !puertaAbierta;
        }

        // L�gica para abrir o cerrar la puerta suavemente
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

    // Detectar cuando el jugador entra en el �rea de la puerta
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    // Detectar cuando el jugador sale del �rea de la puerta
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }

}
