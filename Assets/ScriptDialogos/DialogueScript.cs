using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] Lines;
    public float textSpeed = 0.2f;

    int index;


        
   
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
}
