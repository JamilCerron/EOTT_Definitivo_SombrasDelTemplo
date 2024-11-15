using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorTumi : MonoBehaviour
{
    public static GestorTumi instancia;

    private Tumi tumi;
    public bool jugadorTieneTumiCompleto { get; set; } // Indica si el jugador tiene el Tumi

    [SerializeField] private string escenaVictoriaConTumi;
    [SerializeField] private string escenaVictoriaSinTumi;

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

    public void ComprobarVictoria()
    {
        if (jugadorTieneTumiCompleto)
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
}
