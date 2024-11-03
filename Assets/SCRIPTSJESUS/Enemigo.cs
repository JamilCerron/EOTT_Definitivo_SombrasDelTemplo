using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Tag para identificar al enemigo 
    public string enemytag = "Enemigo";
    //Tiempo de aturdimiento despues de recibir un balazo
    public float stunTime = 10f;
    //referencia al componente de movimiento enemigo
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ;
    }

     void OnCollisionEnter(Collision collision)
    {
        //Verifica si el objeto que colisiono es una bala
        if (collision.gameObject.tag == "Bala")
        {
            //Verifica si el enemigo no esta ya aturdido 
            if (!IsStunned())
            {
                //Aturde al enemigo
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                Stun();
            }
        }
    }

   public  void Stun()
    {
        //Imprime mensaje en consola
        Debug.Log("Enemigo aturdido");

        //Detiene el movimiento del enemigo
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //Espera durante el tiempo de aturdimiento
        Invoke("Unstun", stunTime);
    }

    void Unstun()
    {
        //Restablecer el movimiento el enemigo
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //Imprime mensaje en Consola
        Debug.Log("Enemigo ya no esta aturdido");

    }

    public bool IsStunned()
    {
        //Verifica si el enemigo esa aturdido 
        return rb.velocity == Vector3.zero && rb.angularVelocity == Vector3.zero;
    }
}
