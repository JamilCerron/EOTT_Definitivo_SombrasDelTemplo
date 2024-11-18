using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Componentes de UI")]
    [SerializeField] private Image barraVida;
    [SerializeField] private Image barraCordura;
    [SerializeField] private Image barraResistencia;

    [Header("Referencia")]
    [SerializeField] private PlayerStats playerStats;

    void Awake()
    {
        if (playerStats == null)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        }
    }

    void Update()
    {
        ActualizarHUD();
    }

    void ActualizarHUD()
    {
        // Actualizamos las imágenes de las barras usando fillAmount
        barraVida.fillAmount = (float)playerStats.VidaActual() / playerStats.VidaMaxima();
        barraCordura.fillAmount = playerStats.CorduraActual() / playerStats.CorduraMaxima();
        barraResistencia.fillAmount = playerStats.ResistenciaActual() / playerStats.ResistenciaMaxima();
    }
}
