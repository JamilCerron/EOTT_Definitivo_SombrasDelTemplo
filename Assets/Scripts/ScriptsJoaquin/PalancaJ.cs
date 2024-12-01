using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaJ : MonoBehaviour
{
    [Header("Configuración de la Palanca")]
    [SerializeField] private Transform mangoPalanca; // El mango que rota
    [SerializeField] private float anguloRotacion = -45f; // Ángulo que rotará el mango
    [SerializeField] private float velocidadRotacion = 2f; // Velocidad de la rotación

    [SerializeField] private GameObject señalInteractiva; // Señal visual

    [Header("Configuración de la Plataforma")]
    [SerializeField] private Transform plataforma; // Plataforma que se moverá
    [SerializeField] private Vector3 posicionFinal; // Posición final de la plataforma
    [SerializeField] private float velocidadMovimiento = 2f; // Velocidad de movimiento

    [Header("Configuración de la puerta")]
    [SerializeField] private PuertaInteractiva puertaAsociada; // Referencia a la puerta controlada por esta palanca
    [SerializeField] private bool esPuerta = false;

    private bool activada = false; // Controla si la palanca ha sido activada
    private Vector3 posicionInicialPlataforma; // Posición inicial de la plataforma
    private Quaternion rotacionInicialMango; // Rotación inicial del mango
    private Quaternion rotacionFinalMango; // Rotación final del mango

    private void Awake()
    {
        // Guardar las posiciones iniciales
        posicionInicialPlataforma = plataforma.position;
        rotacionInicialMango = mangoPalanca.rotation;

        // Calcular la rotación final del mango
        rotacionFinalMango = rotacionInicialMango * Quaternion.Euler(0, 0, anguloRotacion);

        señalInteractiva.SetActive(false);
    }

    private void Update()
    {
        if (activada)
        {
            // Rotar el mango de la palanca a su posición final
            mangoPalanca.rotation = Quaternion.Lerp(mangoPalanca.rotation, rotacionFinalMango, Time.deltaTime * velocidadRotacion);

            // Mover la plataforma a su posición final
            plataforma.position = Vector3.MoveTowards(plataforma.position, posicionFinal, Time.deltaTime * velocidadMovimiento);
        }
        else
        {
            // Rotar el mango de la palanca a su posición inicial
            mangoPalanca.rotation = Quaternion.Lerp(mangoPalanca.rotation, rotacionInicialMango, Time.deltaTime * velocidadRotacion);

            // Mover la plataforma a su posición inicial
            plataforma.position = Vector3.MoveTowards(plataforma.position, posicionInicialPlataforma, Time.deltaTime * velocidadMovimiento);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // Detecta interacción con el jugador
        {
            señalInteractiva.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                activada = !activada; // Alterna entre activada y desactivada
                if (esPuerta)
                {
                    if (puertaAsociada != null)
                    {
                        puertaAsociada.VariarBloqueado(true); // Desbloquea la puerta si la palanca está activada
                    }
                    else
                    {
                        Debug.Log("Agregar referencia de puerta en el inspector");
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            señalInteractiva.SetActive(false);
        }
    }
}
