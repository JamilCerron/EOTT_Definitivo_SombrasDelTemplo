    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorTumi : MonoBehaviour
{
    public static GestorTumi instancia;

    private Tumi tumi;
    private bool tumiCompleto = false;

    [SerializeField] private string escenaVictoriaConTumi;
    [SerializeField] private string escenaVictoriaSinTumi;
    [SerializeField] private int fragmentosTumi = 0;

    private void Awake()
    {
        if (tumi != null)
        {
            tumi = GameObject.FindGameObjectWithTag("Tumi").GetComponent<Tumi>();
        }

        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // Mantiene el GameManager entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AñadirFragmentos(int cantidad)
    {
        if (fragmentosTumi >= 0 && fragmentosTumi <= 4)
        {
            fragmentosTumi += cantidad;
        }

        if (fragmentosTumi == 4)
        {
            tumiCompleto = true; // Marca que el jugador tiene el Tumi completo
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ComprobarVictoria();
        }
    }

    public int ObtenerFragmentos()
    {
        return fragmentosTumi;
    }
}
