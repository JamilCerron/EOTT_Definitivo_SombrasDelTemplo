using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Codigo_Pausa : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool Pausa = false;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pausa == false)
            {
                ObjetoMenuPausa.SetActive(true);
                Pausa = true;
                Time.timeScale = 0;
            }
            else if (Pausa == true)
            {
                Resumir();
            }
        }
    }
    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        Pausa = false;
        Time.timeScale = 1;
    }
    public void IrAlMenu(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu);
    }
}
