using UnityEngine;
using UnityEngine.SceneManagement;

public class Codigo_Pausa1 : MonoBehaviour
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

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void IrAlMenu(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu);
    }
    public void SalirDelJuego()
    {
        Application.Quit();
    }
}

