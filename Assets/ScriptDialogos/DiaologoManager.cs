using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaologoManager : MonoBehaviour
{
    public GameObject dialogPanel; // Panel del cuadro de di�logo
    public TextMeshProUGUI dialogText; // Texto del cuadro de di�logo
    public string[] dialogLines; // L�neas de di�logo
    private int currentLine = 0;

    private bool isDialogActive = false; // Para saber si el di�logo est� activo

    // Update is called once per frame
    void Update()
    {
        // Avanzar en el di�logo al presionar "E"
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
        Debug.Log("L�nea actual: " + currentLine); // Rastreo
        if (currentLine < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLine];
            Debug.Log("Mostrando l�nea: " + dialogLines[currentLine]); // Rastreo
        }
        else
        {
            EndDialog();
            Debug.Log("Di�logo terminado.");
        }
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        isDialogActive = false;
    }
}
