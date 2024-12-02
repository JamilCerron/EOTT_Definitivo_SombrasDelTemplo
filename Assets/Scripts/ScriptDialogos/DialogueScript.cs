using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Referencia al componente TextMeshProUGUI para mostrar el di�logo
    public string[] Lines; // Array de l�neas de di�logo
    public float textSpeed = 0.05f; // Velocidad con la que se escriben las letras del di�logo
    public GameObject dialoguePanel; // Panel de di�logo que aparecer�
    public float displayTimeAfterText = 1f; // Tiempo que el texto se muestra completo antes de pasar al siguiente

    private int index; // �ndice de la l�nea de di�logo actual
    private Coroutine currentCoroutine; // Almacena la corrutina activa
    private static DialogueScript activeDialogue; // Referencia est�tica al di�logo activo

    void Start()
    {
        dialogueText.text = string.Empty;
        dialoguePanel.SetActive(false); // Aseg�rate de que el panel est� oculto al inicio
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador ha entrado en el �rea del trigger
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;

            if (activeDialogue != null && activeDialogue != this)
            {
                // Si hay otro di�logo activo, det�nlo
                activeDialogue.StopCurrentDialogue();
            }

            // Establece este script como el di�logo activo y comienza el di�logo
            activeDialogue = this;
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true); // Mostrar el panel de di�logo
        index = 0; // Empezar desde el primer di�logo
        currentCoroutine = StartCoroutine(WriteLine()); // Comenzar a mostrar el di�logo
    }

    IEnumerator WriteLine()
    {
        // Escribir una l�nea de texto letra por letra
        foreach (char letter in Lines[index].ToCharArray())
        {
            dialogueText.text += letter; // A�adir cada letra al texto
            yield return new WaitForSeconds(textSpeed); // Esperar antes de a�adir la siguiente letra
        }

        // Esperar un tiempo despu�s de que se haya mostrado todo el texto
        yield return new WaitForSeconds(displayTimeAfterText);

        // Pasar a la siguiente l�nea de di�logo o cerrar el panel si no hay m�s l�neas
        NextLine();
    }

    void NextLine()
    {
        if (index < Lines.Length - 1)
        {
            index++; // Avanzar al siguiente di�logo
            dialogueText.text = string.Empty; // Limpiar el texto actual
            currentCoroutine = StartCoroutine(WriteLine()); // Mostrar la siguiente l�nea
        }
        else
        {
            EndDialogue();
        }
    }

    public void StopCurrentDialogue()
    {
        // Detener la corrutina activa y limpiar el di�logo actual
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        dialogueText.text = string.Empty;
        dialoguePanel.SetActive(false);
    }

    void EndDialogue()
    {
        // Finalizar el di�logo actual y limpiar
        dialogueText.text = string.Empty;
        dialoguePanel.SetActive(false);
        activeDialogue = null; // Limpiar la referencia al di�logo activo
    }

   
}
