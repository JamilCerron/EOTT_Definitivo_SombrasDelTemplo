using UnityEngine;

public class BalaEscopetaJ : MonoBehaviour
{
    private Rigidbody rb;
    [Header("Atributos")]
    [SerializeField] private float speed = 20f; // Velocidad de la bala
    [SerializeField] private Vector3 direction; // Direcci�n de la bala
    [SerializeField] private float tiempoDeVida = 5f; // Tiempo m�ximo antes de destruir la bala autom�ticamente
    private EspectroVida scriptEspectroVida;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (scriptEspectroVida != null)
        {
            scriptEspectroVida = GameObject.FindGameObjectWithTag("Enemigo").GetComponent<EspectroVida>();
        }
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
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Escopeta"))
        {
            return; // Ignorar colisiones con el jugador o el arma
        }

        if (collision.gameObject.CompareTag("Suelo"))
        {
            Destroy(gameObject);
        }

        else
        {
            Debug.Log($"La bala choc� con un objeto inesperado: {collision.gameObject.name}");
        }

        if (collision.gameObject.CompareTag("Enemigo"))
        {
            scriptEspectroVida.RecibirDa�o(25);
        }
    }
}