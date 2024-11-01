using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 100;
    [SerializeField] private float tiempoParaRecuperacion = 10f;
    private float tiempoSinRecibirDaño;
    [SerializeField] private int vidaActual;

    [Header("Objetos")]
    [SerializeField] public int ContadorCruz = 0;
    [SerializeField] public int ContadorTumi = 0;
    [SerializeField] public float tiempoDeVidaCrucifijo = 0f; // Tiempo de vida actual del crucifijo
    public int tiempoMaximoCrucifijo = 5; // Tiempo máximo que dura el crucifijo
    public bool crucifijoActivo = false; // Estado del crucifijo
  //  [SerializeField] public int ContadorTumi = 0;
    private bool DentroAreaCruz = false;
    private bool DentroAreaTumi = false;            

    public CambioArma cambioArma; // Referencia a la clase CambioArma


    [Header("Cordura")]
    [SerializeField] private float corduraMaxima = 100f;
    [SerializeField] private float corduraActual;

    [Header("Resistencia")]
    [SerializeField] private float resistenciaMaxima = 100f;
    [SerializeField] private float resistenciaActual;
    [SerializeField] private float resistenciaRecuperacion = 10f; // Cuanta resistencia se recupera al no correr
    [SerializeField] private float resistenciaCorrer = 10f; // Cuanta resistencia se consume al correr

    [Header("Movimiento")]
    [SerializeField] private float velocidadCaminar = 10f;
    [SerializeField] private float velocidadCorrer = 20f;
    [SerializeField] private float fuerzaSalto = 10f;
    [SerializeField] private float escalaAgachar = 0.4f;
    private bool enSuelo = false;

    [SerializeField] private Vector2 sensibilidad;
    private Transform camaraJugador;
    private Rigidbody rb;

    [Header("Muerte")]
    [SerializeField] private string escenaMuertePorCordura;
    [SerializeField] private string escenaMuertePorVida;



    void Awake()
    {
        vidaActual = vidaMaxima;

        corduraActual = corduraMaxima;

        resistenciaActual = resistenciaMaxima;
        rb = GetComponent<Rigidbody>();

        camaraJugador = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        Movimiento();
        Camara();
        RecuperarResistencia();
        RecuperarVida();
        Saltar();
        Agachar();

        //// Reducir el tiempo de vida del crucifijo si está activo
        //if (crucifijoActivo && tiempoDeVidaCrucifijo > 0)
        //{
        //    tiempoDeVidaCrucifijo -= Time.deltaTime;

        //    // Cambiar automáticamente a la escopeta cuando el tiempo se agote
        //    if (tiempoDeVidaCrucifijo <= 0)
        //    {
        //        crucifijoActivo = false; // Desactivar el crucifijo
        //        tiempoDeVidaCrucifijo = 0; // Asegúrate de que no haya tiempo residual
        //        Debug.Log("El crucifijo ha expirado, cambiando a la escopeta.");
        //        cambioArma.armaSeleccionada = 0; // Cambia al arma correspondiente
        //        cambioArma.SeleccionarArma(); // Asegúrate de llamar a la función para seleccionar el arma
        //    }
        //}

        if (DentroAreaCruz && Input.GetKeyDown(KeyCode.E))
        {
            // Incrementar el contador de la cruz
            ContadorCruz++;

            
            // Destruir el objeto "Cruz"
            Destroy(GameObject.FindGameObjectWithTag("Cruz"));

            DentroAreaCruz = false;
        }
        else if (DentroAreaTumi && Input.GetKeyDown(KeyCode.E))
        {
            // Incrementar el contador de Tumi
            ContadorTumi++;

            Debug.Log("Contador de Tumi incrementado a: " + ContadorTumi);

            // Destruir el objeto "Tumi"
            Destroy(GameObject.FindGameObjectWithTag("Tumi"));

            DentroAreaTumi = false;
        }


        // Cambiar de arma con las teclas 1 y 2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cambioArma.armaSeleccionada = 0; // Escopeta
            cambioArma.SeleccionarArma();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cambioArma.CambiarACrucifijo(); // Cambia al crucifijo
        }
    }
    private void UsarCrucifijo()
    {
        if (ContadorCruz > 0)
        {
            tiempoDeVidaCrucifijo = tiempoMaximoCrucifijo; // Restablecer el tiempo
            crucifijoActivo = true; // Activar el crucifijo
            ContadorCruz--; // Decrementar el contador de cruz
            Debug.Log("Crucifijo activado. Tiempo de vida: " + tiempoDeVidaCrucifijo + " segundos.");
        }
    }

    void Movimiento()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");
        Vector3 velocidad = Vector3.zero;

        if (movimientoHorizontal != 0 || movimientoVertical != 0)
        {
            Vector3 direccion = (transform.forward * movimientoVertical + transform.right * movimientoHorizontal);
            // Correr o caminar segÚn la resistencia
            if (Input.GetKey(KeyCode.LeftShift) && resistenciaActual > 0)
            {
                velocidad = direccion * velocidadCorrer;
                resistenciaActual -= resistenciaCorrer * Time.deltaTime;
            }
            else
            {
                velocidad = direccion * velocidadCaminar;
            }
        }

        velocidad.y = rb.velocity.y;
        rb.velocity = velocidad;
    }

    void Saltar()
    {
        if (Input.GetButtonDown("Jump") && enSuelo == true)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    void Objeto(int value)
    {
        ContadorCruz += value;

    }

    void Agachar()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))
        {
            transform.localScale = new Vector3(transform.localScale.x, escalaAgachar, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        }
    }

    void Camara()
    {
        float movimientoHorizontal = Input.GetAxis("Mouse X");
        float movimientoVertical = Input.GetAxis("Mouse Y");

        if (movimientoHorizontal != 0)
        {
            transform.Rotate(Vector3.up, movimientoHorizontal * sensibilidad.x);
        }

        if (movimientoVertical != 0)
        {
            float angulo = (camaraJugador.localEulerAngles.x - movimientoVertical * sensibilidad.y + 360) % 360;

            if (angulo > 180)
            {
                angulo -= 360;
            }
            angulo = Mathf.Clamp(angulo, -80, 80); // Limitar el ángulo de rotación vertical
            camaraJugador.localEulerAngles = Vector3.right * angulo;
        }
    }

    void RecuperarResistencia()
    {
        if (resistenciaActual < resistenciaMaxima && rb.velocity.x == 0)
        {
            resistenciaActual += resistenciaRecuperacion * Time.deltaTime;
        }

        else if (resistenciaActual > resistenciaMaxima)
        {
            resistenciaActual = resistenciaMaxima;
        }
    }

    private void RecuperarVida()
    {
        tiempoSinRecibirDaño += Time.deltaTime;

        if (tiempoSinRecibirDaño >= tiempoParaRecuperacion && vidaActual < vidaMaxima)
        {
            vidaActual += 1; // Regenera vida
            tiempoSinRecibirDaño = 0;
        }
    }

    public void ReducirCordura(float cantidad)
    {
        corduraActual -= cantidad;
        if (corduraActual <= 0)
        {
            SceneManager.LoadScene(escenaMuertePorCordura);
            Destroy(gameObject);
        }
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual <= 0)
        {
            SceneManager.LoadScene(escenaMuertePorVida);
            Destroy(gameObject);
        }
    }

    public void RecuperarCordura(float cantidad)
    {
        corduraActual = Mathf.Min(corduraMaxima, corduraActual + corduraMaxima * (cantidad / 100f));
    }

    public float CorduraActual()
    {
        return corduraActual;
    }

    public float CorduraMaxima()
    {
        return corduraMaxima;
    }

    public float VidaActual()
    {
        return vidaActual;
    }

    public float VidaMaxima()
    {
        return vidaMaxima;
    }

    public float ResistenciaActual()
    {
        return resistenciaActual;
    }

    public float ResistenciaMaxima()
    {
        return resistenciaMaxima;
    }

    public bool EstaEnSuelo()
    {
        return enSuelo;
    }

    public bool EstaEnAire()
    {
        return !EstaEnSuelo();
    }

    public int FragmentosTumi()
    {
        return ContadorTumi;
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
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
              // Verificar si el objeto tiene la etiqueta "Cruz"
              if (other.CompareTag("Cruz"))
              {
                 DentroAreaCruz = true;
              }
        }

        if (other.gameObject.CompareTag("Tumi"))
        {
            // Verificar si el objeto tiene la etiqueta "Cruz"
            if (other.CompareTag("Tumi"))
            {
                DentroAreaTumi = true;
            }
        }


        //if (other.CompareTag("ZonaDeMuerte"))
        //{
        //    gameObject.SetActive(false);
        //    SceneManager.LoadScene(escenaMuertePorCordura);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cruz"))
        {
            // Verificar si el objeto tiene la etiqueta "Cruz"
            if (other.CompareTag("Cruz"))
            {
                DentroAreaCruz = false;
            }
        }

        if(other.gameObject.CompareTag("Tumi"))
        {
            // Verificar si el objeto tiene la etiqueta "Cruz"
            if (other.CompareTag("Tumi"))
            {
                DentroAreaTumi = false;
            }
        }
    }

}


