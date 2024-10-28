using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SistemaPantallas : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] protected GameObject panelMenu;
    [SerializeField] protected GameObject panelOpciones;

    [Header("Botones")]
    [SerializeField] protected Button botonJugar;
    [SerializeField] protected Button botonCreditos;
    [SerializeField] protected Button botonOpciones;
    [SerializeField] protected Button botonRegresar;
    [SerializeField] protected Button botonSalir;

    [Header("Fuente")]
    [SerializeField] protected AudioMixer mixer;
    [SerializeField] protected AudioSource fuenteFX;

    [Header("Sonido")]
    [SerializeField] protected AudioClip sonidoClick;

    [Header("Volumen")]
    [SerializeField] protected Slider volumenPrincipal;
    [SerializeField] protected Slider volumenFX;
    protected float ultimoVolumen;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        volumenPrincipal.onValueChanged.AddListener(CambiarVolumenPrincipal);
        volumenFX.onValueChanged.AddListener(CambiarVolumenFX);
    }

    public virtual void AccionBoton()
    {
        // Método genérico para los botones que será sobrescrito en cada hijo.
    }

    protected void ReproducirSonido()
    {
        fuenteFX.PlayOneShot(sonidoClick);
    }

    protected void CambiarVolumenPrincipal(float v)
    {
        mixer.SetFloat("VolMaster", v);
    }

    protected void CambiarVolumenFX(float v)
    {
        mixer.SetFloat("VolFX", v);
    }
}