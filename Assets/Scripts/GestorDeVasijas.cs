using UnityEngine;
using System.Collections.Generic;

public class GestorDeVasijas : MonoBehaviour
{
    [SerializeField] private Transform padreVasijas;  
    [SerializeField] private List<GameObject> prefabsEspecificos;  
    private List<Transform> vasijasRestantes = new List<Transform>();

    private void Start()
    {
        foreach (Transform vasija in padreVasijas)
        {
            vasijasRestantes.Add(vasija);
        }

        ReemplazarVasijasEspecificas();
    }

    private void ReemplazarVasijasEspecificas()
    {
     
        Shuffle(vasijasRestantes);

       
        int reemplazos = Mathf.Min(prefabsEspecificos.Count, vasijasRestantes.Count);

        for (int i = 0; i < reemplazos; i++)
        {
            Transform vasija = vasijasRestantes[i];
            GameObject prefabEspecifico = prefabsEspecificos[i];

            Instantiate(prefabEspecifico, vasija.position, vasija.rotation, padreVasijas);

            Destroy(vasija.gameObject);

            Debug.Log($"Vasija reemplazada por prefab: {prefabEspecifico.name} en posición {vasija.position}");
        }

        vasijasRestantes.RemoveRange(0, reemplazos);
    }

   
    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
