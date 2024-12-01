using UnityEngine;

public class PuertaTumi : MonoBehaviour
{
    private GestorTumi gestorTumi;
    [SerializeField] private GameObject señal;

    private void Start()
    {
        gestorTumi = FindObjectOfType<GestorTumi>();
        señal.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            señal.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                gestorTumi.ComprobarVictoria();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            señal.SetActive(false);
        }
    }
}
