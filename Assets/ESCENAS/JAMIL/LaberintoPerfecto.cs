using UnityEngine;
using System.Collections.Generic;

public class LaberintoPerfecto : MonoBehaviour
{
    public GameObject paredPrefab; // Prefab de pared
    public GameObject inicioPrefab; // Prefab para marcar el inicio
    public GameObject finalPrefab; // Prefab para marcar el final
    public int ancho = 10; // Anchura del laberinto
    public int alto = 10; // Altura del laberinto
    public float tama�oPared = 1.0f; // Tama�o de cada pared

    private bool[,] celdas; // Matriz para marcar celdas visitadas
    private List<Vector2Int> direcciones = new List<Vector2Int>
    {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };

    void Start()
    {
        celdas = new bool[ancho, alto];
        Debug.Log("Generando el laberinto...");
        GenerarLaberinto(new Vector2Int(0, 0)); // Genera el laberinto desde el inicio
        Debug.Log("Construyendo el laberinto...");
        ConstruirLaberinto();
        Debug.Log("�Laberinto generado correctamente!");
    }

    void GenerarLaberinto(Vector2Int actual)
    {
        celdas[actual.x, actual.y] = true; // Marca la celda actual como visitada
        Debug.Log($"Celda visitada: {actual}");

        // Baraja las direcciones para crear caminos aleatorios
        List<Vector2Int> direccionesMezcladas = new List<Vector2Int>(direcciones);
        Shuffle(direccionesMezcladas);

        foreach (var direccion in direccionesMezcladas)
        {
            Vector2Int vecino = actual + direccion;
            Vector2Int entre = actual + direccion / 2;

            // Comprueba que el vecino est� dentro del laberinto y no visitado
            if (EnLimites(vecino) && !celdas[vecino.x, vecino.y])
            {
                celdas[entre.x, entre.y] = true; // Marca el paso entre celdas
                celdas[vecino.x, vecino.y] = true; // Marca el vecino
                Debug.Log($"Celda marcada: {vecino}");
                GenerarLaberinto(vecino); // Recursivamente visita al vecino
            }
        }
    }

    void ConstruirLaberinto()
    {
        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                if (!celdas[x, y])
                {
                    Vector3 posicion = new Vector3(x * tama�oPared, 0, y * tama�oPared);
                    GameObject pared = Instantiate(paredPrefab, posicion, Quaternion.identity, transform);

                    // Ajustar la escala del prefab al tama�o de la celda
                    pared.transform.localScale = new Vector3(tama�oPared, pared.transform.localScale.y, tama�oPared);

                    Debug.Log($"Pared creada en posici�n: {posicion} con escala: {pared.transform.localScale}");
                }
            }
        }

        // Coloca inicio y final
        Vector3 posicionInicio = new Vector3(0, 0, 0);
        GameObject inicio = Instantiate(inicioPrefab, posicionInicio, Quaternion.identity, transform);
        inicio.transform.localScale = new Vector3(tama�oPared, inicio.transform.localScale.y, tama�oPared);

        Vector3 posicionFinal = new Vector3((ancho - 1) * tama�oPared, 0, (alto - 1) * tama�oPared);
        GameObject final = Instantiate(finalPrefab, posicionFinal, Quaternion.identity, transform);
        final.transform.localScale = new Vector3(tama�oPared, final.transform.localScale.y, tama�oPared);
    }


    bool EnLimites(Vector2Int posicion)
    {
        return posicion.x >= 0 && posicion.x < ancho && posicion.y >= 0 && posicion.y < alto;
    }

    void Shuffle<T>(List<T> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            int rnd = Random.Range(0, lista.Count);
            T temp = lista[i];
            lista[i] = lista[rnd];
            lista[rnd] = temp;
        }
    }
}
