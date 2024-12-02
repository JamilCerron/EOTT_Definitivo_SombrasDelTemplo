using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Referencia al componente TextMeshProUGUI para mostrar el diálogo
    public string[] Lines; // Array de líneas de diálogo
    public float textSpeed = 0.05f; // Velocidad con la que se escriben las letras del diálogo
    public GameObject dialoguePanel; // Panel de diálogo que aparecerá
    public float displayTimeAfterText = 1f; // Tiempo que el texto se muestra completo antes de pasar al siguiente

    private int index; // Índice de la línea de diálogo actual
    private Coroutine currentCoroutine; // Almacena la corrutina activa
    private static DialogueScript activeDialogue; // Referencia estática al diálogo activo

    void Start()
    {
        dialogueText.text = string.Empty;
        dialoguePanel.SetActive(false); // Asegúrate de que el panel esté oculto al inicio
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador ha entrado en el área del trigger
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;

            if (activeDialogue != null && activeDialogue != this)
            {
                // Si hay otro diálogo activo, deténlo
                activeDialogue.StopCurrentDialogue();
            }

            // Establece este script como el diálogo activo y comienza el diálogo
            activeDialogue = this;
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true); // Mostrar el panel de diálogo
        index = 0; // Empezar desde el primer diálogo
        currentCoroutine = StartCoroutine(WriteLine()); // Comenzar a mostrar el diálogo
    }

    IEnumerator WriteLine()
    {
        // Escribir una línea de texto letra por letra
        foreach (char letter in Lines[index].ToCharArray())
        {
            dialogueText.text += letter; // Añadir cada letra al texto
            yield return new WaitForSeconds(textSpeed); // Esperar antes de añadir la siguiente letra
        }

        // Esperar un tiempo después de que se haya mostrado todo el texto
        yield return new WaitForSeconds(displayTimeAfterText);

        // Pasar a la siguiente línea de diálogo o cerrar el panel si no hay más líneas
        NextLine();
    }

    void NextLine()
    {
        if (index < Lines.Length - 1)
        {
            index++; // Avanzar al siguiente diálogo
            dialogueText.text = string.Empty; // Limpiar el texto actual
            currentCoroutine = StartCoroutine(WriteLine()); // Mostrar la siguiente línea
        }
        else
        {
            EndDialogue();
        }
    }

    public void StopCurrentDialogue()
    {
        // Detener la corrutina activa y limpiar el diálogo actual
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        dialogueText.text = string.Empty;
        dialoguePanel.SetActive(false);
    }

    void EndDialogue()
    {
        // Finalizar el diálogo actual y limpiar
        dialogueText.text = string.Empty;
        dialoguePanel.SetActive(false);
        activeDialogue = null; // Limpiar la referencia al diálogo activo
    }

   
}
