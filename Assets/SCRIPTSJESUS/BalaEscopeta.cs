using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEscopeta : MonoBehaviour
{
    public float speed = 20f; 
    public float tiempoVida = 2f;
   

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, tiempoVida);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
           
            Enemigo enemigo = collision.gameObject.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirImpacto();
            }

            
            Destroy(gameObject);
        }
    }


}
