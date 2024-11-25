using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscopetaRecogibleJ : MonoBehaviour
{
    [SerializeField] private GameObject señalInteractiva; // Señal visual
    [SerializeField] private GameObject escopeta;

    private void Start()
    {
        if (escopeta == null)
        {
            escopeta = GameObject.FindGameObjectWithTag("Escopeta");
        }

        if (señalInteractiva != null)
        {
            señalInteractiva.SetActive(false);
        }

        if (escopeta != null)
        {
            escopeta.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            señalInteractiva.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                escopeta.SetActive(true);
                var disparoEscopeta = other.GetComponent<DisparoEscopetaJ>();
                if (disparoEscopeta != null && !disparoEscopeta.TieneEscopeta())
                {
                    disparoEscopeta.EquiparEscopeta();
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            señalInteractiva.SetActive(false);
        }
    }
}