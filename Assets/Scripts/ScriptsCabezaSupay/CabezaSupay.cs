using System.Collections;
using UnityEngine;

public class CabezaSupay : MonoBehaviour
{
    [Header("Configuración de Disparo")]
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float rangoDisparo = 20f;
    public float tiempoEntreDisparos = 1.5f;
    [SerializeField] private float velocidadBala = 4f;

    [Header("Configuración de Golpes")]
    private const int golpesMaximos = 3;
    private const float tiempoEsperaAtaque = 10f;

    private Transform jugador;
    private int golpesRecibidos = 0;
    private bool puedeAtacar = true;
    private float tiempoProximoDisparo = 0f;
    private bool golpeando = false;
    private Vector3 posicionInicial;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
        posicionInicial = transform.position;

        if (jugador == null)
        {
            Debug.LogError("Jugador no encontrado. Asegúrate de que el jugador tenga la etiqueta 'Player'.");
        }
    }

    private void Update()
    {
        if (jugador == null || !puedeAtacar) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);
        if (distancia <= rangoDisparo && Time.time >= tiempoProximoDisparo)
        {
            Disparar();
            tiempoProximoDisparo = Time.time + tiempoEntreDisparos;
        }
    }

    private void Disparar()
    {
        if (balaPrefab == null || puntoDisparo == null)
        {
            Debug.LogError("BalaPrefab o PuntoDisparo no están asignados.");
            return;
        }

        Vector3 direccionAlJugador = (jugador.position - puntoDisparo.position).normalized;

        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position + new Vector3(0, -1f, 0), Quaternion.identity);
        bala.transform.rotation = Quaternion.LookRotation(direccionAlJugador);

        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = (direccionAlJugador * velocidadBala) + new Vector3(0, 0.35f, 0);
        }
        else
        {
            Debug.LogError("El prefab de bala no tiene un componente Rigidbody.");
        }
    }

    //public void RecibirGolpe(int dano)
    //{
    //    if (!golpeando)
    //    {
    //        StartCoroutine(RegistrarGolpe(dano));
    //    }
    //}

    private IEnumerator RegistrarGolpe(int dano)
    {
        golpeando = true;

        golpesRecibidos += dano;
        Debug.Log($"Enemigo recibió un golpe. Golpes recibidos: {golpesRecibidos}");

        if (golpesRecibidos >= golpesMaximos)
        {
            Debug.Log("Enemigo baja la cabeza y deja de atacar.");
            golpesRecibidos = 0;

            puedeAtacar = false;

            // Baja la cabeza.
            transform.position += new Vector3(0, -1f, 0);
            yield return new WaitForSeconds(tiempoEsperaAtaque);

            // Regresa a la posición inicial.
            transform.position = posicionInicial;
            puedeAtacar = true;
            Debug.Log("Enemigo vuelve a atacar.");
        }

        yield return new WaitForSeconds(0.1f);
        golpeando = false;
    }

    public void IntentarAtacar()
    {
        Debug.Log(puedeAtacar ? "El enemigo está atacando..." : "El enemigo no puede atacar aún. Esperando...");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangoDisparo);
    }
}

