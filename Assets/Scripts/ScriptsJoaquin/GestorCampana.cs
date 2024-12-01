using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorCampana : MonoBehaviour
{
    [SerializeField] private Campana[] campanas;
    [SerializeField] private GameObject areaOculta; // El área que se desbloquea
    [SerializeField] private float tiempoLimite = 30f; // Tiempo límite para completar el puzzle

    [SerializeField] private TextMeshProUGUI timerText;
    private float timer;
    private bool eventActive = false;

    private int campanasActivadas = 0;
    private bool puzzleCompletado = false;

    [SerializeField] private string escenaDerrota;

    void Awake()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

        foreach (Campana campana in campanas)
        {
            campana.SetGestorJuego(this);
        }
    }

    private void Update()
    {
        if (eventActive)
        {
            timer -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (timer <= 0)
            {
                timerText.gameObject.SetActive(false);
                ResetPuzzle();
                SceneManager.LoadScene(escenaDerrota);
            }
        }
    }

    public void ActivarCampana(Campana campana)
    {
        if (!puzzleCompletado)
        {
            campanasActivadas++;

            if (campanasActivadas == 1)
            {
                StartCoroutine(Temporizador());
            }

            if (campanasActivadas >= campanas.Length)
            {
                DesbloquearAreaOculta();
            }
            else
            {
                // Actualizar mensaje
                GestorMensajes.Instance.MostrarMensaje($"Campanas activas: {campanasActivadas}/{campanas.Length}");
            }
        }
    }
    private IEnumerator Temporizador()
    {
        eventActive = true;
        timer = tiempoLimite;
        timerText.gameObject.SetActive(true);

        while (timer > 0 && !puzzleCompletado)
        {
            yield return null;
        }

        if (!puzzleCompletado)
        {
            ResetPuzzle();
        }
    }

    private void ResetPuzzle()
    {
        if (puzzleCompletado) return;

        Debug.Log("Tiempo agotado. Reiniciando el puzzle.");
        campanasActivadas = 0;

        foreach (Campana campana in campanas)
        {
            campana.Resetear();
        }
    }

    private void DesbloquearAreaOculta()
    {
        eventActive = false;
        timerText.gameObject.SetActive(false);
        puzzleCompletado = true;
        areaOculta.SetActive(false); // Abre el área oculta

        GestorMensajes.Instance.MostrarMensaje("¡Puzzle completado! Desbloqueando área oculta.");
    }

    public int CampanasActivas()
    {
        return campanasActivadas;
    }

    public int TotalCampanas()
    {
        return campanas.Length;
    }
}
