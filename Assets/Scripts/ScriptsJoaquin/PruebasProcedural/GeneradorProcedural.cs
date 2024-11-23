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
            Debug.LogError("No se ha configurado una versi�n v�lida.");
            return;
        }

        // Generar objetos en las posiciones especificadas en la configuraci�n
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
                Debug.LogWarning($"El prefab en el �ndice {i} no est� asignado en la configuraci�n {configuracionActual.name}.");
            }
        }
    }
}
