using System.Collections;
using UnityEngine;

public class EspectroAtaque : MonoBehaviour
{
    [Header("Configuración del ataque del Espectro")]
    [SerializeField] private float corduraDañoEspecial = 20f;
    [SerializeField] private float corduraDaño = 5f;
    [SerializeField] private float vidaDaño = 5f;
    [SerializeField] private float fovJugador = 90f; // Ángulo de visión del jugador
    private Transform jugador;
    public GameObject jugadorObjeto; // Asume que hay un script para manejar los stats del jugador

    [SerializeField] private float tiempoEntreReducciones = 10f;
    public bool enCooldown = false;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void AtaqueBasico()
    {
        jugadorObjeto.GetComponent<PlayerStats>().RecibirDaño((int)vidaDaño);
        jugadorObjeto.GetComponent<PlayerStats>().ReducirCordura((int)corduraDaño);
    }

    void AtaqueEspecial()
    {
        jugadorObjeto.GetComponent<PlayerStats>().ReducirCordura(corduraDañoEspecial);
    }

    //IEnumerator ReducirCorduraConCooldown()
    //{
    //    enCooldown = true;
    //    AtaqueBasico();
    //    yield return new WaitForSeconds(tiempoEntreReducciones);  // Espera antes de volver a reducir
    //    enCooldown = false;
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && !enCooldown)
    //    {
    //        StartCoroutine(ReducirCorduraConCooldown());
    //    }
    //}
}