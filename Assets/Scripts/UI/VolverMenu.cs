using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlMenu : MonoBehaviour
{
    
    public string nombreEscenaMenu = "MenuPrincipal";

    
    public void CargarMenuPrincipal()
    {
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}

