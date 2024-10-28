using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonCreditos : SistemaPantallas
{
    protected override void Init()
    {
        base.Init();
        botonCreditos.onClick.AddListener(AccionBoton); // Asigna el m�todo espec�fico
    }

    public override void AccionBoton()
    {
        ReproducirSonido();
        SceneManager.LoadScene("PantallaDeCr�ditos");
    }
}