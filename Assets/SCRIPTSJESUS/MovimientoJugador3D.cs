using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador3D : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

     void Update()
    {
        movement.x = Input.GetAxis("Horizontal"); 
        movement.z = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = movement.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + moveDirection);
    }


}
