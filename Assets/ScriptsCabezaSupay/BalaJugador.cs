using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaJugador : MonoBehaviour
{
    public float velocidad = 10f;
    public int dano = 1;
    private bool haImpactado = false;

    private void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!haImpactado && other.CompareTag("ENEMIGO"))
        {
            haImpactado = true;

            CabezaSupay enemigo = other.GetComponent<CabezaSupay>();
            if (enemigo != null)
            {
                enemigo.RecibirGolpe(1);
            }

            // Destruye la bala después del impacto
            Destroy(gameObject);
        }
    }
}
