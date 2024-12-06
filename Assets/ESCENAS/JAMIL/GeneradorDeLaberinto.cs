using UnityEngine;
using System.Linq;
using System.Collections.Generic; // Asegúrate de incluir esto para usar List

public class GeneradorDeLaberinto : MonoBehaviour
{
    public GameObject paredPrefab; // Prefab de las paredes
    public GameObject inicioPrefab; // Prefab para marcar el inicio
    public GameObject finalPrefab; // Prefab para marcar el final
    public GameObject enemigoPrefab; // Prefab del enemigo
    public GameObject vasijaPrefab; // Prefab de las vasijas
    public GameObject vasijaConLlavePrefab; // Prefab de la vasija con la llave
    public int cantidadEnemigos = 5; // Cantidad de enemigos a generar
    public int ancho = 10; // Anchura del laberinto
    public int alto = 10; // Altura del laberinto
    public float tamañoCelda = 1.0f; // Tamaño de cada celda
    public float grosorPared = 0.1f; // Grosor manual de las paredes, ajustable desde el Inspector

    private bool[,] celdas; // Celdas del laberinto
    private bool[,] paredesHorizontales; // Paredes horizontales
    private bool[,] paredesVerticales; // Paredes verticales
    private List<Vector3> posicionesVasijas; // Lista para almacenar posiciones de las vasijas

    public Vector2Int puntoInicio = new Vector2Int(0, 0); // Posición del inicio
    public Vector2Int puntoFinal = new Vector2Int(9, 9); // Posición del final

    void Start()
    {
        ValidarPosiciones(); // Asegurarse de que las posiciones sean válidas
        InicializarEstructuras();
        GenerarLaberintoDFS(puntoInicio.x, puntoInicio.y); // Generación del laberinto desde el inicio
        CrearEntradaYSalida(); // Crear una entrada y salida para el laberinto
        ConstruirLaberinto();
        GenerarEnemigos(); // Generar enemigos en el laberinto
        ReemplazarVasijaConLlave(); // Reemplazar una vasija con la vasija que tiene la llave
    }

    void ValidarPosiciones()
    {
        puntoInicio.x = Mathf.Clamp(puntoInicio.x, 0, ancho - 1);
        puntoInicio.y = Mathf.Clamp(puntoInicio.y, 0, alto - 1);
        puntoFinal.x = Mathf.Clamp(puntoFinal.x, 0, ancho - 1);
        puntoFinal.y = Mathf.Clamp(puntoFinal.y, 0, alto - 1);

        if (puntoInicio == puntoFinal)
        {
            Debug.LogWarning("El punto de inicio y el punto final son iguales. Se ajustará automáticamente.");
            puntoFinal = new Vector2Int(ancho - 1, alto - 1);
        }
    }

    void InicializarEstructuras()
    {
        celdas = new bool[ancho, alto];
        paredesHorizontales = new bool[ancho, alto + 1];
        paredesVerticales = new bool[ancho + 1, alto];
        posicionesVasijas = new List<Vector3>(); // Inicializar la lista de posiciones de vasijas

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

        Vector2Int[] direcciones = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        System.Random rng = new System.Random();
        direcciones = direcciones.OrderBy(d => rng.Next()).ToArray();

        foreach (var dir in direcciones)
        {
            int nx = x + dir.x;
            int ny = y + dir.y;

            if (nx >= 0 && nx < ancho && ny >= 0 && ny < alto && !celdas[nx, ny])
            {
                if (dir == Vector2Int.up) paredesHorizontales[x, y + 1] = false;
                else if (dir == Vector2Int.down) paredesHorizontales[x, y] = false;
                else if (dir == Vector2Int.left) paredesVerticales[x, y] = false;
                else if (dir == Vector2Int.right) paredesVerticales[x + 1, y] = false;

                GenerarLaberintoDFS(nx, ny);
            }
        }
    }

    void CrearEntradaYSalida()
    {
        // Crear un espacio libre para la entrada en la posición de inicio
        if (puntoInicio.y == 0) // Si el inicio está en la fila inferior
            paredesHorizontales[puntoInicio.x, puntoInicio.y] = false;
        else if (puntoInicio.y == alto - 1) // Si el inicio está en la fila superior
            paredesHorizontales[puntoInicio.x, puntoInicio.y + 1] = false;
        else if (puntoInicio.x == 0) // Si el inicio está en la columna izquierda
            paredesVerticales[puntoInicio.x, puntoInicio.y] = false;
        else if (puntoInicio.x == ancho - 1) // Si el inicio está en la columna derecha
            paredesVerticales[puntoInicio.x + 1, puntoInicio.y] = false;

        // Crear un espacio libre para la salida en la posición final
        if (puntoFinal.y == 0) // Si el final está en la fila inferior
            paredesHorizontales[puntoFinal.x, puntoFinal.y] = false;
        else if (puntoFinal.y == alto - 1) // Si el final está en la fila superior
            paredesHorizontales[puntoFinal.x, puntoFinal.y + 1] = false;
        else if (puntoFinal.x == 0) // Si el final está en la columna izquierda
            paredesVerticales[puntoFinal.x, puntoFinal.y] = false;
        else if (puntoFinal.x == ancho - 1) // Si el final está en la columna derecha
            paredesVerticales[puntoFinal.x + 1, puntoFinal.y] = false;
    }

    void ConstruirLaberinto()
    {
        Vector3 posicionBase = transform.position;

        // Definir un margen desde el perímetro
        int margen = 1; // Cambia este valor según el tamaño del laberinto

        // Generar paredes horizontales
        for (int x = 0; x < ancho; x++)
        {
            for (int y = 0; y <= alto; y++)
            {
                if (paredesHorizontales[x, y])
                {
                    Vector3 posicion = posicionBase + new Vector3(x * tamañoCelda, 0, y * tamañoCelda - grosorPared / 2);
                    CrearPared(posicion, Quaternion.identity);
                }
            }
        }

        // Generar paredes verticales
        for (int x = 0; x <= ancho; x++)
        {
            for (int y = 0; y < alto; y++)
            {
                if (paredesVerticales[x, y])
                {
                    Vector3 posicion = posicionBase + new Vector3(x * tamañoCelda - grosorPared / 2, 0, y * tamañoCelda);
                    CrearPared(posicion, Quaternion.Euler(0, 90, 0));
                }
            }
        }

        // Instanciar vasijas solo en celdas internas
        for (int x = margen; x < ancho - margen; x++)
        {
            for (int y = margen; y < alto - margen; y++)
            {
                // Verificar si hay un pasillo en la celda
                if (celdas[x, y]) // Asegúrate de que 'celdas' contenga información sobre pasillos
                {
                    Vector3 posicionVasija = posicionBase + new Vector3(x * tamañoCelda, 0, y * tamañoCelda);
                    InstanciarVasija(posicionVasija); // Ajustar la posición de la vasija
                }
            }
        }
    }
    void InstanciarVasija(Vector3 posicion)
    {
        // Asegúrate de que la posición Y sea 0 para que la vasija esté sobre el suelo
        posicion.y = 0; // Ajustar la altura de la vasija al nivel del suelo
        Instantiate(vasijaPrefab, posicion + new Vector3(0, 2.1f, 0), Quaternion.identity, transform);
        posicionesVasijas.Add(posicion); // Agregar la posición de la vasija a la lista
    }

    void ReemplazarVasijaConLlave()
    {
        if (posicionesVasijas.Count > 0)
        {
            // Seleccionar una posición aleatoria de la lista de vasijas
            int indiceAleatorio = Random.Range(0, posicionesVasijas.Count);
            Vector3 posicionVasijaConLlave = posicionesVasijas[indiceAleatorio];

            // Instanciar la vasija con la llave en la posición seleccionada
            Instantiate(vasijaConLlavePrefab, posicionVasijaConLlave + new Vector3(0, 2.1f, 0), Quaternion.identity, transform);

            Debug.Log("Vasija con llave instanciada en: " + posicionVasijaConLlave);

            // Opcional: eliminar la posición de la vasija original de la lista si es necesario
            posicionesVasijas.RemoveAt(indiceAleatorio);
        }
        else
        {
            Debug.LogWarning("No hay posiciones de vasijas disponibles para reemplazar.");
        }
    }

    void CrearPared(Vector3 posicion, Quaternion rotacion)
    {
        Instantiate(paredPrefab, posicion, rotacion, transform);
    }

    void GenerarEnemigos()
    {
        int enemigosGenerados = 0;
        System.Random rng = new System.Random();

        while (enemigosGenerados < cantidadEnemigos)
        {
            int x = rng.Next(0, ancho);
            int y = rng.Next(0, alto);

            // Verificar si la celda está vacía (es un pasillo)
            if (celdas[x, y])
            {
                // Calcular la posición en el mundo
                Vector3 posicionEnemigo = transform.position + new Vector3(x * tamañoCelda, 0, y * tamañoCelda);
                Instantiate(enemigoPrefab, posicionEnemigo, Quaternion.identity);
                enemigosGenerados++;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 size = new Vector3(ancho * tamañoCelda, 0, alto * tamañoCelda);
        Vector3 center = transform.position + size / 2 - new Vector3(tamañoCelda / 2, 0, tamañoCelda / 2);
        Gizmos.DrawWireCube(center, size);

        Gizmos.color = Color.blue;
        Vector3 inicioPos = transform.position + new Vector3(puntoInicio.x * tamañoCelda, 0, puntoInicio.y * tamañoCelda);
        Gizmos.DrawSphere(inicioPos, 0.5f);

        Gizmos.color = Color.red;
        Vector3 finalPos = transform.position + new Vector3(puntoFinal.x * tamañoCelda, 0, puntoFinal.y * tamañoCelda);
        Gizmos.DrawSphere(finalPos, 0.5f);
    }
}