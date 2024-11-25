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
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Vector3 direction = (FindObjectOfType<SupayMover>().transform.position - shootingPoint.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }
}
