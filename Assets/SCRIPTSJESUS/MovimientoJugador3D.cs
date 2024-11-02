using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador3D : MonoBehaviour
{
    public float speed= 5f;
    public GameObject balaPrefab; 
    public Transform puntoDisparo; 

    private Rigidbody rb;
    private Vector3 Movement;
    private bool puedeDisparar = true; // Control para permitir disparos
    public float tiempoEntreDisparos = 0.5f;


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

        // Disparo
        if (Input.GetMouseButtonDown(0) && puedeDisparar) // Click izquierdo del mouse
        {
            Disparar();
        }
    }

    private void Disparar()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direccionDisparo = (hit.point - puntoDisparo.position).normalized;
            GameObject bala = Instantiate(balaPrefab, puntoDisparo.position + Vector3.up * 1f, Quaternion.identity); 
            bala.GetComponent<BalaEscopeta>().Disparar(direccionDisparo);
            puntoDisparo.rotation = Quaternion.LookRotation(direccionDisparo);
            transform.rotation = Quaternion.LookRotation(direccionDisparo);

            // Evitar múltiples disparos
            puedeDisparar = false;
            StartCoroutine(CooldownDisparo());
        }
    }

    private IEnumerator CooldownDisparo()
    {
        yield return new WaitForSeconds(tiempoEntreDisparos); 
        puedeDisparar = true; 
    }
}
