using UnityEngine;

public class CambioArma : MonoBehaviour
{

    public GameObject escopeta;
    public GameObject crucifijo;

    public int armaSeleccionada = 0;  // Por defecto inicia con la escopeta
    [SerializeField] public int tiempoUsoCrucifijo = 5;  // Duración del uso del crucifijo
    private float temporizadorCrucifijo = 0f;  // Control del tiempo restante de uso del crucifijo
    private PlayerStats playerStats;  // Referencia a PlayerStats

    public void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>(); // Busca el primer objeto PlayerStats en la escena
        if (playerStats == null)
        {
            Debug.LogError("No se encontró el componente PlayerStats en la escena.");
        }
        SeleccionarArma();
    }

    public void Update()
    {
        // Solo reducir el temporizador si el crucifijo está activo
        if (armaSeleccionada == 1 && temporizadorCrucifijo > 0)
        {
            temporizadorCrucifijo -= Time.deltaTime;

            // Si el temporizador llega a cero, cambiar a la escopeta
            if (temporizadorCrucifijo <= 0f)
            {
                CambiarAEscopeta();
                Debug.Log("El crucifijo ha expirado, cambiando a la escopeta.");
            }
        }

        // Cambiar de arma con las teclas 1 y 2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CambiarAEscopeta();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CambiarACrucifijo();
        }
        /*
        // Cambiar de arma con la rueda del mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f && armaSeleccionada < 1 && playerStats.ContadorCruz > 0)
        {
            CambiarACrucifijo(); // Cambia a crucifijo si tienes uno
        }
        else if (scroll < 0f && armaSeleccionada > 0)
        {
            CambiarAEscopeta(); // Cambia a escopeta
        }

        // Si intentas cambiar al crucifijo pero no está disponible, cambia a la escopeta
        if (armaSeleccionada == 1 && playerStats.ContadorCruz <= 0)
        {
            CambiarAEscopeta(); // Cambia automáticamente a escopeta
        }
        */
        SeleccionarArma();
    }

    public void CambiarACrucifijo()
    {
        /*
        Debug.Log("Intentando cambiar a Crucifijo. ContadorCruz: " + playerStats.ContadorCruz);
        if (playerStats.ContadorCruz > 0)
        {
            // Cambiar a crucifijo solo si hay cruz disponible
            if (armaSeleccionada != 1) // Verifica si no estás ya usando el crucifijo
            {
                armaSeleccionada = 1;  // Crucifijo
                temporizadorCrucifijo = tiempoUsoCrucifijo;  // Reiniciar el temporizador

                // Restar un contador de cruz
                playerStats.ContadorCruz--; // Asegúrate de que esto se está ejecutando
                Debug.Log("Usando el Crucifijo. ContadorCruz restante: " + playerStats.ContadorCruz);
            }
        }
        */
    }

    public void CambiarAEscopeta()
    {
        armaSeleccionada = 0; // Cambia a escopeta
        SeleccionarArma();
        Debug.Log("Se ha cambiado a la escopeta.");
    }

    public void SeleccionarArma()
    {
        // Activa la arma seleccionada y desactiva la otra
        escopeta.SetActive(armaSeleccionada == 0);
        crucifijo.SetActive(armaSeleccionada == 1);
    }

    // Método para reactivar el crucifijo al obtener uno nuevo
    public void ReactivarCrucifijo()
    {
        temporizadorCrucifijo = tiempoUsoCrucifijo;  // Reiniciar el temporizador
    }

    // Método para aumentar el tiempo del crucifijo
    public void AumentarTiempoCrucifijo(float tiempo)
    {
        temporizadorCrucifijo += tiempo; // Aumenta el tiempo de uso del crucifijo
    }

    public bool EsEscopetaActiva()
    {
        return armaSeleccionada == 0;
    }
}