using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FijarPosicionEnemigo : MonoBehaviour
{
    private float posicionInicialY;

     void Start()
    {
        posicionInicialY = transform.position.y;
    }

     void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, posicionInicialY, transform.position.z);
    }
}
