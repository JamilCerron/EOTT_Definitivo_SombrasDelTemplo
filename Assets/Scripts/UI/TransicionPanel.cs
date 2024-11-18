using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicionPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duracionFade; // Duración del fade-out
    [SerializeField] private float retrasoInicio; // Tiempo de espera antes de iniciar el fade-out

    private void Awake()
    {
        gameObject.SetActive(true);
        if (canvasGroup != null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        // Iniciar la corutina de retraso
        StartCoroutine(EsperarYEmpezarFade());
    }

    // Corutina para esperar antes de iniciar el fade-out
    private IEnumerator EsperarYEmpezarFade()
    {
        yield return new WaitForSeconds(retrasoInicio);
        StartCoroutine(FadeOut());
    }

    // Corutina para reducir la opacidad del panel
    private IEnumerator FadeOut()
    {
        float tiempo = 0f;

        // Mientras el tiempo sea menor que la duración, reducir opacidad
        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, tiempo / duracionFade);
            yield return null;
        }

        // Asegurarse de que el alpha es 0 al finalizar
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);

    }
}
