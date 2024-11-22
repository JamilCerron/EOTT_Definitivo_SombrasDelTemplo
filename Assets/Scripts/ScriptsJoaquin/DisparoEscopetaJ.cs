using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        municionActual = municionMaxima;
        escopetaIcono.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && tieneEscopeta && municionActual > 0)
        {
            Disparar();
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

        Debug.Log("ADAJDAKDA");
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
    }

    private void Recargar()
    {
        if (municionActual < municionMaxima)
        {
            if (municionRecarga > 0)
            {
                municionActual = Mathf.Min(municionActual + 4, municionMaxima); ; // Llena la munición al máximo
                municionRecarga -= 4;
                Debug.Log("Recargado: " + municionActual + " balas.");
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
