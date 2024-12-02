using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorTumi : MonoBehaviour
{
    public static GestorTumi instancia;

    private bool tumiCompleto = false;

    [SerializeField] private string escenaVictoriaConTumi = "VictoriaConTumi";
    [SerializeField] private string escenaVictoriaSinTumi = "VictoriaSinTumi";

    // Escenas donde los fragmentos deben conservarse
    [SerializeField] private string nivel1 = "Nivel1Definitivo";
    [SerializeField] private string nivel2 = "Nivel2Definitivo";
    [SerializeField] private string nivel3 = "Nivel3Definitivo";

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
        if (escenaActual != nivel1 && escenaActual != nivel2 && escenaActual != nivel3)
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
            SceneManager.LoadScene(escenaVictoriaSinTumi);
        }
    }

    public int ObtenerFragmentos()
    {
        return fragmentosTumi;
    }
}
