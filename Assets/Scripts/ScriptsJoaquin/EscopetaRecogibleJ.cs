using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscopetaRecogibleJ : MonoBehaviour
{
    [SerializeField] private GameObject se�alInteractiva; // Se�al visual
    [SerializeField] private GameObject escopeta;

    private void Start()
    {
        if (escopeta == null)
        {
            escopeta = GameObject.FindGameObjectWithTag("Escopeta");
        }

        if (se�alInteractiva != null)
        {
            se�alInteractiva.SetActive(false);
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
            se�alInteractiva.SetActive(true);
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
            se�alInteractiva.SetActive(false);
        }
    }
}