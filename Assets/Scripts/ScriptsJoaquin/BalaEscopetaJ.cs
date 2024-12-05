using UnityEngine;

public class BalaEscopetaJ : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Atributos")]
    [SerializeField] private float speed = 20f; // Velocidad de la bala
    [SerializeField] private Vector3 direction; // Direcci�n de la bala
    [SerializeField] private float tiempoDeVida = 5f; // Tiempo m�ximo antes de destruir la bala autom�ticamente
    public int dano = 1; // Da�o infligido por la bala

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Destruye la bala autom�ticamente despu�s de cierto tiempo
        Destroy(gameObject, tiempoDeVida);
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void FixedUpdate()
    {
        // Mueve la bala en la direcci�n configurada
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Escopeta"))
        {
            return; // Ignorar colisiones con el jugador o el arma
        }

        if (collision.gameObject.CompareTag("Suelo") || collision.gameObject.CompareTag("Muro")  || collision.gameObject.CompareTag("Techo"))
        {
            Destroy(gameObject); // Destruye la bala si impacta el suelo
            return;
        }

        if (collision.gameObject.CompareTag("Enemigo"))
        {
            // Intenta infligir da�o al enemigo si tiene el script adecuado
            CabezaSupay enemigo = collision.gameObject.GetComponent<CabezaSupay>();
            if (enemigo != null)
            {
                //enemigo.RecibirGolpe(dano);
            }

            // Destruir la bala despu�s de impactar
            Destroy(gameObject);
            return;
        }

        Debug.Log($"La bala choc� con un objeto inesperado: {collision.gameObject.name}");
        Destroy(gameObject); // Destruir la bala si choca con cualquier otra cosa
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Espectro"))
        {
            other.GetComponent<EspectroVida>().RecibirDa�o(25);
            Destroy(gameObject);
            return;
        }
    }
}

