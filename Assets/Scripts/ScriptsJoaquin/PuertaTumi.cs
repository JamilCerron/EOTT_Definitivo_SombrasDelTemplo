using UnityEngine;
using UnityEngine.UI;

public class PuertaTumi : MonoBehaviour
{
    private GestorTumi gestorTumi;
    [SerializeField] private Button botonFinal;

    private void Start()
    {
        gestorTumi = FindObjectOfType<GestorTumi>();

        if (botonFinal == null)
        {
            Debug.LogError("Botón Final no asignado en el inspector.");
            return;
        }

        botonFinal.onClick.RemoveAllListeners(); // Elimina cualquier listener previo
        botonFinal.onClick.AddListener(() =>
        {
            Debug.Log("Botón clickeado.");
            ComprobarFinal();
        });
    }

        private void ComprobarFinal()
    {
        Debug.Log("Click");
        gestorTumi.ComprobarVictoria();
    }
}
