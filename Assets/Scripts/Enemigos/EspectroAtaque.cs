using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EspectroAtaque : MonoBehaviour
{
    [Header("Configuración del ataque del Espectro")]
    [SerializeField] private float corduraDañoEspecial = 20f;
    [SerializeField] private float corduraDaño = 20f;
    [SerializeField] private float vidaDaño = 10f;
    //[SerializeField] private float fovJugador = 90f; 
    private Transform jugador;
    [SerializeField] private GameObject jugadorObjeto; 

    [SerializeField] private float tiempoEntreReducciones = 3f;
    private bool enCooldown = false;

    void Start()
    {
        if (jugadorObjeto == null)
        {
            jugadorObjeto = GameObject.FindGameObjectWithTag("Player");
        }
        jugador = jugadorObjeto?.transform;

        
    }

    public void AtaqueBasico()
    {
        PlayerStats stats = jugadorObjeto.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.RecibirDaño((int)vidaDaño);
            stats.ReducirCordura((int)corduraDaño);
        }
    }

    public void AtaqueEspecial()
    {
        PlayerStats stats = jugadorObjeto.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.ReducirCordura(corduraDañoEspecial);
        }
    }

    IEnumerator ReducirCorduraConCooldown()
    {
        PlayerStats stats = jugadorObjeto.GetComponent<PlayerStats>();
        enCooldown = true;
        stats.ReducirCordura((int)3);
        yield return new WaitForSeconds(tiempoEntreReducciones);  
        enCooldown = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !enCooldown)
        {
            StartCoroutine(ReducirCorduraConCooldown());
        }
    }
}
