using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverJugador : MonoBehaviour
{
    public float velocidad = 10f; 

    private Rigidbody rb;

    public int salud = 100;

    public GameObject balaPrefab;  
    public Transform firePoint;
    [SerializeField] float velocidadBala = 4f;
    private Transform enemigoTransform;
    public void ReducirSalud(int dano)
    {
        salud -= dano;
        Debug.Log("El jugador recibi� " + dano + " de da�o. Salud restante: " + salud);

        if (salud <= 0)
        {
            Debug.Log("El jugador ha muerto");
            Destroy(gameObject);
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemigoTransform = GameObject.FindGameObjectWithTag("ENEMIGO").transform;
    }

    void FixedUpdate()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical).normalized;
        transform.position += movimiento * velocidad * Time.deltaTime;

        if (Input.GetMouseButtonDown(0)) 
        {
            Disparar();
        }
    }

    void Disparar()
    {
        if (enemigoTransform != null)
        {
            // Calcula la direcci�n hacia el enemigo
            Vector3 direccionAlEnemigo = (enemigoTransform.position - firePoint.position).normalized;

            // Crea la bala
            GameObject bala = Instantiate(balaPrefab, firePoint.position, Quaternion.identity);

            // Ajusta la rotaci�n de la bala para que apunte en la direcci�n correcta (hacia el enemigo)
            bala.transform.rotation = Quaternion.LookRotation(direccionAlEnemigo);

            // Asigna la velocidad a la bala en la direcci�n calculada
            bala.GetComponent<Rigidbody>().velocity = direccionAlEnemigo * velocidadBala;
        }
    }

}
