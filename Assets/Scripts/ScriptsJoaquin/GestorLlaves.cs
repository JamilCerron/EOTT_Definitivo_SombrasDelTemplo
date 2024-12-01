using System.Collections.Generic;
using UnityEngine;

public class GestorLlaves : MonoBehaviour
{
    public static GestorLlaves instancia;

    private HashSet<string> llavesRecogidas = new HashSet<string>(); // Lista de llaves recogidas

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegistrarLlave(string idLlave)
    {
        if (!llavesRecogidas.Contains(idLlave))
        {
            llavesRecogidas.Add(idLlave);
            Debug.Log($"Llave registrada: {idLlave}");
        }
    }

    public bool TieneLlave(string idLlave)
    {
        return llavesRecogidas.Contains(idLlave);
    }
}