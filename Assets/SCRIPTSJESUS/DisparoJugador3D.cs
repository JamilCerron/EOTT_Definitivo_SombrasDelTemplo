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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (hit.point - firePoint.position).normalized;
            GirarFirePoint(direction);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    void Disparar()
    {
       
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        
        rb.velocity = firePoint.forward * bulletSpeed;

       
        rb.useGravity = false;
    }

    public void GirarFirePoint(Vector3 direccion)
    {
        firePoint.rotation = Quaternion.LookRotation(direccion);
    }


}
