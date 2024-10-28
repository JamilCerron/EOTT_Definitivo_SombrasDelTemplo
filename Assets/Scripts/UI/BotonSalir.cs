using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonSalir : SistemaPantallas
{
    protected override void Init()
    {
        base.Init();
        botonSalir.onClick.AddListener(AccionBoton); // Asigna el m�todo espec�fico
    }

    public override void AccionBoton()
    {
        ReproducirSonido();
        Application.Quit();
    }
}
