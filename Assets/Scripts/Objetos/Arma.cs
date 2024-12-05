using UnityEngine;

public class Arma : MonoBehaviour
{
    [Header("Cámara")]
    [SerializeField] private Transform camaraJugador; // La cámara en primera persona

    [Header("Offsets")]
    [SerializeField] private Vector3 offsetEscopeta = new Vector3(0.5f, -0.5f, 1f);    // Ajuste de posición para que la escopeta esté en el lugar correcto
    [SerializeField] private Vector3 offsetCrucifijo = new Vector3(0.5f, -0.5f, 0.65f); // Ajuste de posición para que el crucifijo esté en el lugar correcto

    private Vector3 offsetActual;

    private void Awake()
    {
        camaraJugador = GameObject.FindGameObjectWithTag("VirtualCamera").transform;

        // Inicializar el offset basado en el tag del arma
        if (CompareTag("Escopeta"))
        {
            offsetActual = offsetEscopeta;
        }
        else if (CompareTag("Crucifijo"))
        {
            offsetActual = offsetCrucifijo;
        }
    }

    void Update()
    {
        AliniearArma();
    }

    private void AliniearArma()
    {
        // Alinear el arma con la cámara usando el offset actual
        transform.position = camaraJugador.position + camaraJugador.TransformDirection(offsetActual);
        transform.rotation = Quaternion.Lerp(transform.rotation, camaraJugador.rotation, Time.deltaTime * 10f);
    }
}
