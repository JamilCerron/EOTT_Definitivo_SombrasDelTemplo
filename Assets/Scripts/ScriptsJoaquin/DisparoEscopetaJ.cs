using UnityEngine;

public class DisparoEscopetaJ : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject escopetaIcono;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;

    [Header("Atributos")]
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private int municionMaxima = 16;
    [SerializeField] private int municionActual = 0;
    [SerializeField] private int municionRecarga = 0;
    [SerializeField] private bool tieneEscopeta = false;

    [Header("Cooldown")]
    [SerializeField] private float tiempoEntreDisparos = 0.5f; // Tiempo entre disparos
    private float siguienteDisparo = 0f; // Tiempo para el próximo disparo permitido

    private void Awake()
    {
        municionActual = municionMaxima;
        if (tieneEscopeta == false)
        {
            escopetaIcono.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && tieneEscopeta && Time.time >= siguienteDisparo && municionActual > 0)
        {
            Disparar();
            siguienteDisparo = Time.time + tiempoEntreDisparos; // Actualizar tiempo para el próximo disparo
        }

        if (Input.GetKeyDown(KeyCode.R) && tieneEscopeta)
        {
            Recargar();
        }
    }

    private void Disparar()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<BalaEscopetaJ>().SetDirection(bulletSpawn.forward);
        bullet.GetComponent<BalaEscopetaJ>().SetSpeed(bulletSpeed);

        municionActual--;

        //Debug.Log("Disparo realizado. Munición actual: " + municionActual);
    }

    public void EquiparEscopeta()
    {
        tieneEscopeta = true;
        escopetaIcono.SetActive(true);
    }

    public bool TieneEscopeta()
    {
        return tieneEscopeta;
    }

    public void AumentarMunicion(int cantidad)
    {
        municionRecarga += cantidad;
        Debug.Log("Munición aumentada. Munición de recarga: " + municionRecarga);
    }

    private void Recargar()
    {
        if (municionActual < municionMaxima)
        {
            if (municionRecarga > 0)
            {
                municionActual = Mathf.Min(municionActual + 6, municionMaxima); ; // Llena la munición al máximo
                municionRecarga -= 6;
                Debug.Log("Recargado: " + municionActual + " balas.");
            }
            else
            {
                Debug.Log("Sin munición para recargar.");
            }
        }
        else
        {
            Debug.Log("Munición ya está llena.");
        }
    }

    public int EstadoMunicionMaxima()
    {
        return municionMaxima;
    }

    public int EstadoMunicionActual()
    {
        return municionActual;
    }

    public int EstadoMunicionRecarga()
    {
        return municionRecarga;
    }
}
