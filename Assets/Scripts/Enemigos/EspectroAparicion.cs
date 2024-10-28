using UnityEngine;

public class EspectroAparicion : MonoBehaviour
{
    [SerializeField] private GameObject espectroPrefab;
    [SerializeField] private float tiempoEntreApariciones = 10f;
    [SerializeField] private float tiempoActual;
    [SerializeField] private Transform[] puntosAparicion;
    private PlayerStats statsJugador;

    void Start()
    {
        statsJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {
        tiempoActual += Time.deltaTime;

        if (tiempoActual >= tiempoEntreApariciones && statsJugador.CorduraActual() <= 50)
        {
            SpawnEspectro();
            tiempoActual = 0f; // Resetea el contador
        }
    }

    void SpawnEspectro()
    {
        Transform punto = puntosAparicion[Random.Range(0, puntosAparicion.Length)];
        Instantiate(espectroPrefab, punto.position, punto.rotation);
        Debug.Log("Espectro ha aparecido.");
    }
}
