using UnityEngine;
using System.Linq; // Necesario para usar LINQ

public class GeneradorDeLaberinto : MonoBehaviour
{
    public GameObject paredPrefab; // Prefab de las paredes
    public GameObject inicioPrefab; // Prefab para marcar el inicio
    public GameObject finalPrefab; // Prefab para marcar el final
    public int ancho = 10; // Anchura del laberinto
    public int alto = 10; // Altura del laberinto
    public float tamañoPared = 1.0f; // Tamaño de las paredes

    private bool[,] celdas; // Celdas del laberinto
    private bool[,] paredesHorizontales; // Paredes horizontales
    private bool[,] paredesVerticales; // Paredes verticales
    private Vector2Int puntoInicio;
    private Vector2Int puntoFinal;

    void Start()
    {
        InicializarEstructuras();
        GenerarLaberintoDFS(0, 0); // Generación del laberinto con DFS
        DefinirInicioYFinal();
        ConstruirLaberinto();
    }

    void InicializarEstructuras()
    {
        celdas = new bool[ancho, alto];
        paredesHorizontales = new bool[ancho, alto + 1];
        paredesVerticales = new bool[ancho + 1, alto];

        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y <= alto; y++)
                paredesHorizontales[x, y] = true;
        }

        for (int x = 0; x <= ancho; x++)
        {
            for (int y = 0; y < alto; y++)
                paredesVerticales[x, y] = true;
        }
    }

    void GenerarLaberintoDFS(int x, int y)
    {
        celdas[x, y] = true; // Marca la celda como visitada

        // Direcciones aleatorias
        Vector2Int[] direcciones = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        System.Random rng = new System.Random();
        direcciones = direcciones.OrderBy(d => rng.Next()).ToArray();

        foreach (var dir in direcciones)
        {
            int nx = x + dir.x;
            int ny = y + dir.y;

            // Verifica si la celda vecina está dentro de los límites y no ha sido visitada
            if (nx >= 0 && nx < ancho && ny >= 0 && ny < alto && !celdas[nx, ny])
            {
                // Elimina la pared entre las celdas
                if (dir == Vector2Int.up) paredesHorizontales[x, y + 1] = false;
                else if (dir == Vector2Int.down) paredesHorizontales[x, y] = false;
                else if (dir == Vector2Int.left) paredesVerticales[x, y] = false;
                else if (dir == Vector2Int.right) paredesVerticales[x + 1, y] = false;

                // Llama recursivamente a la celda vecina
                GenerarLaberintoDFS(nx, ny);
            }
        }
    }

    void DefinirInicioYFinal()
    {
        puntoInicio = new Vector2Int(0, 0);
        puntoFinal = new Vector2Int(ancho - 1, alto - 1);

        // Asegura que las celdas de inicio y final no tengan paredes bloqueándolas
        paredesHorizontales[0, 0] = false; // Entrada abierta
        paredesHorizontales[ancho - 1, alto] = false; // Salida abierta
    }

    void ConstruirLaberinto()
    {
        // Instancia las paredes en la escena
        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y < alto + 1; y++)
            {
                if (paredesHorizontales[x, y])
                {
                    Vector3 posicion = new Vector3(x * tamañoPared, 0, y * tamañoPared - tamañoPared / 2);
                    Instantiate(paredPrefab, posicion, Quaternion.identity, transform);
                }
            }
        }

        for (int x = 0; x < ancho + 1; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                if (paredesVerticales[x, y])
                {
                    Vector3 posicion = new Vector3(x * tamañoPared - tamañoPared / 2, 0, y * tamañoPared);
                    Instantiate(paredPrefab, posicion, Quaternion.Euler(0, 90, 0), transform);
                }
            }
        }

        // Coloca el marcador de inicio y final
        Vector3 posicionInicio = new Vector3(puntoInicio.x * tamañoPared, 0, puntoInicio.y * tamañoPared);
        Instantiate(inicioPrefab, posicionInicio, Quaternion.identity, transform);

        Vector3 posicionFinal = new Vector3(puntoFinal.x * tamañoPared, 0, puntoFinal.y * tamañoPared);
        Instantiate(finalPrefab, posicionFinal, Quaternion.identity, transform);
    }
}
