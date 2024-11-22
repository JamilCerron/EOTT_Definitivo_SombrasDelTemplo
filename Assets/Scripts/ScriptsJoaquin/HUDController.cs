using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Componentes de UI")]
    [SerializeField] private Image barraVida;
    [SerializeField] private Image barraResistencia;
    [SerializeField] private TextMeshProUGUI textoCordura;
    [SerializeField] private GameObject fuegoCordura; // Objeto visual del fuego azul
    
    [Header("Cara del jugador")]
    [SerializeField] private Animator caraAnimator;

    [Header("Referencia")]
    [SerializeField] private PlayerStats playerStats;

    private float corduraActual;
    private float corduraMaxima;

    void Awake()
    {
        if (playerStats != null)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        }

        corduraMaxima = playerStats.CorduraMaxima();
    }

    void Update()
    {
        ActualizarHUD();
    }

    void ActualizarHUD()
    {
        // Actualizamos las barras de vida, cordura y resistencia
        barraVida.fillAmount = (float)playerStats.VidaActual() / playerStats.VidaMaxima();
        barraResistencia.fillAmount = playerStats.ResistenciaActual() / playerStats.ResistenciaMaxima();

        // Actualizamos la cordura actual
        corduraActual = playerStats.CorduraActual();

        // Actualizamos el texto de porcentaje de cordura
        int porcentajeCordura = Mathf.RoundToInt((corduraActual / corduraMaxima) * 100);
        textoCordura.text = porcentajeCordura + "%";

        // Actualizamos la escala del fuego azul
        if (fuegoCordura != null)
        {
            float escalaFuego = corduraActual / corduraMaxima;
            fuegoCordura.transform.localScale = new Vector3(escalaFuego, escalaFuego, escalaFuego);
        }

        // Actualizamos el Animator con el valor de cordura actual
        if (caraAnimator != null)
        {
            caraAnimator.SetFloat("Cordura", playerStats.CorduraActual());
        }

    }
}
