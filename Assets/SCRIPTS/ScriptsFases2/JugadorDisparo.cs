using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorDisparo : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float shootingCooldown = 0.5f;
    private float nextShotTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + shootingCooldown;
            Shoot();
        }
    }

private void Shoot()
    {
        // Buscar el objeto Supay
        SupayMover supay = FindObjectOfType<SupayMover>();
        if (supay == null)
        {
            Debug.LogWarning("No se encontró el Supay, no se puede disparar.");
            return; // Salir del método si no hay enemigo
        }

        // Crear la bala
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

        // Calcular la dirección
        Vector3 direction = (supay.transform.position - shootingPoint.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }
}
