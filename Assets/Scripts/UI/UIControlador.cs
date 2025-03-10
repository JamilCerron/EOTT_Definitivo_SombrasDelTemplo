using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControlador : MonoBehaviour
{
    public static UIControlador Instancia { get; private set; }

    [Header("Referencias UI")]
    [SerializeField] private Image barraVida;
    [SerializeField] TextMeshProUGUI cantidadVida;

    [SerializeField] private Image barraCordura;
    [SerializeField] TextMeshProUGUI cantidadCordura;

    [SerializeField] private Image barraResistencia;
    [SerializeField] TextMeshProUGUI cantidadResistencia;

    [SerializeField] private TextMeshProUGUI textoFragmentos;

    [SerializeField] private GameObject jugador;
    private PlayerStats jugadorStats;
    private GestorTumi gestorTumi;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        jugadorStats = jugador.GetComponent<PlayerStats>();
        gestorTumi = GameObject.FindGameObjectWithTag("GestorTumi").GetComponent<GestorTumi>();
    }

    private void Update()
    {
        ActualizarHUD();
    }

    private void ActualizarHUD()
    {
        if (jugador != null)
        {
            // Actualizar las barras de vida y resistencia
            cantidadVida.text = $"Vida: {(int)jugadorStats.VidaActual()}";
            barraVida.fillAmount = (float)jugadorStats.VidaActual() / jugadorStats.VidaMaxima();

            cantidadCordura.text = $"Cordura: {(int)jugadorStats.CorduraActual()}";
            barraCordura.fillAmount = jugadorStats.CorduraActual() / jugadorStats.CorduraMaxima();

            cantidadResistencia.text = $"Resistencia: {(int)jugadorStats.ResistenciaActual()}";
            barraResistencia.fillAmount = jugadorStats.ResistenciaActual() / jugadorStats.ResistenciaMaxima();
        }
        /*
        if (gestorTumi != null)
        {
            // Actualizar el texto de monedas
            textoFragmentos.text = $"Fragmentos: 4/{jugadorStats.FragmentosTumi()}";
        }
        */
    }
}