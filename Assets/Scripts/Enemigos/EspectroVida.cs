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

    public void RecibirDaño(float cantidad, bool esCrucifijo)
    {
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
