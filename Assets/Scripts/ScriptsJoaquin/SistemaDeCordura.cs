using Cinemachine;
using UnityEngine;

public class SistemaDeCordura : MonoBehaviour
{
    private static SistemaDeCordura instance;

    [Header("Audio Configuración")]
    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioClip cordura75;
    [SerializeField] private AudioClip cordura50;
    [SerializeField] private AudioClip cordura15;
    [SerializeField] private float volumenAudio = 0.25f;

    [Header("Cinemachine Configuración")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private NoiseSettings noiseSettings;

    private PlayerStats playerStats;

    private bool isShaking = false; // Control del estado de shake
    private float lastCorduraState = 100f; // Guarda el último rango de cordura para evitar duplicados

    public static SistemaDeCordura Instance => instance;

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
            ReproducirSonido(cordura75);
        }
        else if (corduraActual <= 50 && corduraActual > 25)
        {
            ReproducirSonido(cordura50);
        }
        else if (corduraActual <= 25 && corduraActual > 15)
        {
            if (!isShaking)
            {
                ShakeCamera();
            }
        }
        else if (corduraActual <= 15)
        {
            ReproducirSonido(cordura15);
        }

        // Detener ShakeCamera si cordura está fuera del rango [15, 25]
        if ((corduraActual > 25 || corduraActual <= 15) && isShaking)
        {
            StopShakeCamera();
        }

        lastCorduraState = corduraActual;
    }

    private void ShakeCamera()
    {
        var perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin != null)
        {
            perlin.m_NoiseProfile = noiseSettings;
            isShaking = true;
        }
    }

    private void StopShakeCamera()
    {
        var perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin != null)
        {
            perlin.m_NoiseProfile = null; // Elimina el perfil de ruido
            isShaking = false;
        }
    }

    private void ReproducirSonido(AudioClip clip)
    {
        if (fxSource.clip != clip || !fxSource.isPlaying)
        {
            fxSource.clip = clip;
            fxSource.volume = volumenAudio; // Ajusta el volumen desde el inspector
            fxSource.loop = true; // Activa el loop para los clips
            fxSource.Play();
        }
    }
}
