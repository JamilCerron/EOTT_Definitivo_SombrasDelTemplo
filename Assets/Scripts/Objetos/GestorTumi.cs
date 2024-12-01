    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorTumi : MonoBehaviour
{
    public static GestorTumi instancia;

    private bool tumiCompleto = false;

    [SerializeField] private string escenaVictoriaConTumi;

    // Escenas donde los fragmentos deben conservarse
    [SerializeField] private string nivel1;
    [SerializeField] private string nivel2;

    // Fragmentos estáticos
    private static int fragmentosTumi;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else if (instancia != this)
        {
            Destroy(gameObject); // Elimina duplicados
        }
    }

    private void Start()
    {
        // Si no estamos en una escena permitida, reinicia fragmentos
        string escenaActual = SceneManager.GetActiveScene().name;
        if (escenaActual != nivel1 && escenaActual != nivel2)
        {
            ReiniciarFragmentos();
        }
    }

    private void ReiniciarFragmentos()
    {
        fragmentosTumi = 0;
        tumiCompleto = false;
        Debug.Log("Fragmentos de Tumi reiniciados.");
    }

    public void AñadirFragmentos(int cantidad)
    {
        if (fragmentosTumi < 4)
        {
            fragmentosTumi += cantidad;
            GestorMensajes.Instance.MostrarMensaje($"Fragmentos recogidos: {fragmentosTumi}/4");

            if (fragmentosTumi == 4)
            {
                tumiCompleto = true;
                GestorMensajes.Instance.MostrarMensaje("¡Tumi completo!");
            }
        }
        else
        {
            Debug.Log("Ya tienes todos los fragmentos del Tumi.");
        }
    }

    public void ComprobarVictoria()
    {
        if (tumiCompleto)
        {
            SceneManager.LoadScene(escenaVictoriaConTumi);
        }
        else
        {
            GestorMensajes.Instance.MostrarMensaje("No tienes todas las piezas del Tumi.");
        }
    }

    public int ObtenerFragmentos()
    {
        return fragmentosTumi;
    }
}
