using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaologoManager : MonoBehaviour
{
    public GameObject dialogPanel; // Panel del cuadro de diálogo
    public TextMeshProUGUI dialogText; // Texto del cuadro de diálogo
    public string[] dialogLines; // Líneas de diálogo
    private int currentLine = 0;

    private bool isDialogActive = false; // Para saber si el diálogo está activo

    // Update is called once per frame
    void Update()
    {
        // Avanzar en el diálogo al presionar "E"
        if (isDialogActive && Input.GetKeyDown(KeyCode.E))
        {
            AdvanceDialog();
        }
    }

    public void StartDialog(string[] lines)
    {
        dialogLines = lines;
        currentLine = 0;
        isDialogActive = true;
        dialogPanel.SetActive(true);
        dialogText.text = dialogLines[currentLine];
    }

    private void AdvanceDialog()
    {
        currentLine++;
        Debug.Log("Línea actual: " + currentLine); // Rastreo
        if (currentLine < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLine];
            Debug.Log("Mostrando línea: " + dialogLines[currentLine]); // Rastreo
        }
        else
        {
            EndDialog();
            Debug.Log("Diálogo terminado.");
        }
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        isDialogActive = false;
    }
}
