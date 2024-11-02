using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEscopeta : MonoBehaviour
{
    public float speed = 20f;
    

    public void Disparar(Vector3 direccion)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = direccion.normalized * speed; //velocidad de la bala 
        Destroy(gameObject, 5f);
    }
}
