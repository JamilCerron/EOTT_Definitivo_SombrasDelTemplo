using UnityEngine;
using UnityEngine.UI;

public class PuertaTumi : MonoBehaviour
{
    private GestorTumi gestorTumi;

    private void Start()
    {
        gestorTumi = FindObjectOfType<GestorTumi>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gestorTumi.ComprobarVictoria();
        }
    }
}
