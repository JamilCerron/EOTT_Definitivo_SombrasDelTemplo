using System.Collections;
using UnityEngine;

public class EspectroAtaque : MonoBehaviour
{
    [Header("Configuraci�n del ataque del Espectro")]
    [SerializeField] private float corduraDa�oEspecial = 20f;
    [SerializeField] private float corduraDa�o = 5f;
    [SerializeField] private float vidaDa�o = 5f;
    [SerializeField] private float fovJugador = 90f; // �ngulo de visi�n del jugador
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
        jugadorObjeto.GetComponent<PlayerStats>().RecibirDa�o((int)vidaDa�o);
        jugadorObjeto.GetComponent<PlayerStats>().ReducirCordura((int)corduraDa�o);
    }

    void AtaqueEspecial()
    {
        jugadorObjeto.GetComponent<PlayerStats>().ReducirCordura(corduraDa�oEspecial);
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