using System.Collections;
using UnityEngine;

public class EspectroAtaque : MonoBehaviour
{
    [Header("Configuración del ataque del Espectro")]
    [SerializeField] private float rangoAtaque = 5f; // Distancia para atacar al jugador
    [SerializeField] private float corduraDañoEspecial = 20f;
    [SerializeField] private float corduraDaño = 20f;
    [SerializeField] private float vidaDaño = 10f;
    [SerializeField] private float tiempoEntreReducciones = 3f;

    private Transform jugador;
    private PlayerStats jugadorStats;
    private bool enCooldown = false;

    void Start()
    {
        GameObject jugadorObjeto = GameObject.FindGameObjectWithTag("Player");
        if (jugadorObjeto != null)
        {
            jugador = jugadorObjeto.transform;
            jugadorStats = jugadorObjeto.GetComponent<PlayerStats>();
        }
    }

    void Update()
    {
        if (jugador == null || jugadorStats == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= rangoAtaque && !enCooldown)
        {
            StartCoroutine(ReducirCorduraConCooldown());
        }
    }

    public void AtaqueBasico()
    {
        if (jugadorStats != null)
        {
            jugadorStats.RecibirDaño((int)vidaDaño);
            jugadorStats.ReducirCordura((int)corduraDaño);
        }
    }

    public void AtaqueEspecial()
    {
        if (jugadorStats != null)
        {
            jugadorStats.ReducirCordura(corduraDañoEspecial);
        }
    }

    private IEnumerator ReducirCorduraConCooldown()
    {
        enCooldown = true;

        if (jugadorStats != null)
        {
            jugadorStats.ReducirCordura(2);
        }

        yield return new WaitForSeconds(tiempoEntreReducciones);
        enCooldown = false;
    }
}
