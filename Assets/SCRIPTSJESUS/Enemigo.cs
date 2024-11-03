using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public bool estaAturdido = false; 
    public float tiempoAturdimiento = 10f; 
   

    public void RecibirImpacto()
    {
        Debug.Log("Recibiendo impacto.");
        if (!estaAturdido)
        {
            StartCoroutine(AturdirEnemigo());
        }
    }

    IEnumerator AturdirEnemigo()
    {
        estaAturdido = true;


        Debug.Log("Enemigo está aturdido");

        yield return new WaitForSeconds(tiempoAturdimiento);

        Debug.Log("Enemigo ya no está aturdido");
        estaAturdido = false;
    }
}
