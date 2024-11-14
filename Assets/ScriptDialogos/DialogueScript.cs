using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] Lines;
    public float textSpeed = 0.2f;
    public GameObject dialoguePanel; // Referencia al panel de diálogo

    int index;
    private bool playerInRange = false; // Para detectar si el jugador está cerca

    void Start()
    {
        dialogueText.text = string.Empty;
        dialoguePanel.SetActive(false);
    }

    
    void Update()
    {
        if (playerInRange && Input.GetMouseButtonDown(0))
        {
            if(dialogueText.text == Lines[index])
            {
                NextLine();
            }
            else 
            {
                StopAllCoroutines();
                dialogueText.text = Lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true); // Mostrar panel
        index = 0;
        StartCoroutine(WriteLine());


    }

    IEnumerator WriteLine()
    {
        foreach (char letter in Lines[index].ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < Lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) // Detectar colisión con el jugador
    {
        if (other.CompareTag("PLAYER"))
        {
            playerInRange = true;
            StartDialogue();
        }
    }

    private void OnTriggerExit(Collider other) // Finalizar interacción al salir del rango
    {
        if (other.CompareTag("PLAYER"))
        {
            playerInRange = false;
            dialoguePanel.SetActive(false);
            StopAllCoroutines();
            dialogueText.text = string.Empty;
        }
    }
}
