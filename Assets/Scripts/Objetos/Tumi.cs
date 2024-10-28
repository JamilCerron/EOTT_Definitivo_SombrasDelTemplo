using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumi : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    private GestorTumi gestorTumi;

    private void Awake()
    {
        dialogueMark.SetActive(false);
        gestorTumi = GameObject.FindGameObjectWithTag("GestorTumi").GetComponent<GestorTumi>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dialogueMark.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                gestorTumi.AñadirFragmentos(1);
                Destroy(gameObject); // Destruye el objeto Tumi en la escena
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        dialogueMark.SetActive(false);
    }
}