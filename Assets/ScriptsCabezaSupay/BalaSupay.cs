using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaSupay : MonoBehaviour
{
    public float velocidad = 10f;
    public int dano = 20;

    private void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLAYER"))
        {
            MoverJugador jugador = other.GetComponent<MoverJugador>();
            if (jugador != null)
            {
                jugador.ReducirSalud(dano);
            }
            Destroy(gameObject); // Destruye la bala después del impacto
        }
    }

}
