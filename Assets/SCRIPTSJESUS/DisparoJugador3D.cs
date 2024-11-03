using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DisparoJugador3D : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 direccion;

       
        if (Physics.Raycast(ray, out hit))
        {
            
            direccion = (hit.point - firePoint.position).normalized;
        }
        else
        {
            
            direccion = firePoint.forward;
        }

        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
          
            rb.velocity = direccion * bulletSpeed;
        }

    }
}
