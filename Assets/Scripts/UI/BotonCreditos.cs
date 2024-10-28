using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonCreditos : SistemaPantallas
{
    protected override void Init()
    {
        base.Init();
        botonCreditos.onClick.AddListener(AccionBoton); // Asigna el método específico
    }

    public override void AccionBoton()
    {
        ReproducirSonido();
        SceneManager.LoadScene("PantallaDeCréditos");
    }
}