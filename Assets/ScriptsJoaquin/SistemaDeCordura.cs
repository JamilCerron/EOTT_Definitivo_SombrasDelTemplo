using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SistemaDeCordura : MonoBehaviour
{
    private static SistemaDeCordura instance;

    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioClip cordura75;
    [SerializeField] private AudioClip cordura50;
    [SerializeField] private AudioClip cordura15;
    private PlayerStats playerStats;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    public static SistemaDeCordura Instance
    {
        get 
        { 
            return instance; 
        }
    }

    private void Awake()
    {
        instance = this;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        float corduraActual = playerStats.CorduraActual();

        if (corduraActual <= 75 && corduraActual > 50)
        {
            ReproducirSonido(cordura50);
        }
        else if (corduraActual <= 50 && corduraActual > 20)
        {
            ReproducirSonido(cordura50);
        }
        else if (corduraActual <= 20 && corduraActual > 15)
        {
            ShakeCamera();
        }
        else if (corduraActual <= 15)
        {
            ReproducirSonido(cordura15);
        }
    }

    private void ShakeCamera()
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse(Vector3.up * 2f);
        }
    }

    private void ReproducirSonido(AudioClip clip)
    {
        // Solo reproduce el sonido si no hay uno en curso
        if (!fxSource.isPlaying)
        {
            fxSource.clip = clip;
            fxSource.Play();
        }
    }
}
