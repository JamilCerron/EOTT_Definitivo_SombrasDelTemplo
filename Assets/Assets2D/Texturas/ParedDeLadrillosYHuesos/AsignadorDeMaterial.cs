using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AsignadorDeMaterial : MonoBehaviour
{
    public Material newMaterial; // Material que quieres asignar
    public GameObject rootObject; // Objeto raíz (por ejemplo, "Paredes")

    [ContextMenu("Asignar Material al Elemento 1")]
    void AssignMaterialToChildren()
    {
        if (rootObject == null || newMaterial == null)
        {
            Debug.LogError("Por favor, asigna un Root Object y un Material.");
            return;
        }

        foreach (Transform parent in rootObject.transform)
        {
            foreach (Transform child in parent)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null && renderer.sharedMaterials.Length > 1) // Verifica que haya al menos 2 materiales
                {
                    // Reemplazar el material en el índice 1
                    Material[] materials = renderer.sharedMaterials;
                    materials[1] = newMaterial;
                    renderer.sharedMaterials = materials;

                    // Marcar el objeto como modificado para guardar el cambio en la escena
                    EditorUtility.SetDirty(renderer);
                }
            }
        }

        Debug.Log("Material asignado al Elemento 1 de todos los hijos.");
    }
}
