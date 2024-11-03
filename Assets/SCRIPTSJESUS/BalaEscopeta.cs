using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEscopeta : MonoBehaviour
{
    public float speed = 20f; 
    public float tiempoVida = 2f;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }


}
