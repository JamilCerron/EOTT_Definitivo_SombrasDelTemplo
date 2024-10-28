using UnityEngine;

public class EspectroVida : MonoBehaviour
{
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private float vidaActual;
    [SerializeField] private bool esEspectral = true; // Si el enemigo es inmune al da�o f�sico
    [SerializeField] private GameObject particulasMuerte;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDa�o(float cantidad, bool esCrucifijo)
    {
        //if (esEspectral && !esCrucifijo)
        //{
        // Si es inmune al da�o f�sico y el da�o no proviene del crucifijo, no hacer nada
        //return;
        //}

        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        if (particulasMuerte != null)
        {
            Instantiate(particulasMuerte, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
