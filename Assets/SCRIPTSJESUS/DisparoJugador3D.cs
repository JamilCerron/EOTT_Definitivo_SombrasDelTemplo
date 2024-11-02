using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DisparoJugador3D : MonoBehaviour
{
    public GameObject balaprefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 0.5f;
    public float tiempoVidaBala = 2f;
    public Camera cam;
    private RaycastHit hit;

    private bool puedeDisparar = true;

    void Update()
    {
        RotarHaciaMouse();

        if (Input.GetMouseButtonDown(0) && puedeDisparar)
        {
            Disparar();
        }
    }

    private void RotarHaciaMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        
        Ray ray = cam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPoint = hit.point;

          
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0; 

            if (direction.sqrMagnitude > 0.01f) 
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction); 
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); 
            }
        }
    }


    private void Disparar()
    {
        GameObject bala = Instantiate(balaprefab, puntoDisparo.position, Quaternion.LookRotation((puntoDisparo.position - hit.point).normalized));

       
        Rigidbody balaRb = bala.GetComponent<Rigidbody>();
        if (balaRb != null)
        {
            balaRb.AddForce((hit.point - puntoDisparo.position).normalized * 10f, ForceMode.Impulse);
        }

        Destroy(bala, tiempoVidaBala);
        puedeDisparar = false;
        StartCoroutine(CoolDownDisparo());
    }

    private IEnumerator CoolDownDisparo()
    {
        yield return new WaitForSeconds(tiempoEntreDisparos);
        puedeDisparar = true;
    }
}
