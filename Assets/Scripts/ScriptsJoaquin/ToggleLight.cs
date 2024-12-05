using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private GameObject luz; // Luz que se activa/desactiva
    [SerializeField] private KeyCode teclaToggle = KeyCode.Alpha3; // Tecla configurable para alternar la luz

    private bool luzActiva = false; // Estado inicial de la luz

    private void Start()
    {
        if (luz != null)
        {
            luz.SetActive(false); // Asegurar que la luz esté apagada al inicio
        }
        else
        {
            Debug.LogWarning("La referencia a la luz no está asignada en el inspector.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(teclaToggle) && luz != null)
        {
            luzActiva = !luzActiva; // Alternar el estado de la luz
            luz.SetActive(luzActiva); // Activar o desactivar la luz según el estado
        }
    }
}
