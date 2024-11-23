using UnityEngine;

public class EspectroVida : MonoBehaviour
{
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private float vidaActual;
    [SerializeField] private GameObject particulasMuerte;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDa�o(float cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }
}
