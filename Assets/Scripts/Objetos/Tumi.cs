using UnityEngine;

public class Tumi : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    private GestorTumi gestorTumi;

    private void Awake()
    {
        dialogueMark.SetActive(false);
        InicializarGestorTumi();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dialogueMark.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                InicializarGestorTumi(); // Asegurar que gestorTumi esté inicializado

                if (gestorTumi != null)
                {
                    if (gestorTumi.ObtenerFragmentos() < 4)
                    {
                        gestorTumi.AñadirFragmentos(1);
                        Destroy(gameObject); // Destruye el objeto Tumi en la escena
                    }
                    else
                    {
                        Debug.Log("Ya tienes todos los fragmentos del Tumi.");
                    }
                }
                else
                {
                    Debug.LogError("GestorTumi no está presente o no se pudo inicializar.");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dialogueMark.SetActive(false);
        }
    }

    private void InicializarGestorTumi()
    {
        if (gestorTumi == null)
        {
            // Intentar obtener el gestor del singleton
            gestorTumi = GestorTumi.instancia;

            if (gestorTumi == null)
            {
                // Respaldo: buscar el objeto GestorTumi en la escena
                gestorTumi = GameObject.Find("GestorTumi")?.GetComponent<GestorTumi>();

                if (gestorTumi == null)
                {
                    // Último recurso: usar FindObjectOfType
                    gestorTumi = FindObjectOfType<GestorTumi>();
                }

                if (gestorTumi == null)
                {
                    Debug.LogError("GestorTumi no se encontró en la escena.");
                }
                else
                {
                    Debug.LogWarning("GestorTumi fue encontrado usando un respaldo. Revisa el patrón singleton.");
                }
            }
        }
    }
}