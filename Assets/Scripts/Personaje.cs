using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
     [SerializeField] protected float velocidad = 5f;
     [SerializeField]protected float fuerzaSalto = 7f;
     public bool agachado =  false;

    protected Rigidbody rb;
    public bool enSuelo;

    private void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        Saltar();
        Correr();
        Agacharse();
    }

    protected virtual void Move()
    {

    }

    protected virtual void Saltar()
    {

    }

    protected virtual void Correr()
    {

    }

    protected virtual void Agacharse()
    {

    }

    protected virtual void Levantarse()
    {

    }
    protected virtual void Objeto(int value)
    {

    }

    protected virtual void CambioCordura(int value)
    {

    }

    protected virtual void CambioVida(int value)
    {
       
    }

    protected virtual bool EstaEnSuelo()
    {
        return enSuelo;
    }

    protected virtual bool EstaEnAire()
    {
        return !EstaEnSuelo();
    }

   
}
