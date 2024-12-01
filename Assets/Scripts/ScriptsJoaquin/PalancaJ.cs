using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaJ : MonoBehaviour
{
    [Header("Configuraci�n de la Palanca")]
    [SerializeField] private Transform mangoPalanca; // El mango que rota
    [SerializeField] private float anguloRotacion = -45f; // �ngulo que rotar� el mango
    [SerializeField] private float velocidadRotacion = 2f; // Velocidad de la rotaci�n

    [SerializeField] private GameObject se�alInteractiva; // Se�al visual

    [Header("Configuraci�n de la Plataforma")]
    [SerializeField] private Transform plataforma; // Plataforma que se mover�
    [SerializeField] private Vector3 posicionFinal; // Posici�n final de la plataforma
    [SerializeField] private float velocidadMovimiento = 2f; // Velocidad de movimiento

    [Header("Configuraci�n de la puerta")]
    [SerializeField] private PuertaInteractiva puertaAsociada; // Referencia a la puerta controlada por esta palanca
    [SerializeField] private bool esPuerta = false;

    private bool activada = false; // Controla si la palanca ha sido activada
    private Vector3 posicionInicialPlataforma; // Posici�n inicial de la plataforma
    private Quaternion rotacionInicialMango; // Rotaci�n inicial del mango
    private Quaternion rotacionFinalMango; // Rotaci�n final del mango

    private void Awake()
    {
        // Guardar las posiciones iniciales
        posicionInicialPlataforma = plataforma.position;
        rotacionInicialMango = mangoPalanca.rotation;

        // Calcular la rotaci�n final del mango
        rotacionFinalMango = rotacionInicialMango * Quaternion.Euler(0, 0, anguloRotacion);

        se�alInteractiva.SetActive(false);
    }

    private void Update()
    {
        if (activada)
        {
            // Rotar el mango de la palanca a su posici�n final
            mangoPalanca.rotation = Quaternion.Lerp(mangoPalanca.rotation, rotacionFinalMango, Time.deltaTime * velocidadRotacion);

            // Mover la plataforma a su posici�n final
            plataforma.position = Vector3.MoveTowards(plataforma.position, posicionFinal, Time.deltaTime * velocidadMovimiento);
        }
        else
        {
            // Rotar el mango de la palanca a su posici�n inicial
            mangoPalanca.rotation = Quaternion.Lerp(mangoPalanca.rotation, rotacionInicialMango, Time.deltaTime * velocidadRotacion);

            // Mover la plataforma a su posici�n inicial
            plataforma.position = Vector3.MoveTowards(plataforma.position, posicionInicialPlataforma, Time.deltaTime * velocidadMovimiento);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // Detecta interacci�n con el jugador
        {
            se�alInteractiva.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                activada = !activada; // Alterna entre activada y desactivada
                if (esPuerta)
                {
                    if (puertaAsociada != null)
                    {
                        puertaAsociada.VariarBloqueado(true); // Desbloquea la puerta si la palanca est� activada
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
            se�alInteractiva.SetActive(false);
        }
    }
}
