using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Versiones Disponibles")]
    [SerializeField] private VersionConfig[] configuracionesVersiones;

    [Header("Generador Procedural")]
    [SerializeField] private GeneradorProcedural generadorProcedural;

    private VersionConfig versionSeleccionada;

    private void Start()
    {
        if (configuracionesVersiones.Length == 0)
        {
            Debug.LogError("No se han asignado configuraciones de versiones en el GameManager.");
            return;
        }

        SeleccionarVersion();
    }

    private void SeleccionarVersion()
    {
        // Seleccionar una versión aleatoria (o puedes implementar lógica diferente)
        int indiceVersion = Random.Range(0, configuracionesVersiones.Length);
        versionSeleccionada = configuracionesVersiones[indiceVersion];
        Debug.Log($"Versión seleccionada: {versionSeleccionada.name}");

        if (generadorProcedural != null)
        {
            generadorProcedural.ConfigurarVersion(versionSeleccionada);
        }
        else
        {
            Debug.LogError("GeneradorProcedural no asignado en el GameManager.");
        }
    }

    public void GuardarVersion()
    {
        if (versionSeleccionada == null)
        {
            Debug.LogError("No hay una versión seleccionada para guardar.");
            return;
        }

        // Encuentra todos los objetos en escena con tag "Spawnable" y guarda sus posiciones
        GameObject[] objetos = GameObject.FindGameObjectsWithTag("Spawnable");
        versionSeleccionada.tiposDeObjetos = new GameObject[objetos.Length];
        versionSeleccionada.posiciones = new Vector3[objetos.Length];

        for (int i = 0; i < objetos.Length; i++)
        {
            versionSeleccionada.tiposDeObjetos[i] = objetos[i];
            versionSeleccionada.posiciones[i] = objetos[i].transform.position;
        }

        Debug.Log("Versión guardada con éxito.");
    }
}
