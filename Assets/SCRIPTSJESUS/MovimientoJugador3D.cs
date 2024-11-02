using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador3D : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

     void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

     void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }



}
