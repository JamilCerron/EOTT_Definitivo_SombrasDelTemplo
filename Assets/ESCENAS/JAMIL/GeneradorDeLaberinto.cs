using UnityEngine;

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
        GenerarLaberinto(0, 0, ancho, alto);
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

    void EliminarParedesInteriores()
    {
        // Eliminar paredes horizontales interiores
        for (int x = 1; x < ancho - 1; x++)
        {
            for (int y = 1; y < alto; y++)
            {
                if (paredesHorizontales[x, y] && Random.Range(0f, 1f) < 0.5f)
                {
                    paredesHorizontales[x, y] = false;
                }
            }
        }

        // Eliminar paredes verticales interiores
        for (int x = 1; x < ancho; x++)
        {
            for (int y = 1; y < alto - 1; y++)
            {
                if (paredesVerticales[x, y] && Random.Range(0f, 1f) < 0.5f)
                {
                    paredesVerticales[x, y] = false;
                }
            }
        }

        // Asegúrate de eliminar las paredes en las celdas del punto de inicio
        paredesHorizontales[puntoInicio.x, puntoInicio.y] = false;
        if (puntoInicio.y > 0)
        {
            paredesHorizontales[puntoInicio.x, puntoInicio.y - 1] = false;
            paredesHorizontales[puntoInicio.x-1, puntoInicio.y - 1] = false;
            paredesHorizontales[puntoInicio.x + 1, puntoInicio.y - 1] = false;
        }
        if (puntoInicio.x > 0)
        {
            paredesVerticales[puntoInicio.x - 1, puntoInicio.y] = false;
            paredesVerticales[puntoInicio.x-1 , puntoInicio.y-1] = false;
            paredesVerticales[puntoInicio.x - 1, puntoInicio.y + 1] = false;
        }

        // Asegúrate de eliminar las paredes en las celdas del punto final
        paredesHorizontales[puntoFinal.x, puntoFinal.y] = false;
        if (puntoFinal.y < alto)
        {
            paredesHorizontales[puntoFinal.x, puntoFinal.y + 1] = false;
        }
        if (puntoFinal.x < ancho)
        {
            paredesVerticales[puntoFinal.x + 1, puntoFinal.y] = false;
        }
    }




    void GenerarLaberinto(int x, int y, int ancho, int alto)
    {
        if (ancho <= 1 || alto <= 1)
            return;

        bool dividirHorizontal = Random.Range(0, 2) == 0;

        if (ancho > alto)
            dividirHorizontal = false;
        else if (alto > ancho)
            dividirHorizontal = true;

        if (dividirHorizontal)
        {
            int fila = y + Random.Range(1, alto);
            for (int i = x; i < x + ancho; i++)
                paredesHorizontales[i, fila] = true;

            int hueco = x + Random.Range(0, ancho);
            paredesHorizontales[hueco, fila] = false;

            GenerarLaberinto(x, y, ancho, fila - y);
            GenerarLaberinto(x, fila, ancho, y + alto - fila);
        }
        else
        {
            int columna = x + Random.Range(1, ancho);
            for (int i = y; i < y + alto; i++)
                paredesVerticales[columna, i] = true;

            int hueco = y + Random.Range(0, alto);
            paredesVerticales[columna, hueco] = false;

            GenerarLaberinto(x, y, columna - x, alto);
            GenerarLaberinto(columna, y, x + ancho - columna, alto);
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
        // Primero genera el laberinto
        GenerarLaberinto(0, 0, ancho, alto);

        // Luego elimina las paredes interiores
        EliminarParedesInteriores();

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

