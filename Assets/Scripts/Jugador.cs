using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Jugador : Personaje
{

    [SerializeField] public float vida = 100f;
    public float Maxvida = 100f;
    [SerializeField] public float cordura = 100f;
    public float Maxcordura = 100f;

    [SerializeField] public float ContadorObjetos = 0f;
    [SerializeField] public float ContadorTumi = 0f;

    //public float MaxcorduraObjetos = 2f;


    public Transform[] enemigos;  // Array para almacenar todos los enemigos en la escena
    public float radioPerdidaCordura = 5f;  // Radio en el que se pierde cordura
    public int perdidaCorduraPorSegundo = -1;  // Cordura que se pierde por segundo al ver un enemigo


    private void Start()
    {
        StartCoroutine(AumentarCordura());  // Corutina para aumentar la cordura cada 10 segundos
        StartCoroutine(VerificarEnemigosCercanos());  // Corutina para verificar enemigos cercanos
    }

    //caminar
    protected override void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Movemos al jugador según la velocidad y las direcciones
        Vector3 movement = transform.forward * vertical + transform.right * horizontal;
        movement.Normalize(); // Normalizamos para evitar mayor velocidad en diagonales
        rb.velocity = new Vector3(movement.x * velocidad, rb.velocity.y, movement.z * velocidad);

    }
    //Correr
    protected override void Correr()
    {
        if(Input.GetKey(KeyCode.LeftShift) && EstaEnSuelo())
        {
            velocidad = 8.5f;
        }
        else
        {
            velocidad = 4.5f;
        }
    }
    //Saltar
    protected override void Saltar()
    {
        if(Input.GetButtonDown("Jump") && EstaEnSuelo())
        { 
            rb.AddForce(new Vector3(0,fuerzaSalto,0) , ForceMode.Impulse);
        
        }
    }
    //Agacharse
    protected override void Agacharse()
    {
       if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, 0.35f, transform.localScale.z);
            agachado = true;
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            agachado = false;
        }
    }
    //Objeto
    protected override void Objeto(int value)
    {
        ContadorObjetos += value;
        ContadorTumi += value;
        
    }

    //Vida
    protected override void CambioVida(int value)
    {
        vida += value;

        if (vida > Maxvida)
        {
           vida = Maxvida;

        }


        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
    //Cordura
    protected override void CambioCordura(int value)
    {
        cordura += value;

        if (cordura > Maxcordura)
        {
            cordura = Maxcordura;

        }

        if (cordura <= 0)
        {
            Destroy(gameObject);
        }



    }

    IEnumerator AumentarCordura()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);  // Esperar 10 segundos
            CambioCordura(2);  // Aumenta 2 de cordura cada 10 segundos
        }
    }

    IEnumerator VerificarEnemigosCercanos()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);  // Verifica cada segundo

            bool enemigoCercano = false;

            foreach (Transform enemigo in enemigos)
            {
                // Si la distancia es menor o igual al radio de pérdida de cordura
                if (Vector3.Distance(transform.position, enemigo.position) <= radioPerdidaCordura)
                {
                    enemigoCercano = true;
                    break;  // Si encontramos un enemigo en el rango, salimos del bucle
                }
            }

            // Si hay un enemigo cercano, perder cordura
            if (enemigoCercano)
            {
                CambioCordura(perdidaCorduraPorSegundo);  // Perder cordura cada segundo
            }
        }
    }


    //Metodo para collisiones con el suelo

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
        

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo01"))
        {
            CambioVida(-5);
            CambioCordura(-10);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cruz"))
        {
            ContadorObjetos++;
            

            // Destruir el objeto de munición (opcional)
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Tumi"))
        {
            ContadorTumi++;
            Destroy(other.gameObject);

        }
    }


}
