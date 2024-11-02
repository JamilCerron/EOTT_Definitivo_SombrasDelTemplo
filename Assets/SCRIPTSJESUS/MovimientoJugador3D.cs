using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador3D : MonoBehaviour
{
    public float speed= 5f;

    private Rigidbody rb;
    private Vector3 Movement;

     void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

     void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

       
        Vector3 Movement = new Vector3(horizontal, 0, vertical);

        if (Movement.magnitude > 1)
        {
            Movement.Normalize();
        }

        rb.MovePosition(rb.position + Movement * speed * Time.fixedDeltaTime);

    }

  

}
