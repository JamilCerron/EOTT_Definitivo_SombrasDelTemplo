using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorProcedural : MonoBehaviour
{
    private VersionConfig configuracionActual;

    public void ConfigurarVersion(VersionConfig configuracion)
    {
        configuracionActual = configuracion;
        GenerarObjetos();
    }

    private void GenerarObjetos()
    {
        if (configuracionActual == null)
        {
            Debug.LogError("No se ha configurado una versión válida.");
            return;
        }

        // Generar objetos en las posiciones especificadas en la configuración
        for (int i = 0; i < configuracionActual.tiposDeObjetos.Length; i++)
        {
            GameObject prefab = configuracionActual.tiposDeObjetos[i];
            Vector3 posicion = configuracionActual.posiciones[i];

            if (prefab != null)
            {
                Instantiate(prefab, posicion, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"El prefab en el índice {i} no está asignado en la configuración {configuracionActual.name}.");
            }
        }
    }
}
