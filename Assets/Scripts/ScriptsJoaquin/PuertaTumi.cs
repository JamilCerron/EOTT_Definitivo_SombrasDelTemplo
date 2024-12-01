using UnityEngine;

public class PuertaTumi : MonoBehaviour
{
    private GestorTumi gestorTumi;
    [SerializeField] private GameObject se�al;

    private void Start()
    {
        gestorTumi = FindObjectOfType<GestorTumi>();
        se�al.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            se�al.SetActive(true);

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
            se�al.SetActive(false);
        }
    }
}
