using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumi : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private int fragmentosTumi = 0;

    private void Awake()
    {
        dialogueMark.SetActive(false);
    }

    public void AñadirFragmentos(int cantidad)
    {
        if (fragmentosTumi >= 0 && fragmentosTumi <= 4)
        {
            fragmentosTumi += cantidad;
        }

        if (fragmentosTumi == 4)
        {
            GestorTumi.instancia.jugadorTieneTumiCompleto = true; // Marca que el jugador tiene el Tumi completo
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            dialogueMark.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                AñadirFragmentos(1);
                Destroy(gameObject); // Destruye el objeto Tumi en la escena
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        dialogueMark.SetActive(false);
    }

    public int ObtenerFragmentos()
    {
        return fragmentosTumi;
    }
}